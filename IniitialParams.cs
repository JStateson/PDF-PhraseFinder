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

        private void btnApply_Click(object sender, EventArgs e)
        {
            char[] delimiters = new char[] { '\r', '\n' };

            string strIn = tbPhrases.Text;
            str1 = strIn.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in str1)
            {
                OutStr.Add(str);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            OutStr.Clear();
            this.Close();
        }
    }
}
