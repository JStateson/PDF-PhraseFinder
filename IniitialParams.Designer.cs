namespace PDF_PhraseFinder
{
    partial class InitialParams
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox2 = new GroupBox();
            btnCancel = new Button();
            btnApply = new Button();
            btnClear = new Button();
            tbPhrases = new TextBox();
            toolTip1 = new ToolTip(components);
            label1 = new Label();
            label2 = new Label();
            btnChkErr = new Button();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnChkErr);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(btnCancel);
            groupBox2.Controls.Add(btnApply);
            groupBox2.Controls.Add(btnClear);
            groupBox2.Controls.Add(tbPhrases);
            groupBox2.Location = new Point(31, 44);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(695, 376);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Default Phrases";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(553, 150);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(553, 100);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(75, 23);
            btnApply.TabIndex = 2;
            btnApply.Text = "Apply";
            toolTip1.SetToolTip(btnApply, "Copy phrases into table\r\nand saves them in windows\r\ndefault settings");
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(553, 53);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 1;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // tbPhrases
            // 
            tbPhrases.Location = new Point(6, 74);
            tbPhrases.Multiline = true;
            tbPhrases.Name = "tbPhrases";
            tbPhrases.ScrollBars = ScrollBars.Both;
            tbPhrases.Size = new Size(460, 281);
            tbPhrases.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Info;
            label1.Location = new Point(506, 265);
            label1.Name = "label1";
            label1.Size = new Size(178, 90);
            label1.TabIndex = 5;
            label1.Text = "Be sure to click the \r\n\"Save Phrases\"when\r\nyou return to the main form\r\nDashs, slash, underscore and\r\nall puncuation are ignored.\r\nDo not use them in your phrases";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Info;
            label2.Location = new Point(34, 39);
            label2.Name = "label2";
            label2.Size = new Size(299, 15);
            label2.TabIndex = 6;
            label2.Text = "Use the clipboard and notepad to save / restore phrases\r\n";
            // 
            // btnChkErr
            // 
            btnChkErr.Location = new Point(553, 210);
            btnChkErr.Name = "btnChkErr";
            btnChkErr.Size = new Size(102, 23);
            btnChkErr.TabIndex = 7;
            btnChkErr.Text = "Check Syntax";
            btnChkErr.UseVisualStyleBackColor = true;
            btnChkErr.Click += btnChkErr_Click;
            // 
            // InitialParams
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(759, 450);
            Controls.Add(groupBox2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "InitialParams";
            Text = "IniitialParams";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox2;
        private TextBox tbPhrases;
        private Button btnApply;
        private ToolTip toolTip1;
        private Button btnClear;
        private Button btnCancel;
        private Label label2;
        private Label label1;
        private Button btnChkErr;
    }
}