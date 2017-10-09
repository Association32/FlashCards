using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FlashCards
{
    class CardManager
    {
        private List<Card> cards = new List<Card>();
        private List<string> falseAnswer = new List<string>();
        int firstFalse;
        int secFalse;
        int rollQuestion;
        int prevQuestion;
        List<int> correctAnswers =  new List<int>();
        int points;

        public CardManager()
        {
            //this.cards = cards;
        }

        /// <summary>
        /// Getting questions from external file.
        /// </summary>
        public void GetQuestions(string address)
        {
            correctAnswers = new List<int>();
            string[] importData = File.ReadAllLines(address);
            falseAnswer = new List<string>();
            cards = new List<Card>();
            for (int i = 0; i<importData.Length; i++)
            {
                string[] question = importData[i].Split(';');
                if(question.Length>3)
                {
                    string[] thisSecFalse = new string[question.Length - 2];
                    for (int ii = 2; ii < question.Length; ii++)
                    {
                        thisSecFalse[ii - 2] = question[ii];
                    }
                    cards.Add(new Card(question[0], question[1], 0, 0, true, thisSecFalse));
                    falseAnswer.Add(question[1]);

                    string[] thisFalse = new string[question.Length - 2];
                    for(int ii = 2; ii<question.Length;ii++)
                    {
                        thisFalse[ii - 2] = question[ii];
                    } 
                    cards.Add(new Card(question[0], question[1], 0, 0, true, thisFalse));
                    falseAnswer.Add(question[1]);
                }
                else
                {
                    cards.Add(new Card(question[0], question[1], 0, 0, false));
                    falseAnswer.Add(question[1]);
                }
                
            }
            points = 0;
            NewQuestion();
        }

        public void NewQuestion()
        {
            if(CheckIfLast())
            {
                prevQuestion = -1;
            }
            else
            {
                prevQuestion = rollQuestion;
            }
            
            Roll();
        }

        private void Roll()
        {
            SetRollQuestion();
            RollFirstFalse();
            RollSecFalse();
        }

        public string PrintTrue()
        {
            string text ="";
           
            text = cards[rollQuestion].PrintAnswer();
            return text;
        }

        public string PrintQuestion()
        {
            string text = "";
            
            text += cards[rollQuestion].PrintQuestion();

            return text;
        }

        public string PrintFirstFalse()
        {
            string text = "";
            
            if(cards[rollQuestion].HasFalse)
            {
                cards[rollQuestion].SetFalseOnes();
                text = cards[rollQuestion].GetFirstFalse();
            }
            else
            {
                text = falseAnswer[firstFalse];
            }
            return text;
        }

        public string PrintSectFalse()
        {
            string text = "";
            if (cards[rollQuestion].HasFalse)
            {
                text = cards[rollQuestion].GetSecFalse();
            }
            else
            {
                text = falseAnswer[secFalse];
            }
            
            return text;
        }


        private void RollFirstFalse()
        {
            Random myRandom = new Random();
            int roll;
            do
            {
                roll = myRandom.Next(0, falseAnswer.Count);
                //System.Windows.Forms.MessageBox.Show("1 "+roll.ToString());
            } while (roll == rollQuestion);

            firstFalse = roll;

        }

        private void RollSecFalse()
        {
            Random myRandom = new Random();
            int roll;
            do
            {
                roll = myRandom.Next(0, falseAnswer.Count);
                //System.Windows.Forms.MessageBox.Show("2 "+roll.ToString());
            } while ((roll==firstFalse)|| (roll == rollQuestion));
            secFalse = roll;
        }

        public void SetRollQuestion()
        {
            
            int roll;
            Random myRandom = new Random();
            do
            {
                roll = myRandom.Next(0, cards.Count);
            } while ((roll == prevQuestion)||(correctAnswers.Contains(roll)));


            rollQuestion = roll;
            
        }

        public int GetPoints()
        {
            return points;
        }

        public bool CheckOK()
        {
            bool isOK =  correctAnswers.Count<cards.Count;


            return isOK;
        }

        public void AddQuestion(string question, string answer, int nrRight, int nrFalse)
        {
            cards.Add(new Card(question, answer, 0, 0, false));
            falseAnswer.Add(answer);
        }



        public void AddQuestion(string question, string answer, int nrRight, int nrFalse, params string[] falseAnswers)
        {
            cards.Add(new Card(question, answer, 0, 0, true, falseAnswers));
            falseAnswer.Add(answer);
        }

        public string SaveQuestions()
        {
            string dataOut="";
            for (int i =0; i<cards.Count; i++)
            {
                dataOut += cards[i].Question;
                dataOut += ";";
                dataOut += cards[i].Answer;
                

                if(cards[i].HasFalse)
                {
                    dataOut += cards[i].PrintFalseToSave();
                }

                dataOut += Environment.NewLine;
            }


            return dataOut;
        }

        public bool ListLengthOK()
        {
            bool isOK = cards.Count >= 3;

            return isOK;
        }

        public void WrongAnswer()
        {
            cards[rollQuestion].NrFalse++;
            points--;
        }

        public void RightAnswer()
        {

            cards[rollQuestion].NrTrue++;
            

            //correctAnswers.Add(rollQuestion);

            if (cards[rollQuestion].NrTrue- cards[rollQuestion].NrFalse>0)
            {
                //System.Windows.Forms.MessageBox.Show(cards[rollQuestion].NrTrue.ToString()+" "+ cards[rollQuestion].NrFalse.ToString());
                correctAnswers.Add(rollQuestion);
            }

            if(cards[rollQuestion].NrFalse<=0)
            {
                points++;
            }
            
        }

        public int GetCardsLength()
        {
            int length = cards.Count;

            return length;
        }

        private bool CheckIfLast()
        {
            bool isLast = true;
            int counter = 0;

            for(int i = 0; i<cards.Count;i++)
            {
                if(!correctAnswers.Contains(i))
                {
                    counter++;
                }
            }
            isLast = counter == 1;

            return isLast;
        }
    }
}
