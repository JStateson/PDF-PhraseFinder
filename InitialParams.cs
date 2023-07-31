using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_PhraseFinder
{
    public partial class InitialParams : Form
    {
        private List<string> OutStr;
        private string[] str1;
        private string[] str2;  
        public InitialParams(ref string[] InitialPhrase, ref List<string> strReturn)
        {
            InitializeComponent();
            tbPhrases.Text = "";
            foreach (string Phrase in InitialPhrase)
            {
                tbPhrases.Text += Phrase + "\r\n";
            }
            OutStr = strReturn;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPhrases.Text = "";
        }

        private string[] StrToStrs(string strIn)
        {
            char[] delimiters = new char[] { '\r', '\n' };
            return strIn.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            str1 = StrToStrs(tbPhrases.Text);
            foreach (string str in str1)
            {
                OutStr.Add(str);
            }
            this.Close();
        }

        private bool CheckSyntax()
        {
            string BadLetters = ".,/|[]{}\\-_=!@#$%^&*()+`~,./;:'\"";
            string strBad = "";
            int n = BadLetters.Length;
            str1 = StrToStrs(tbPhrases.Text);
            foreach (string str in str1)
            {
                for(int i = 0; i < n; i++)
                {
                    if(str.Contains(BadLetters.Substring(i,1)))
                    {
                        strBad += str + "\r\n";
                        break;
                    }
                }
            }
            if(strBad != "")
            {
                MessageBox.Show(strBad, "Bad phrases");
            }
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OutStr.Clear();
            this.Close();
        }

        private void btnChkErr_Click(object sender, EventArgs e)
        {
            CheckSyntax();
        }
    }
}
