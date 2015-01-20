using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class Round
    {
        public int ID { get; set; }
        //public string Category { get; set; }
        public string CategoryName { get; set; }
        public List<GameQuestion> Questions { get; set; }

        public Round(int number)
        {
            ID = number;
        }

        public void AddQuestions(IEnumerable<Question> questions)
        {
            Questions = new List<GameQuestion>();
            foreach (Question q in questions)
            {
                Questions.Add(new GameQuestion(q));
            }
        }
    }
}