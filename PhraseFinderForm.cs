
using Acrobat;
using AFORMAUTLib;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

// data mining PDF appplication
// copyright 2023, Joseph Stateson  github/jstateson  
/*
 * Notes
 * must add references to adobe and set imbed to false or no
 * must select application and select settings to create settings.settings
 * had to select os 8, not 10.  not sure why sdk is missing???
 * copy BuildDate.txt and usda.ico in resources.resx
 * add this phrase to pre-build event: echo %date% %time% > "$(ProjectDir)\BuildDate.txt"

 */

namespace PDF_PhraseFinder
{

    public partial class PhraseFinderForm : Form
    {

        private CAcroApp acroApp;
        private AcroAVDoc ThisDoc;
        private CAcroAVPageView ThisDocView;
        private int[] ThisPageList;
        private int iCurrentPage = 0;
        private bool bStopEarly = false;
        private int NumPhrases = 5;
        private long TotalPDFPages, TotalMatches;
        private IFields theseFields;
        private bool bFormDirty = false;
        private StringCollection scSavedWords;
        private string CurrentActivePhrase = "";
        private int iNullCount = 0;
        private int iCurrentPagePhraseCount = 0;
        private int iCurrentPagePhraseActive = 0;
        private int iCurrentRow = 0;
        private int[] SrtIndex;
        private bool[] dgv_ph_CKs;

        private List<cPhraseTable> phlist = new List<cPhraseTable>();   // table of phrases
        private cLocalSettings LocalSettings = new cLocalSettings();    // table of settings
        private class cLocalSettings         // used to restore user settings
        {
            public bool bExitEarly;             // for debugging or demo purpose only examine a limited number of page
            public string strLastFolder = "";     // where last PDF was obtained
            public bool bIgnoreCase = true;
        }


        private class cPhraseTable
        {
            public bool Select { get; set; }
            public string Phrase { get; set; }
            public string Number { get; set; }
            public int iNumber;
            public int iDupPageCnt;
            public int iLastPage;
            public string strPages = "";
            public string[] strInSeries;
            public List<int> WordsOnPage = new List<int>();
            public int nFollowing; // number of words to check in sequence

            // count the number of following words that must match
            private int CountWords(string strIn)
            {
                char[] delimiters = new char[] { ' ', '\r', '\n' };
                strInSeries = strIn.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                int n = strInSeries.Length;
                if (n > 1) return n - 1; // if 2 words then must check one more word
                return 0;
            }
            public void InitPhrase(string aPhrase)
            {
                Select = true;
                Number = " ";
                iNumber = 0;
                strPages = "";
                iDupPageCnt = 0;
                iLastPage = -1;
                Phrase = aPhrase;
                nFollowing = CountWords(aPhrase);
            }

            public void InitPhrase(string aPhrase, bool bSelect)
            {
                Select = bSelect;
                Number = " ";
                iNumber = 0;
                strPages = "";
                iDupPageCnt = 0;
                iLastPage = -1;
                Phrase = aPhrase;
                nFollowing = CountWords(aPhrase);
            }

            public void AddPage(int jPage) // do not add the same page twice
            {
                int iPage = jPage + 1;
                if (strPages == "")
                {
                    strPages = iPage.ToString();
                    iLastPage = iPage;
                    WordsOnPage.Add(1);
                }
                else
                {
                    if (iLastPage == iPage)
                    {
                        iDupPageCnt++;
                        WordsOnPage[^1]++;  // increment the last page count
                        return;
                    }
                    strPages += "," + iPage.ToString();
                    WordsOnPage.Add(1);
                    iLastPage = iPage;
                }
            }
            public void IncMatch()
            {
                iNumber++;
            }
        }



