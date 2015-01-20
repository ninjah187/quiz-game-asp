using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizGameASP.Models;

namespace QuizGameASP.Controllers
{
    public class UsersController : Controller
    {
        private GameDBContext db = new GameDBContext();

        // GET: /Users/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginResult(string login, string password)
        {
            //User user = db.Users.Find(login);
            User user = db.Users.Single(u => u.Login == login);
            if (user != null)
            {
                if (user.Password == password)
                {
                    /*if (ModelState.IsValid)
                    {
                        LoggedUser loggedUser = new LoggedUser(user);
                        db.LoggedUsers.Add(loggedUser);
                        db.SaveChanges();
                        Session["User"] = loggedUser;
                    }*/
                    Session["User"] = user;
                    //if (HttpContext.Application["LoggedUsers"] == null)
                    //{
                    //    HttpContext.Application["LoggedUsers"] = new List<User>();
                    //}
                    ((List<User>)HttpContext.Application["LoggedUsers"]).Add(user);
                    ViewBag.Message = "Zalogowano.";
                }
                else
                {
                    ViewBag.Message = "Niepoprawne hasło.";
                }
            }
            else
            {
                ViewBag.Message = "Niepoprawna nazwa użytkownika.";
            }
            return RedirectToAction("Menu", "Game");
        }

        public ActionResult Logout()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                ((List<User>)HttpContext.Application["LoggedUsers"]).Remove(user);

                var queue = (List<Match>) HttpContext.Application["MatchmakingQueue"];
                //Deleting all matches of user from matchmaking queue (TODO auto-win to enemy)
                var toRemove = ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Where(
                    match => match.Player1.ID == user.ID || match.Player2.ID == user.ID
                    ).ToList();

                foreach (Match match in toRemove)
                {
                    ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Remove(match);
                }

                Session["User"] = null;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoggedUsers()
        {
            var loggedUsers = (List<User>)HttpContext.Application["LoggedUsers"];
            //if (loggedUsers == null)
            //{
            //    loggedUsers = new List<User>();
            //}
            return View(loggedUsers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
