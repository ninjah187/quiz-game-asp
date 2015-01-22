using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class RoundResult
    {
        public int Question1 { get; set; }
        public int Question2 { get; set; }
        public int Question3 { get; set; }

        public int Total
        {
            get { return Question1 + Question2 + Question3; }
        }

        public RoundResult()
        {
            Question1 = Question2 = Question3 = 0;
        }
    }
}