        //string[] InitialPhrase = new string[NumPhrases] { " prorated ", " lender & grant ", " lender", " grant ", " contract & school & lunches " };
        // the above using "&" was not implementable because I was unable to read a line and know that two words were on the
        // same line.  Since the SDK retrieves whole words there is no need for a space before or after a phrase
        private string[] InitialPhrase = new string[5] { "school lunch", "prorated", "contract", "food service", "food" };
        private string[] WorkingPhrases = new string[5]; // same as above but optomises somewhat for case sensitivity
        private bool[] bUsePhrase = new bool[5] { true, true, true, true, true };
        private string strUsePhrase = "11111";

        //show a simple date on the form
        private string GetSimpleDate(string sDT)
        {
            //Sun 06/09/2019 23:33:53.18 
            int i = sDT.IndexOf(' ');
            i++;
            int j = sDT.LastIndexOf('.');
            return sDT.Substring(i, j - i);
        }


        /// <summary>
        /// change string value of 11l01 to true,true,true,false,true
        /// need to tell which phrases are to be used
        /// </summary>
        private void SetPhraseUsage()
        {
            int n = strUsePhrase.Length;
            bUsePhrase = new bool[n];
            for (int i = 0; i < n; i++)
            {
                bUsePhrase[i] = strUsePhrase.Substring(i, 1) == "1";
            }
        }
        /// <summary>
        /// change bool to string of 0 or 1
        /// </summary>
        private void SavePhraseUsage()
        {
            strUsePhrase = "";
            foreach (bool b in bUsePhrase)
            {
                strUsePhrase += b ? "1" : "0";
            }
            Properties.Settings.Default.UsePhrase = strUsePhrase;
        }

        /// <summary>
        /// need a bunch of '1' for boolean settings
        /// cannot save boolean as a property setting so using string of 1 and 0 instead
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string PadTrue(int n)
        {
            string strReturn = "";
            for (int i = 0; i < n; i++)
                strReturn += "1";
            return strReturn;
        }

        /// <summary>
        /// entry point for main form
        /// </summary>
        public PhraseFinderForm()
        {
            InitializeComponent();
            try
            {
                acroApp = new AcroAppClass();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Adobe PRO or STANDARD is not present");
                this.Close();
            }
            scSavedWords = new StringCollection();
            int n = 0;
            if (null != Properties.Settings.Default.SearchPhrases)
                n = Properties.Settings.Default.SearchPhrases.Count;
            if (n > 0)
            {
                string[] tempArr = new string[Properties.Settings.Default.SearchPhrases.Count];
                Properties.Settings.Default.SearchPhrases.CopyTo(tempArr, 0);
                scSavedWords.AddRange(tempArr);
                InitialPhrase = new string[n];
                WorkingPhrases = new string[n];
                NumPhrases = n;
                for (int i = 0; i < n; i++)
                {
                    InitialPhrase[i] = scSavedWords[i];
                }
            }
            else
            {
                SaveSettings();
            }
            if ("" != Properties.Settings.Default.UsePhrase)
            {
                strUsePhrase = Properties.Settings.Default.UsePhrase;
                if (NumPhrases != strUsePhrase.Length)
                {
                    strUsePhrase = PadTrue(NumPhrases);
                }
            }
            Properties.Settings.Default.UsePhrase = strUsePhrase;
            SetPhraseUsage();
            FillPhrases();
            GetLocalSettings();
            tbPdfName.Text = "Build date: " + GetSimpleDate(Properties.Resources.BuildDate) +
                " (v) 1.0 (c)Stateson";
        }


        /// <summary>
        /// user click the file open so help them find a pdf and
        /// report some success or failure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ofd = new OpenFileDialog();
            ofd.DefaultExt = "*pdf";
            ofd.InitialDirectory = LocalSettings.strLastFolder;
            ofd.Filter = "(Adobe PDF)|*.pdf";

            if (DialogResult.OK != ofd.ShowDialog())
            {
                tbPdfName.Text = "ERROR:no PDF file found";
                btnRunSearch.Enabled = false;
                return;
            }
            tbPdfName.Text = ofd.FileName;
            LocalSettings.strLastFolder = Path.GetDirectoryName(ofd.FileName);
            if (bFormDirty)
            {
                FillPhrases();
                bFormDirty = false;
            }
            // enable the run button if a docuement was loaded
            btnRunSearch.Enabled = bOpenDocs(tbPdfName.Text);
        }

