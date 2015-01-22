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

        //dictionary with pairs <Key, Value> where key is user ID and value is RoundResult of that user
        //every player sends his result in AJAX after end of each round (see Match.cshtml, Round.cshtml and RoundResult.cshtml)
        public Dictionary<int, RoundResult> Results { get; private set; } // Dictionary<userID, userResult>

        public Round(int number)
        {
            ID = number;

            Questions = null;

            Results = new Dictionary<int, RoundResult>();
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