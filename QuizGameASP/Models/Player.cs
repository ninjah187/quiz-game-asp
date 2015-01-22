using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace QuizGameASP.Models
{
    public class Player
    {
        public enum State { Playing, ChoosingCategory, Waiting }

        public User User { get; private set; }

        public Player(User user)
        {
            User = user;
        }

        //to co user + wynik
    }
}