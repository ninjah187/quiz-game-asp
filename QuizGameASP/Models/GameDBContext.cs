using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class GameDBContext : DbContext
    {
        public GameDBContext()
            : base("name=GameDBContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LoggedUser> LoggedUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}