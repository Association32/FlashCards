using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCards
{
    partial class AddQuestionForm : Form
    {
        CardManager newCardManager;
        public AddQuestionForm(CardManager thisCardmanager)
        {
            newCardManager = thisCardmanager;
            InitializeComponent();
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            UpdateGUI();


        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = false;
            txtBoxAnswer.Text = "";
            txtBoxQuestion.Text = "";
            txtBoxFalse1.Text = "";
            txtBoxFalse2.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string question = txtBoxQuestion.Text.Trim();
            string answer = txtBoxAnswer.Text.Trim();
            string falseOne = txtBoxFalse1.Text.Trim();
            string falseTwo = txtBoxFalse2.Text.Trim();
            if((falseOne.Length>0) &&(falseTwo.Length>0))
            {
                string[] falseOnes = { falseOne, falseTwo };
                newCardManager.AddQuestion(question, answer, 0, 0, falseOnes);
            }
            else
            {
                newCardManager.AddQuestion(question, answer, 0, 0);
            }

            
            UpdateGUI();
        }

        private void CheckAdd()
        {
            if((txtBoxAnswer.Text.Trim().Length>0)&&(txtBoxQuestion.Text.Trim().Length > 0))
            {
                btnAdd.Enabled = true;
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        private void txtBoxQuestion_TextChanged(object sender, EventArgs e)
        {
            CheckAdd();
        }

        private void txtBoxAnswer_TextChanged(object sender, EventArgs e)
        {
            CheckAdd();
        }
    }
}
