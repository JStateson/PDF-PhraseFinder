﻿using Acrobat;
using AFORMAUTLib;

namespace PDF_PhraseFinder
{
    partial class PhraseFinderForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            ofd = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            btnExport = new Button();
            btnImport = new Button();
            btnAdd = new Button();
            btnRemove = new Button();
            btnSave = new Button();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            tbTotalMatch = new TextBox();
            groupBox2 = new GroupBox();
            tbNumPages = new TextBox();
            tbPdfName = new TextBox();
            groupBox5 = new GroupBox();
            groupBox4 = new GroupBox();
            btnStopScan = new Button();
            pbarLoading = new ProgressBar();
            btnRunSearch = new Button();
            groupBox6 = new GroupBox();
            groupBox9 = new GroupBox();
            btnInvert = new Button();
            btnUncheckall = new Button();
            btnSelectAll = new Button();
            cbIgnoreCase = new CheckBox();
            groupBox8 = new GroupBox();
            groupBox7 = new GroupBox();
            dgv_phrases = new DataGridView();
            tbMatches = new TextBox();
            gbPageCtrl = new GroupBox();
            tbViewPage = new TextBox();
            nudPage = new NumericUpDown();
            btnViewDoc = new Button();
            tbpageNum = new TextBox();
            MStrip.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox9.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_phrases).BeginInit();
            gbPageCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).BeginInit();
            SuspendLayout();
            // 
            // MStrip
            // 
            MStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem, settingsToolStripMenuItem });
            MStrip.Location = new Point(0, 0);
            MStrip.Name = "MStrip";
            MStrip.Size = new Size(1205, 24);
            MStrip.TabIndex = 0;
            MStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // ofd
            // 
            ofd.FileName = "openFileDialog1";
            // 
            // btnExport
            // 
            btnExport.Location = new Point(26, 32);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(75, 23);
            btnExport.TabIndex = 0;
            btnExport.Text = "Export";
            toolTip1.SetToolTip(btnExport, "copy to clipboard");
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // btnImport
            // 
            btnImport.Location = new Point(26, 81);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(75, 23);
            btnImport.TabIndex = 1;
            btnImport.Text = "Import";
            toolTip1.SetToolTip(btnImport, "copy from clipboard\r\nuse notepad to edit phrase \r\nthen select and copy");
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(49, 19);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(97, 23);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add Phrase";
            toolTip1.SetToolTip(btnAdd, "Adds a dummy  phrase");
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnRemove
            // 
            btnRemove.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnRemove.ForeColor = Color.Red;
            btnRemove.Location = new Point(49, 64);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(97, 23);
            btnRemove.TabIndex = 1;
            btnRemove.Text = "Delete Phrase";
            toolTip1.SetToolTip(btnRemove, "Delete all checked phrases");
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(49, 107);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(97, 23);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save Phrases";
            toolTip1.SetToolTip(btnSave, "Save in users app data");
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(tbPdfName);
            groupBox1.Location = new Point(253, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(556, 171);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Active PDF";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tbTotalMatch);
            groupBox3.Location = new Point(193, 85);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(108, 67);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Total Matches";
            // 
            // tbTotalMatch
            // 
            tbTotalMatch.Location = new Point(21, 28);
            tbTotalMatch.Name = "tbTotalMatch";
            tbTotalMatch.Size = new Size(58, 23);
            tbTotalMatch.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tbNumPages);
            groupBox2.Location = new Point(32, 85);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(104, 67);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Number Pages";
            // 
            // tbNumPages
            // 
            tbNumPages.Location = new Point(24, 29);
            tbNumPages.Name = "tbNumPages";
            tbNumPages.Size = new Size(58, 23);
            tbNumPages.TabIndex = 0;
            // 
            // tbPdfName
            // 
            tbPdfName.Location = new Point(76, 38);
            tbPdfName.Name = "tbPdfName";
            tbPdfName.Size = new Size(416, 23);
            tbPdfName.TabIndex = 0;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(groupBox4);
            groupBox5.Controls.Add(pbarLoading);
            groupBox5.Location = new Point(857, 27);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(322, 171);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "Search Progress";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tbpageNum);
            groupBox4.Controls.Add(btnStopScan);
            groupBox4.Location = new Point(39, 85);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(253, 67);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Exit Early";
            // 
            // btnStopScan
            // 
            btnStopScan.Enabled = false;
            btnStopScan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStopScan.ForeColor = Color.Red;
            btnStopScan.Location = new Point(31, 28);
            btnStopScan.Name = "btnStopScan";
            btnStopScan.Size = new Size(131, 23);
            btnStopScan.TabIndex = 0;
            btnStopScan.Text = "Click to stop";
            btnStopScan.UseVisualStyleBackColor = true;
            btnStopScan.Click += btnStopScan_Click;
            // 
            // pbarLoading
            // 
            pbarLoading.Location = new Point(39, 38);
            pbarLoading.Name = "pbarLoading";
            pbarLoading.Size = new Size(253, 23);
            pbarLoading.TabIndex = 0;
            // 
            // btnRunSearch
            // 
            btnRunSearch.Enabled = false;
            btnRunSearch.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnRunSearch.ForeColor = SystemColors.MenuHighlight;
            btnRunSearch.Location = new Point(61, 95);
            btnRunSearch.Name = "btnRunSearch";
            btnRunSearch.Size = new Size(135, 40);
            btnRunSearch.TabIndex = 3;
            btnRunSearch.Text = "Run Search";
            btnRunSearch.UseVisualStyleBackColor = true;
            btnRunSearch.Click += btnRunSearch_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(groupBox9);
            groupBox6.Controls.Add(cbIgnoreCase);
            groupBox6.Controls.Add(groupBox8);
            groupBox6.Controls.Add(groupBox7);
            groupBox6.Controls.Add(dgv_phrases);
            groupBox6.Location = new Point(33, 219);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(776, 355);
            groupBox6.TabIndex = 4;
            groupBox6.TabStop = false;
            groupBox6.Text = "Phrase Searching";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(btnInvert);
            groupBox9.Controls.Add(btnUncheckall);
            groupBox9.Controls.Add(btnSelectAll);
            groupBox9.Location = new Point(210, 194);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(176, 146);
            groupBox9.TabIndex = 4;
            groupBox9.TabStop = false;
            groupBox9.Text = "Checkbox Property";
            // 
            // btnInvert
            // 
            btnInvert.Location = new Point(42, 117);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(104, 23);
            btnInvert.TabIndex = 2;
            btnInvert.Text = "Invert Selection";
            btnInvert.UseVisualStyleBackColor = true;
            btnInvert.Click += btnInvert_Click;
            // 
            // btnUncheckall
            // 
            btnUncheckall.Location = new Point(42, 73);
            btnUncheckall.Name = "btnUncheckall";
            btnUncheckall.Size = new Size(104, 23);
            btnUncheckall.TabIndex = 1;
            btnUncheckall.Text = "Uncheck all";
            btnUncheckall.UseVisualStyleBackColor = true;
            btnUncheckall.Click += btnUncheckall_Click;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Location = new Point(42, 31);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(104, 23);
            btnSelectAll.TabIndex = 0;
            btnSelectAll.Text = "Check All";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += btnSelectAll_Click;
            // 
            // cbIgnoreCase
            // 
            cbIgnoreCase.AutoSize = true;
            cbIgnoreCase.Location = new Point(42, 95);
            cbIgnoreCase.Name = "cbIgnoreCase";
            cbIgnoreCase.Size = new Size(88, 19);
            cbIgnoreCase.TabIndex = 3;
            cbIgnoreCase.Text = "Ignore Case";
            cbIgnoreCase.UseVisualStyleBackColor = true;
            cbIgnoreCase.CheckedChanged += cbIgnoreCase_CheckedChanged;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnSave);
            groupBox8.Controls.Add(btnRemove);
            groupBox8.Controls.Add(btnAdd);
            groupBox8.Location = new Point(210, 31);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(176, 146);
            groupBox8.TabIndex = 2;
            groupBox8.TabStop = false;
            groupBox8.Text = "Phrases";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btnImport);
            groupBox7.Controls.Add(btnExport);
            groupBox7.Location = new Point(16, 203);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(147, 137);
            groupBox7.TabIndex = 1;
            groupBox7.TabStop = false;
            groupBox7.Text = "Clikpboard";
            // 
            // dgv_phrases
            // 
            dgv_phrases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_phrases.Location = new Point(413, 31);
            dgv_phrases.Name = "dgv_phrases";
            dgv_phrases.RowTemplate.Height = 25;
            dgv_phrases.Size = new Size(344, 309);
            dgv_phrases.TabIndex = 0;
            dgv_phrases.Click += dgv_phrases_Click;
            // 
            // tbMatches
            // 
            tbMatches.BorderStyle = BorderStyle.FixedSingle;
            tbMatches.Location = new Point(857, 236);
            tbMatches.Multiline = true;
            tbMatches.Name = "tbMatches";
            tbMatches.ScrollBars = ScrollBars.Vertical;
            tbMatches.Size = new Size(322, 205);
            tbMatches.TabIndex = 5;
            // 
            // gbPageCtrl
            // 
            gbPageCtrl.Controls.Add(tbViewPage);
            gbPageCtrl.Controls.Add(nudPage);
            gbPageCtrl.Controls.Add(btnViewDoc);
            gbPageCtrl.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            gbPageCtrl.Location = new Point(857, 459);
            gbPageCtrl.Name = "gbPageCtrl";
            gbPageCtrl.Size = new Size(306, 115);
            gbPageCtrl.TabIndex = 6;
            gbPageCtrl.TabStop = false;
            gbPageCtrl.Text = "Page View Control";
            gbPageCtrl.Visible = false;
            // 
            // tbViewPage
            // 
            tbViewPage.Location = new Point(178, 44);
            tbViewPage.Name = "tbViewPage";
            tbViewPage.Size = new Size(73, 33);
            tbViewPage.TabIndex = 2;
            tbViewPage.Visible = false;
            // 
            // nudPage
            // 
            nudPage.Location = new Point(132, 44);
            nudPage.Name = "nudPage";
            nudPage.Size = new Size(16, 33);
            nudPage.TabIndex = 1;
            nudPage.Visible = false;
            nudPage.ValueChanged += nudPage_ValueChanged;
            // 
            // btnViewDoc
            // 
            btnViewDoc.Location = new Point(15, 44);
            btnViewDoc.Name = "btnViewDoc";
            btnViewDoc.Size = new Size(99, 36);
            btnViewDoc.TabIndex = 0;
            btnViewDoc.Text = "View Doc";
            btnViewDoc.UseVisualStyleBackColor = true;
            btnViewDoc.Click += btnViewDoc_Click;
            // 
            // tbpageNum
            // 
            tbpageNum.Location = new Point(181, 29);
            tbpageNum.Name = "tbpageNum";
            tbpageNum.Size = new Size(51, 23);
            tbpageNum.TabIndex = 1;
            // 
            // PhraseFinderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1205, 586);
            Controls.Add(gbPageCtrl);
            Controls.Add(tbMatches);
            Controls.Add(groupBox6);
            Controls.Add(btnRunSearch);
            Controls.Add(groupBox5);
            Controls.Add(groupBox1);
            Controls.Add(MStrip);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MainMenuStrip = MStrip;
            Name = "PhraseFinderForm";
            Text = "USDA / FNS";
            FormClosing += PhraseFinderForm_FormClosing;
            MStrip.ResumeLayout(false);
            MStrip.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_phrases).EndInit();
            gbPageCtrl.ResumeLayout(false);
            gbPageCtrl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MStrip;
        private OpenFileDialog ofd;
        private ToolTip toolTip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private GroupBox groupBox1;
        private TextBox tbPdfName;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private TextBox tbTotalMatch;
        private TextBox tbNumPages;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private ProgressBar pbarLoading;
        private Button btnRunSearch;
        private GroupBox groupBox6;
        private DataGridView dgv_phrases;
        private TextBox tbMatches;
        private GroupBox groupBox7;
        private Button btnImport;
        private Button btnExport;
        private GroupBox groupBox8;
        private CheckBox cbIgnoreCase;
        private Button btnSave;
        private Button btnRemove;
        private Button btnAdd;
        private GroupBox groupBox9;
        private Button btnInvert;
        private Button btnUncheckall;
        private Button btnSelectAll;
        private GroupBox gbPageCtrl;
        private TextBox tbViewPage;
        private NumericUpDown nudPage;
        private Button btnViewDoc;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button btnStopScan;
        private TextBox tbpageNum;
    }
}