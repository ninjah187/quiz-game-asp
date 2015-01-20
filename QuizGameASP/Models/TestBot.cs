using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class TestBot : User
    {
        public TestBot()
        {
            base.ID = -1;
            base.Login = "TestBot";
            base.Password = "pwd";
        }
    }
}