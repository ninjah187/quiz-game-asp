using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class GameQuestion
    {
        private static readonly Random random = new Random();

        public int ID { get; set; }
        //public string Category { get; private set; }
        public string Content { get; private set; }
        public string[] Answers { get; private set; }

        public GameQuestion(Question question)
        {
            this.ID = question.ID;
            //this.Category = question.Category;
            this.Content = question.Content;

            this.Answers = new string[4];
            for (int i = 0; i < 4; i++)
            {
                Answers[i] = "";
            }

            AddAnswer(question.CorrectAnswer);
            AddAnswer(question.Answer1);
            AddAnswer(question.Answer2);
            AddAnswer(question.Answer3);
        }

        private void AddAnswer(string answer)
        {
            int i = -1;
            do
            {
                i = random.Next(4);
            } while (Answers[i] != "");
            Answers[i] = answer;
        }
    }
}