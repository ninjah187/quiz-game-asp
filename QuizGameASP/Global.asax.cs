using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using QuizGameASP.Models;

namespace QuizGameASP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer botTimer;
        private TestBot testBot;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Application["LoggedUsers"] = new List<User>();
            var loggedUsers = (List<User>)Application["LoggedUsers"];
            loggedUsers.Add(testBot = new TestBot());

            Application["MatchmakingQueue"] = new List<Match>();
            var queue = (List<Match>)Application["MatchmakingQueue"];
            //queue.Add(new Match(testBot));

            //uruchom timer, który sprawdza, czy jest jakis wolny mecz, a jeśli jest dołącza do niego bota
            botTimer = new Timer(
                joinBotToMatch,
                null,
                new TimeSpan(0, 0, 0, 0, 10),
                new TimeSpan(0, 0, 0, 1, 0)
                );
        }

        private void joinBotToMatch(object state)
        {
            var emptyMatches = ((List<Match>) Application["MatchmakingQueue"]).Where(
                match => match.Player2 == null
                ).ToList();

            foreach (var match in emptyMatches)
            {
                match.Player2 = testBot;
            }
        }

        public void Session_End()
        {
            //Application.Lock();
            User user = (User)Session["User"];
            if (user != null)
            {
                var loggedUsers = (List<User>) Application["LoggedUsers"];
                loggedUsers.Remove(user);

                List<Match> toRemove = new List<Match>();
                var queue = (List<Match>) Application["MatchmakingQueue"];
                toRemove = queue.Where(m => m.Player1.ID == user.ID || m.Player2.ID == user.ID).ToList();

                foreach (Match item in toRemove)
                {
                    queue.Remove(item);
                }

                user = null;
            }
            //Application.UnLock();
        }
    }
}
