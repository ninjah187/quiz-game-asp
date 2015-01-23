using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using QuizGameASP.Models;

namespace QuizGameASP.Controllers
{
    //Occurs when player doesnt belong to match he's trying to play
    public class WrongMatchException : Exception
    {
        
    }

    public class GameController : Controller
    {
        private static readonly Random random = new Random();

        private GameDBContext dbContext = new GameDBContext();

        public ActionResult Menu()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View((User)Session["User"]);
            }
        }

        public ActionResult Match(int? id)
        {
            //wybierz odpowiedni mecz
            Match match = ((List<Match>)HttpContext.Application["MatchmakingQueue"]).Single(
                m => m.ID == id
                );
            User user = (User)Session["User"];
            if (user == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                if (match.Player1.ID != user.ID &&
                    match.Player2.ID != user.ID)
                {
                    throw new WrongMatchException();
                }
                else
                {
                    return View(match);
                }
            }
        }

        public ActionResult CategoryChoose()
        {
            var model = new List<Category>();
            var categories = dbContext.Categories.ToList();
            for (int i = 0; i < 3; i++)
            {
                int x = random.Next(categories.Count);
                var category = categories[x];
                categories.RemoveAt(x);
                model.Add(category);
            }

            return PartialView(model);
        }

        public ActionResult Round(int matchID, string categoryName)
        {
            var match = ((List<Match>) HttpContext.Application["MatchmakingQueue"]).Single(
                m => m.ID == matchID
                );
            var category = dbContext.Categories.Single(c => c.Name == categoryName);

            match.CurrentRound.CategoryName = categoryName;

            var questions = dbContext.Questions.Where(q => q.Category.ID == category.ID).ToList(); //categoryID ? vs Category.ID
            var roundQuestions = new List<Question>();
            for (int i = 0; i < 3; i++)
            {
                int x = random.Next(questions.Count);
                var question = questions[x];
                roundQuestions.Add(question);
                questions.RemoveAt(x);
            }

            match.CurrentRound.AddQuestions(roundQuestions);

            return View(match);
        }

        public string CheckAnswer(int questionID, string answer)
        {
            var question = dbContext.Questions.Find(questionID);
            if (question.CorrectAnswer == answer)
            {
                return "ok";
            }
            return "wrong";
        }

        public ActionResult RoundResult(int matchID, string answerPoints)
        {
            var match = ((List<Match>) HttpContext.Application["MatchmakingQueue"]).Single(m => m.ID == matchID);

            string[] answerResultStrings = answerPoints.Split(',');
            var roundResult = new RoundResult()
            {
                Question1 = Int32.Parse(answerResultStrings[0]),
                Question2 = Int32.Parse(answerResultStrings[1]),
                Question3 = Int32.Parse(answerResultStrings[2]),
            };

            match.CurrentRound.Results.Add(((User)Session["User"]).ID, roundResult);

            int result = roundResult.Total;

            //TESTING
            //match.NextRoundWithTestBot();

            //IMPORTANT! Occurs ONLY when Player1 is me
            if (match.CurrentPlayer.ID == match.Player1.ID)
            {
                var categories = dbContext.Categories.ToList();
                int x = random.Next(categories.Count);
                var category = categories[x];

                var questions = dbContext.Questions.Where(q => q.CategoryID == category.ID).ToList();
                var roundQuestions = new List<Question>();
                for (int i = 0; i < 3; i++)
                {
                    int y = random.Next(questions.Count);
                    var question = questions[y];
                    roundQuestions.Add(question);
                    questions.RemoveAt(y);
                }

                match.NextRoundWithTestBot(roundQuestions);
            }
            else
            {
                match.NextRoundWithTestBot();
            }

            //return Redirect("/Game/Match?matchID=" + match.ID.ToString());
            return RedirectToAction("Match", new {id = match.ID});
            //return PartialView(match);
        }

        /*private static readonly Random random = new Random();

        private CategoryDBContext catDB = new CategoryDBContext();
        private QuestionDBContext questDB = new QuestionDBContext();
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Start()
        {
            return PartialView();
        }

        public ActionResult CategoryChoose()
        {
            var chooses = new List<Category>();
            var categories = catDB.Categories.ToList();

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(categories.Count);
                chooses.Add(categories[index]);
                categories.RemoveAt(index);
            }
            
            return PartialView(chooses);
        }

        public ActionResult Questions(string category)
        {
            var chooses = new List<GameQuestion>();
            //where zwraca wszystkie
            //first pierwszy
            //single jedno
            /*var questionsResult = questDB.Questions.Where(q => string.Equals(q.Category, category));
            var questions = questionsResult.ToList();*/
            
            /*var questions = new List<Question>();
            foreach (var question in questDB.Questions)
            {
                //if(string.Equals(question.Category, category))
                if(question.Category == category)
                {
                    questions.Add(question);
                }
            }*/

        /*    var questions = questDB.Questions.Where(q => q.Category == category).ToList();

            for (int i = 0; i < 3; i++)
            {
                int index = random.Next(questions.Count);
                chooses.Add(new GameQuestion(questions[index]));
                questions.RemoveAt(index);
            }

            return PartialView(chooses);
        }

        public string CheckAnswer(int id, string answer)
        {
            Question question = questDB.Questions.Find(id);
            if (answer == question.CorrectAnswer)
                return "ok";
            else return "wrong";
        }

        public ActionResult Result(string result, string answerString)
        {
            ViewBag.Result = result;
            string[] answers = answerString.Split(';');
            ViewBag.Question1 = answers[0];
            ViewBag.Question2 = answers[1];
            ViewBag.Question3 = answers[2];
            return View();
        }

	*/}
}