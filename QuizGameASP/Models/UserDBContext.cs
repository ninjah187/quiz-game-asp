using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class UserDBContext : DbContext
    {
        public UserDBContext()
            : base("name=UserDBContext")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}