        /// <summary>
        /// count number of matches for that were found for each phrase 
        /// and return the total number of matches
        /// </summary>
        /// <returns></returns>
        private long GetMatchCount()
        {
            long lCnt = 0;
            for (int i = 0; i < NumPhrases; i++)
            {
                int j = phlist[i].iNumber;
                lCnt += j;
                phlist[i].Number = j.ToString();
            }
            return lCnt;
        }

        /// <summary>
        /// progress bar
        /// note that DoEvents had the side effect of allowing the use to click on widgets
        /// while the program is running.  One must disable some feature to prevent, for example,
        /// starting a new search before the old search has finished.
        /// </summary>
        /// <param name="p"></param>
        private void SetPBAR(int p)
        {
            double pbarSlope = pbarLoading.Maximum * p;
            pbarSlope /= TotalPDFPages;
            pbarLoading.Value = Convert.ToInt32(pbarSlope);
            pbarLoading.Update();
            pbarLoading.Refresh();
            Application.DoEvents();
        }

        //open file needs adobe professional (not always found) in addition to badly formed PDFs
        // i need to give warning if PRO is not on the system
        // must set interop type to false for acrobat modules
        private bool bOpenDocs(string strPath)
        {
            try
            {
                AcroPDDocClass objPages = new AcroPDDocClass();
                objPages.Open(strPath);
                TotalPDFPages = objPages.GetNumPages();
                tbNumPages.Text = TotalPDFPages.ToString();
                objPages.Close();
            }
            catch
            {
                tbPdfName.Text = "missing Adobe DLL or bad file:" + tbPdfName.Text;
                return false;
            }
            return true;
        }


        private void FindMatches(ref string strBig, int j, int p)
        {
            string strPhrase = WorkingPhrases[j];
            int iWidth = strPhrase.Length;

            while (true)
            {
                int i = strBig.IndexOf(strPhrase);
                if (i == -1) return;
                phlist[j].AddPage(p);
                phlist[j].IncMatch();
                strBig = strBig.Remove(i, iWidth);
            }
        }

        private void GetFullPage(int p)
        {
            string word, strBig = "";
            int numWords = int.Parse(theseFields.ExecuteThisJavascript("event.value=this.getPageNumWords(" + p + ");"));
            for (int i = 0; i < numWords; i++)
            {
                try
                {
                    word = theseFields.ExecuteThisJavascript("event.value=this.getPageNthWord(" + p.ToString() + "," + i.ToString() + ", true);"); ;
                }
                catch (Exception e)
                {
                    word = "";
                    iNullCount++;
                }
                if (word != null)
                {
                    if (cbIgnoreCase.Checked) word = word.ToLower();
                    strBig += word;
                }
                strBig += " ";
            }

            for (int i = 0; i < NumPhrases; i++)
            {
                if (!phlist[i].Select) continue;
                FindMatches(ref strBig, i, p);
                //if (strBig.Contains(WorkingPhrases[i]))
                //{
                //    phlist[i].IncMatch();
                //    phlist[i].AddPage(p);
                //}
            }
        }
        // get the next word in the PDF
        private string GetThisWord(int iCurrent, int iLastWord, int iCurrentPage, ref bool bError)
        {
            string chkWord = "";
            try
            {
                chkWord = theseFields.ExecuteThisJavascript("event.value=this.getPageNthWord(" + iCurrentPage + "," + iCurrent + ", true);"); // true
            }
            catch
            {
                MessageBox.Show("Error in PDF at page " + iCurrentPage);
                bError = true;
                return "";
            }
            return chkWord;
        }

