using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class Question
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Content { get; set; }
        public string CorrectAnswer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }

        public virtual Category Category { get; set; }
    }
}