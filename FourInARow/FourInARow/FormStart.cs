using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
    public partial class FormStart : Form
    {
        private FormGame m_MyGameForm;
        private int m_ButtonOkClickCount = 0;

        public FormStart(FormGame i_MyGame)
        {
            m_MyGameForm = i_MyGame;
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            m_ButtonOkClickCount++;

            if (m_ButtonOkClickCount > 1)
            {
                string message = "Start a new game?";
                DialogResult result = MessageBox.Show(message, "4 In A row", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    m_MyGameForm.ResetGameProperties();
                    m_MyGameForm.OkPressed();
                }
                else
                {
                    string noMessage = string.Format("New board size will take effect{0}on the next game.", Environment.NewLine);
                    MessageBox.Show(noMessage, "4 In A row", MessageBoxButtons.OK);
                    m_MyGameForm.InitGameNextRound = true;
                }
            }
            else
            {
                m_MyGameForm.OkPressed();
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            m_MyGameForm.Close();
            Close();
        }

        public string PlayerOneName
        {
            get
            {
                return textboxPlayer1.Text;
            }
        }

        public string PlayerTwoName
        {
            get
            {
                return textboxPlayer2.Text;
            }
        }

        public decimal NumOfRows
        {
            get
            {
                return numericUpDownRows.Value;
            }
        }

        public decimal NumOfCols
        {
            get
            {
                return numericUpDownCols.Value;
            }
        }
    }
}
