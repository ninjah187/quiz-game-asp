using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class MatchDBContext : DbContext
    {
        public MatchDBContext()
            : base("name=MatchDBContext")
        {
        }
        
        public DbSet<Match> Matches { get; set; }
    }
}