        /// <summary>
        /// This function highlights the phrase in  the pdf viewer
        /// </summary>
        private void ViewSelectedPage()
        {
            if (iCurrentPage < 0) return;
            try
            {
                ThisDocView = ThisDoc.GetAVPageView() as CAcroAVPageView;
                //ThisDocView.ZoomTo(1 /*AVZoomFitPage*/, 100); // was in an example app, not sure how useful
                ThisDocView.GoTo(iCurrentPage - 1);
                bool bFound = ThisDoc.FindText(CurrentActivePhrase,
                    cbIgnoreCase.Checked ? 0 : 1,
                    cbWholeWord.Checked ? 1 : 0,
                    0);
            }
            catch (Exception ex)
            {

            }

        }

        //AcroRd32.exe /A "zoom=50&navpanes=1=OpenActions&search=batch" PdfFile
        // above search for the phrase "batch"

        /// <summary>
        /// Open the PDF using Acrobat (I assume) and position to the current page selected
        /// </summary>
        /// <param name="fileName"></param>
        private void ViewDoc(string fileName)
        {
            if (iCurrentPage < 0) return;
            ThisDoc = new AcroAVDoc();
            ThisDoc.Open(fileName, "");
            ThisDoc.BringToFront();
            ThisDoc.SetViewMode(1); // (2)PDUseThumbs
            ViewSelectedPage();
        }

        private void SavePH_CheckMarks()
        {
            dgv_ph_CKs = new bool[NumPhrases];
            for(int i = 0; i < NumPhrases; i++)
            {
                dgv_ph_CKs[i] = dgv_phrases.Rows[i].Cells[0].Selected;
                phlist[i].Select = dgv_ph_CKs[i];
            }
        }


        private void RestorePH_CheckMarks()
        {
            for (int i = 0; i < NumPhrases; i++)
            {
                dgv_phrases.Rows[i].Cells[0].Selected= dgv_ph_CKs[i];
            }
        }

        /// <summary>
        /// Start the search. Unaccountably, checkmarks are all set to true after avDoc is created
        /// </summary>
        /// <returns></returns>

        private bool RunSearch()
        {
            string OutText = "";
            TotalMatches = 0;
            iNullCount = 0;
            string strPath = tbPdfName.Text;
            AcroAVDocClass avDoc = new AcroAVDocClass();
            IAFormApp formApp = new AFormAppClass();

            try
            {
                avDoc.Open(strPath, "Title");
                theseFields = (IFields)formApp.Fields;
            }
            catch
            {
                tbPdfName.Text = "corrupt pdf:" + tbPdfName.Text;
                return false;
            }
            for (int p = 0; p < TotalPDFPages; p++)
            {
                GetFullPage(p);
                SetPBAR(p);
                if ((p % 10) == 0)
                {
                    tbpageNum.Text = p.ToString();
                }
                if (bStopEarly)
                {
                    bStopEarly = false;
                    break;
                }
            }
            SetPBAR(0);   // clear the progress bar and show results of the pattern search
            for (int i = 0; i < NumPhrases; i++)
            {
                if (phlist[i].iNumber > 0)
                {
                    OutText += ">" + phlist[i].Phrase.ToUpper() + "<    found on following pages\r\n";
                    OutText += phlist[i].strPages + "\r\n";
                    OutText += "Total Duplicate pages: " + phlist[i].iDupPageCnt + "\r\n\r\n";
                }
            }
            tbMatches.Text = "";
            if (iNullCount > 0) tbMatches.Text = "Null words found:" + iNullCount.ToString() + "\r\n";
            tbMatches.Text += OutText;
            TotalMatches = GetMatchCount();
            tbTotalMatch.Text = TotalMatches.ToString();
            avDoc.Close(0);
            //avDoc = null;
            //formApp = null;
            dgv_phrases.DataSource = phlist.ToArray(); // connect results to the data grid view widget
            bFormDirty = true;
            gbPageCtrl.Visible = TotalMatches > 0;  // show page control group only if matches found
            RestorePH_CheckMarks();
            return true;
        }

