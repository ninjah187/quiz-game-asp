using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class CategoryDBContext : DbContext
    {
        public CategoryDBContext()
            : base("name=CategoryDBContext")
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}