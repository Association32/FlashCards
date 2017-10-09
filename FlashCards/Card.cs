using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards
{
    class Card
    {
        private string question;
        private string answer;
        private int nrTrue;
        private int nrFalse;
        private const int maxLineLength = 20;
        private bool hasFalse;
        string[] falseAnswers;
        int firstFalse;
        int secFalse;


        public string Answer
        {
            get
            {
                return answer;
            }

        }

        public string Question
        {
            get
            {
                return question;
            }
        }

        public bool HasFalse
        {
            get
            {
                return hasFalse;
            }

            set
            {
                hasFalse = value;
            }
        }

        public int NrFalse
        {
            get
            {
                return nrFalse;
            }

            set
            {
                nrFalse = value;
            }
        }

        public int NrTrue
        {
            get
            {
                return nrTrue;
            }

            set
            {
                nrTrue = value;
            }
        }

        

        public Card(string question, string answer, int nrTrue, int nrFalse, bool hasFalse)
        {
            this.question = question;
            this.answer = answer;
            this.nrFalse = nrFalse;
            this.nrTrue = nrTrue;
            this.HasFalse = hasFalse;
        }

        public Card(string question, string answer, int nrTrue, int nrFalse, bool hasFalse, params string[] falseAnswers)
        {
            this.question = question;
            this.answer = answer;
            this.nrFalse = nrFalse;
            this.nrTrue = nrTrue;
            this.HasFalse = hasFalse;
            this.falseAnswers = falseAnswers;
        }

       

        public string PrintAnswer()
        {
            string currAnswer = "";
            int counter = 0;

            for (int i = 0; i<Answer.Length; i++)
            {
                if ((counter >= maxLineLength)&& (Answer[i] == ' '))
                {
                    currAnswer += Environment.NewLine;
                    counter = 0;
                }
                else
                {
                    currAnswer += Answer[i];
                    counter++;
                }
            }

            return currAnswer;
        }

        

        public string PrintQuestion()
        {
            string currQuestion = "";
            int counter = 0;

            for (int i = 0; i < Question.Length; i++)
            {
                if ((counter >= maxLineLength) && (Question[i] == ' '))
                {
                    currQuestion += Environment.NewLine;
                    counter = 0;
                }
                else
                {
                    currQuestion += Question[i];
                    counter++;
                }
            }
            return currQuestion;
        }

        private void SetFirstFalse()
        {
            Random myRandom = new Random();
            this.firstFalse = myRandom.Next(0, falseAnswers.Length);
        }

        private void SetSecFalse()
        {
            int roll;
            do
            {
                Random myRandom = new Random();
                 roll= myRandom.Next(0, falseAnswers.Length);
            } while (roll == this.firstFalse);
            this.secFalse = roll;
        }
        
        public void SetFalseOnes()
        {
            SetFirstFalse();
            SetSecFalse();
        } 

        public string GetFirstFalse()
        {
            return falseAnswers[firstFalse];
        }

        public string GetSecFalse()
        {
            return falseAnswers[secFalse];
        }

        public string PrintFalseToSave()
        {
            string output = "";
            for (int i = 0; i<falseAnswers.Length; i++)
            {
                output += ";";
                output += falseAnswers[i];
            }
            return output;
        }
    }
}
