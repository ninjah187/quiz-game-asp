using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class QuestionDBContext : DbContext
    {
        public QuestionDBContext()
            : base("name=QuestionDBContext")
        {
        }

        public DbSet<Question> Questions { get; set; }
    }
}