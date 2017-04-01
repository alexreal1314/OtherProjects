using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FourInARow
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ImportTextFromFile();
        }

        public void ImportTextFromFile()
        {
            string path = @"C:\FourInARowHelp.txt";
            string messageFileNotFound = @"File Not Found! Place 'FourInARowHelp.txt' in C:\";
            textBoxInstructions.ReadOnly = true;
            textBoxInstructions.BackColor = SystemColors.Window;

            if (!File.Exists(path))
            {
                textBoxInstructions.Text = messageFileNotFound;
            }
            else
            {
                textBoxInstructions.WordWrap = true;
                textBoxInstructions.Text = File.ReadAllText(path);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
