using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QuizGameASP.Models;

namespace QuizGameASP.Controllers
{
    public class MatchmakingController : Controller
    {
        private GameDBContext db = new GameDBContext();

        //
        // GET: /Matchmaking/
        private ActionResult Index()
        {
            return View();
        }

        /*public ActionResult JoinQueue(int? id)
        {
            User user = ((List<User>)HttpContext.Application["LoggedUsers"]).Single(u => u.ID == id);
            if (user != null)
            {
                Match joinMatch = ((List<Match>)HttpContext.Application["MatchmakinQueue"]).First(
                    m => m.Player2 == null
                );
                if (joinMatch == null)
                {
                    joinMatch = new Match(user);
                    ((List<Match>)HttpContext.Application["MatchmakinQueue"]).Add(joinMatch);
                }
                else
                {
                    joinMatch.Player2 = user;
                }
            }
            return RedirectToAction("Menu", "Game");
        }*/
        
        public ActionResult JoinQueue()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                //get first match with empty place
                Match joinMatch = null;
                /*joinMatch = ((List<Match>)HttpContext.Application["MatchmakingQueue"]).First(
                        m => m.Player2 == null
                    );*/
                try
                {
                    joinMatch = ((List<Match>)HttpContext.Application["MatchmakingQueue"]).First(
                        m => m.Player2 == null
                    );
                    //if there's such match, join player to it
                    joinMatch.Player2 = user;
                }
                catch (InvalidOperationException exc) //exception prawdopodobnie przez MatchmakinQueue, wróć do ifów
                {
                //    //if not, create new match and add it to matchmaking queue
                    joinMatch = new Match(user);
                    ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Add(joinMatch);
                }
                //if there's no such match, create new and add id to matchmaking queue
                //if (joinMatch == null)
                //{
                //    joinMatch = new Match(user);
                //    ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Add(joinMatch);
                //}
                ////if there is, join player to such match
                //else
                //{
                //    joinMatch.Player2 = user;
                //}
            }
            else
            {
                //throw new ArgumentNullException();
                RedirectToAction("Login", "Users");
            }

            return RedirectToAction("Menu", "Game");
        }

        public ActionResult UserQueue()
        {
            User user = (User)Session["User"];
            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                IEnumerable<Match> userMatches = ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Where(
                    (Match match) => match.Player1.ID == user.ID || match.Player2.ID == user.ID
                );
                return PartialView(userMatches);
            }
        }

        /*public delegate void myFuncDelegate(int arg);
        myFuncDelegate del = new myFuncDelegate((int x) => { x++; x--; });*/

        public ActionResult ShowQueue()
        {
            return View((List<Match>)HttpContext.Application["MatchmakingQueue"]);
        }
	}
}