        /// <summary>
        /// use only those phrases that have been selected
        /// </summary>
        private void FillPhrases()
        {
            cPhraseTable cpt;
            phlist.Clear();
            SortPhrasesList();
            for (int i = 0; i < NumPhrases; i++)
            {
                cpt = new cPhraseTable();
                cpt.InitPhrase(InitialPhrase[i], bUsePhrase[SrtIndex[i]]);
                phlist.Add(cpt);
            }

            dgv_phrases.DataSource = phlist.ToArray();
        }

        /// <summary>
        /// Initial setup of phrases, mark all as selected
        /// </summary>
        private void FillNewPhrases()
        {
            cPhraseTable cpt;
            phlist.Clear();
            for (int i = 0; i < NumPhrases; i++)
            {
                cpt = new cPhraseTable();
                cpt.InitPhrase(InitialPhrase[i]);
                phlist.Add(cpt);
            }
            dgv_phrases.DataSource = phlist.ToArray();
        }

        private void ClearLastResults()
        {
            FillPhrases();
            tbMatches.Clear();
        }

        private void FormSortList()
        {
            SrtIndex = new int[NumPhrases];
            int[] SrtValue = new int[NumPhrases];
            int j1, j2, k = NumPhrases - 1;
            for (int i = 0; i < NumPhrases; ++i)
            {
                SrtIndex[i] = i;
                SrtValue[i] = InitialPhrase[i].Length;
            }
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    j1 = SrtIndex[j];
                    j2 = SrtIndex[j + 1];
                    if (SrtValue[j1] < SrtValue[j2])
                    {
                        SrtIndex[j] = j2;
                        SrtIndex[j + 1] = j1;
                    }
                }
            }
        }

        private void SortPhrasesList()
        {
            int j;
            string[] pTemp = new string[NumPhrases];
            FormSortList();
            for (int i = 0; i < NumPhrases; i++)
            {
                j = SrtIndex[i];
                pTemp[i] = InitialPhrase[j];
            }
            for (int i = 0; i < NumPhrases; i++)
            {
                InitialPhrase[i] = pTemp[i];
            }
        }

        private void FormWorkingFromInitial()
        {

            for (int i = 0; i < NumPhrases; i++)
            {
                WorkingPhrases[i] = cbWholeWord.Checked ? " " : "";
                string strTemp = cbIgnoreCase.Checked ? InitialPhrase[i].ToLower() : InitialPhrase[i];
                WorkingPhrases[i] += strTemp.Trim();
                WorkingPhrases[i] += cbWholeWord.Checked ? " " : "";
            }

        }

        private void btnRunSearch_Click(object sender, EventArgs e)
        {
            ClearLastResults();
            FormWorkingFromInitial();
            btnRunSearch.Enabled = false;
            btnStopScan.Enabled = true;
            SavePH_CheckMarks();
            RunSearch();
            btnRunSearch.Enabled = true;
            btnStopScan.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int n = InitialPhrase.Length + 1;
            string[] OldPhrases = new string[n];
            bool[] bOld = new bool[n];
            WorkingPhrases = new string[n];
            for (int i = 0; i < n - 1; i++)
            {
                OldPhrases[i] = InitialPhrase[i];
                bOld[i] = bUsePhrase[i];
            }
            InitialPhrase = new string[n];
            bUsePhrase = new bool[n];
            for (int i = 0; i < n - 1; i++)
            {
                InitialPhrase[i] = OldPhrases[i];
                bUsePhrase[i] = bOld[i];
            }
            InitialPhrase[n - 1] = "change me then SAVE";
            bUsePhrase[n - 1] = true;
            NumPhrases = n;
            FillPhrases();
        }


        private void nudPage_ValueChanged(object sender, EventArgs e)
        {
            if (ThisPageList == null) return;
            int iVal = Convert.ToInt32(nudPage.Value);
            iCurrentPage = ThisPageList[iVal];
            tbViewPage.Text = iCurrentPage.ToString() + ".1";
            ViewDoc(tbPdfName.Text);
            iCurrentPagePhraseActive = 0;
            iCurrentPagePhraseCount = phlist[iCurrentRow].WordsOnPage[Convert.ToInt32(nudPage.Value)];
            btnNext.Visible = iCurrentPagePhraseCount > 0;
            return;
        }

        private void btnViewDoc_Click(object sender, EventArgs e)
        {
            tbViewPage.Text = iCurrentPage.ToString() + ".1";
            iCurrentPagePhraseActive = 0;
            nudPage.ValueChanged -= nudPage_ValueChanged;
            nudPage.Value = 0;  // cannot let the event fire when resetting the value of the widget
            nudPage.ValueChanged += nudPage_ValueChanged;
            ViewDoc(tbPdfName.Text);

        }

        /// <summary>
        /// Get the list of pages that contains the wanted phrase and update the
        /// numeric up/down widget so the pages can be scrolled
        /// </summary>
        private void GetSelection()
        {
            Point ThisRC = dgv_phrases.CurrentCellAddress;
            iCurrentRow = ThisRC.Y;
            int iCol = ThisRC.X;
            iCurrentPage = -1;
            if (phlist[iCurrentRow].strPages != "")
            {
                ThisPageList = phlist[iCurrentRow].strPages.Split(',').Select(int.Parse).ToArray();
                nudPage.Maximum = ThisPageList.Length - 1;
                tbViewPage.Text = ThisPageList[0].ToString() + ".1";
                tbViewPage.Visible = ThisPageList.Length > 0;   // only show page number of there are pages
                if (tbViewPage.Visible)
                    iCurrentPage = ThisPageList[0];
                nudPage.Visible = ThisPageList.Length > 1;      // only show numeric up/down if more than 1 page
                CurrentActivePhrase = phlist[iCurrentRow].Phrase;
                iCurrentPagePhraseActive = 0;
                iCurrentPagePhraseCount = phlist[iCurrentRow].WordsOnPage[0]; // [Convert.ToInt32(nudPage.Value)];
                btnNext.Visible = iCurrentPagePhraseCount > 0;
            }
        }

        private void dgv_phrases_Click(object sender, EventArgs e)
        {
            GetSelection();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int n = 0;
            int i = 0;
            List<int> KeepList = new List<int>();
            bool[] bOld;
            foreach (DataGridViewRow dgvr in dgv_phrases.Rows)
            {
                if (dgvr.Selected)
                {
                    n++;
                }
                else KeepList.Add(i);
                i++;
            }
            DialogResult dialogResult = MessageBox.Show(
    "This operation will delete " + n + " highlighted filter phrases.  Are you sure?",
    "Warning: don't forget to save", MessageBoxButtons.YesNo);
            if (n == 0) return;
            n = NumPhrases - n;
            if (dialogResult == DialogResult.Yes)
            {
                WorkingPhrases = new string[n];
                bOld = new bool[n];
                i = 0;
                foreach (int j in KeepList)
                {
                    WorkingPhrases[i] = InitialPhrase[j];
                    bOld[i] = bUsePhrase[j];
                    i++;
                }
                NumPhrases = n;
                InitialPhrase = new string[n];
                bUsePhrase = new bool[n];
                i = 0;
                foreach (string str in WorkingPhrases)
                {
                    InitialPhrase[i] = str;
                    bUsePhrase[i] = bOld[i];
                    i++;
                }
                FillPhrases();
            }
        }

        private void SaveSettings()
        {
            // should be at AppData\Local\Microsoft\YOUR APPLICATION NAME File name is user.config
            scSavedWords = new StringCollection();
            foreach (string str1 in InitialPhrase)
            {
                scSavedWords.Add(str1);
            }
            SavePhraseUsage();
            Properties.Settings.Default.SearchPhrases = scSavedWords;
            Properties.Settings.Default.Save();
        }

        // the phrase list was edited so copy the edits so they can be saved
        // the searching is done starting with the InitialPhrase list
        // and then setting up workingphrase and phlist 
        private void UpdateSettings()
        {
            for (int i = 0; i < NumPhrases; i++)
            {
                string strTemp = phlist[i].Phrase;
                InitialPhrase[i] = strTemp.Trim();
                bUsePhrase[i] = phlist[i].Select;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateSettings();
            SaveSettings();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NumPhrases; i++)
            {
                dgv_phrases.Rows[i].Cells[0].Value = true;
            }
        }

        private void btnUncheckall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NumPhrases; i++)
            {
                dgv_phrases.Rows[i].Cells[0].Value = false;
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NumPhrases; i++)
            {
                dgv_phrases.Rows[i].Cells[0].Value = !(bool)dgv_phrases.Rows[i].Cells[0].Value;
            }
        }

        private void cbIgnoreCase_CheckedChanged(object sender, EventArgs e)
        {
            LocalSettings.bIgnoreCase = cbIgnoreCase.Checked;
        }

        private void SaveLocalSettings()
        {
            Properties.Settings.Default.IsLastFolder = LocalSettings.strLastFolder;
            Properties.Settings.Default.bIgnoreCase = LocalSettings.bIgnoreCase;
            Properties.Settings.Default.Save();
        }

        private void GetLocalSettings()
        {
            LocalSettings.strLastFolder = Properties.Settings.Default.IsLastFolder;
            LocalSettings.bIgnoreCase = Properties.Settings.Default.bIgnoreCase;
            cbIgnoreCase.Checked = LocalSettings.bIgnoreCase;
            if (LocalSettings.strLastFolder == "")
            {
                LocalSettings.strLastFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                SaveLocalSettings();
            }
        }

        private void PhraseFinderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLocalSettings();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            string strOut = "";
            for (int i = 0; i < NumPhrases; i++)
            {
                strOut += phlist[i].Phrase + "\r\n";
            }
            System.Windows.Forms.Clipboard.SetText(strOut);
        }

        private void HaveNewPhrases(string strTemp)
        {
            string[] strTemps = Regex.Split(strTemp, "\r\n");
            NumPhrases = strTemps.Count();
            InitialPhrase = new string[NumPhrases];
            WorkingPhrases = new string[NumPhrases];
            for (int i = 0; i < NumPhrases; i++)
            {
                InitialPhrase[i] = strTemps[i];
            }
            FillPhrases();
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            string strTemp = System.Windows.Forms.Clipboard.GetText();
            if (strTemp == "")
            {
                MessageBox.Show("Clipboard is empty.  Do an export to see the correct format");
                return;
            }
            HaveNewPhrases(strTemp);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); // may want to prompt user to save changes ???
        }



        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help MyHelp = new help();
            MyHelp.Show();  // this leaves dialog box on the screen
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> strReturn = new List<string>();
            InitialParams ipSetup = new InitialParams(ref InitialPhrase, ref strReturn);
            ipSetup.ShowDialog();   // does not return unless dialog box closed
            if (strReturn.Count() == 0) return;
            NumPhrases = strReturn.Count();
            InitialPhrase = new string[NumPhrases];
            WorkingPhrases = new string[NumPhrases];
            for (int i = 0; i < NumPhrases; i++)
            {
                InitialPhrase[i] = strReturn[i];
            }
            SortPhrasesList();
            FillNewPhrases();
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            bStopEarly = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ThisPageList == null) return;
            iCurrentPagePhraseActive++;
            if (iCurrentPagePhraseActive == iCurrentPagePhraseCount)
            {
                nudPage.UpButton();
                return;
            }
            tbViewPage.Text = iCurrentPage.ToString() + "." + (1 + iCurrentPagePhraseActive).ToString();
            if (ThisDocView != null)
            {
                ViewSelectedPage();
            }
        }

        private void cbIgnoreCase_CheckedChanged_1(object sender, EventArgs e)
        {
            LocalSettings.bIgnoreCase = cbIgnoreCase.Checked;
        }
    }
}