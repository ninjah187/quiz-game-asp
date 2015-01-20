using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class LoggedUser
    {
        public int LoggedUserID { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public LoggedUser(User user)
        {
            UserID = user.ID;
            User = user;
        }
    }
}