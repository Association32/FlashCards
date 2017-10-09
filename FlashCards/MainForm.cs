using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCards
{
    public partial class MainForm : Form
    {
        CardManager currManager;
        int posRightAnswer = -1;
        int step = 0;
        int turn = 0;
        private string result;
        Timer myTimer = new Timer();

        
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
            
        }

        private void InitializeGUI()
        {
            lblQuestion.Text = "";
            lblResult.Text = "";
            lblNrQuestion.Text = "";

            btnAltOne.Text = "";
            btnAltTwo.Text = "";
            btnAltThree.Text = "";

            btnSave.Enabled = false;
            btnAltTwo.Enabled = false;
            btnAltOne.Enabled = false;
            btnAltThree.Enabled = false;
            btnStart.Enabled = false;
            btnAdd.Enabled = false;
            currManager = new CardManager();
            myTimer.Tick += MyTimer_Tick;
            myTimer.Interval = 1;
            result = "";
            UpdateGUI();
            
        }

        private void UpdateGUI()
        {
            lblPoints.Text = "Score: " + currManager.GetPoints();
            lblNrQuestion.Text = "Max: " + currManager.GetCardsLength();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "txt files (*.txt)|*.txt";
            DialogResult result=  myDialog.ShowDialog();

            if(result ==  DialogResult.OK)
            {
                string address = myDialog.FileName;
                if(File.ReadAllLines(address).Length>2)
                {
                    currManager.GetQuestions(address);
                    btnAltTwo.Enabled = true;
                    btnAltOne.Enabled = true;
                    btnAltThree.Enabled = true;
                    btnStart.Enabled = true;
                    UpdateGUI();
                    btnAdd.Enabled = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Minumum three questions needed.");
                }
            }
        }

        private void GetQuestions()
        {
            if(currManager.CheckOK())
            {
                currManager.NewQuestion();
                lblQuestion.Text = currManager.PrintQuestion();
                Random myRandom = new Random();
                posRightAnswer = myRandom.Next(1, 4);
                if (posRightAnswer == 1)
                {
                    btnAltOne.Text = currManager.PrintTrue();
                    btnAltTwo.Text = currManager.PrintFirstFalse();
                    btnAltThree.Text = currManager.PrintSectFalse();
                }
                else if (posRightAnswer == 2)
                {
                    btnAltTwo.Text = currManager.PrintTrue();
                    btnAltOne.Text = currManager.PrintFirstFalse();
                    btnAltThree.Text = currManager.PrintSectFalse();
                }
                else
                {
                    btnAltThree.Text = currManager.PrintTrue();
                    btnAltOne.Text = currManager.PrintSectFalse();
                    btnAltTwo.Text = currManager.PrintFirstFalse();
                }
                UpdateGUI();
            }
            else
            {

                DialogResult result = MessageBox.Show("Vincere potes Hannibal.Victoria uti nescis!");

                if(result == DialogResult.OK)
                {
                    InitializeGUI();
                }
            }
            
        }
        private void Right()
        {
            currManager.RightAnswer();
            UpdateGUI();
            if(!currManager.CheckOK())
            {
                DialogResult result = MessageBox.Show("Vincere potes Hannibal. Victoria uti nescis!");

                if (result == DialogResult.OK)
                {
                    InitializeGUI();
                }
            }
            else
            {
                GetQuestions();
            }
        }

        private void btnAltOne_Click(object sender, EventArgs e)
        {
            if(posRightAnswer==1)
            {
                result = "True";
                Right();
            }
            else
            {
                result = "False";
                GetQuestions();
                currManager.WrongAnswer();
            }
            ShowResult();
            UpdateGUI();
        }

        private void btnAltTwo_Click(object sender, EventArgs e)
        {
            if (posRightAnswer == 2)
            {
                result = "True";
                Right();
            }
            else
            {
                result = "False";
                GetQuestions();
                currManager.WrongAnswer();
            }
            ShowResult();
            UpdateGUI();
        }

        private void btnAltThree_Click(object sender, EventArgs e)
        {
            if (posRightAnswer == 3)
            {
                result = "True";
                Right();
            }
            else
            {
                result = "False";
                GetQuestions();
                currManager.WrongAnswer();
            }
            ShowResult();
            UpdateGUI();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GetQuestions();
            btnStart.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddQuestionForm currForm = new AddQuestionForm(currManager);
            DialogResult result = currForm.ShowDialog();
            
            if((result == DialogResult.OK)&&(currManager.ListLengthOK()))
            {
                btnStart.Enabled = true;
                btnAltTwo.Enabled = true;
                btnAltOne.Enabled = true;
                btnAltThree.Enabled = true;
                btnStart.Enabled = true;
                UpdateGUI();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog myDialog = new SaveFileDialog();
            myDialog.Filter = "txt files (*.txt)|*.txt";
            DialogResult result = myDialog.ShowDialog();
            string saveString = currManager.SaveQuestions();
            

            if(result == DialogResult.OK)
            {
                string address = myDialog.FileName;
                File.WriteAllText(address, saveString, Encoding.UTF8);
            }
        }

        private void ShowResult()
        {
            turn = 0;
            myTimer.Start();

        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            step++;

            if(step<10)
            {
                lblResult.Text = result;
            }
            else if(step<20)
            {
                lblResult.Text = "";
            }
            else if(step >20)
            {
                step = 0;
                turn++;
            }


            if (turn >4)
            {
                myTimer.Stop();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            InitializeGUI();
            btnAdd.Enabled = true;
            btnSave.Enabled = true;
        }
    }
}
