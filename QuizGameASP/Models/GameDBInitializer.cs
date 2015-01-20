using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QuizGameASP.Models
{
    public class GameDBInitializer : DropCreateDatabaseIfModelChanges<GameDBContext>
    {
        //To use initializer, there's need to add context in Web.config (in <entityFramework>)
        protected override void Seed(GameDBContext context)
        {
            var categories = new List<Category>
            {
                new Category() { Name = "Historia" },
                new Category() { Name = "Geografia" },
                new Category() { Name = "Matematyka" },
                new Category() { Name = "Muzyka" },
                new Category() { Name = "Film" },
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var users = new List<User>
            {
                new User() { Login = "Ninja", Password = "pwd" },
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var questions = new List<Question>
            {
                new Question()
                {
                    CategoryID = 1, 
                    Content = "Kto jako pierwszy stanął na Księżycu?",
                    CorrectAnswer = "Neil Armstrong",
                    Answer1 = "Louis Armstrong",
                    Answer2 = "Jurij Gagarin",
                    Answer3 = "Lance Armstrong",
                },
                new Question()
                {
                    CategoryID = 1, 
                    Content = "Kto był pierwszym władcą Polski?",
                    CorrectAnswer = "Mieszko I",
                    Answer1 = "Bolesław Chrobry",
                    Answer2 = "Piast",
                    Answer3 = "Popiel",
                },
                new Question()
                {
                    CategoryID = 3, 
                    Content = "Ile wynosi pierwiastek sześcienny z 27?",
                    CorrectAnswer = "3",
                    Answer1 = "9",
                    Answer2 = "2",
                    Answer3 = "7",
                },
                new Question()
                {
                    CategoryID = 1, 
                    Content = "W którym roku miał miejsce I rozbiór Polski?",
                    CorrectAnswer = "1772",
                    Answer1 = "1762",
                    Answer2 = "1795",
                    Answer3 = "1788",
                },
                new Question()
                {
                    CategoryID = 1, 
                    Content = "Jak nazywała się żona Stefana I Batorego?",
                    CorrectAnswer = "Bona",
                    Answer1 = "Fiona",
                    Answer2 = "Dona",
                    Answer3 = "Maria",
                },
                new Question()
                {
                    CategoryID = 1, 
                    Content = "Inflanty obejmują tereny dzisiejszej:",
                    CorrectAnswer = "Estonii i Łotwy",
                    Answer1 = "Litwy i Rosji",
                    Answer2 = "Łotwy i Litwy",
                    Answer3 = "Estonii",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "Państwo o największej powierzchni na świecie to:",
                    CorrectAnswer = "Rosja",
                    Answer1 = "Kanada",
                    Answer2 = "Chiny",
                    Answer3 = "Stany Zjednoczone",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "Najgłębsze jezioro świata to:",
                    CorrectAnswer = "Bajkał",
                    Answer1 = "Morze Kaspijskie",
                    Answer2 = "Wostok",
                    Answer3 = "Tanganika",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "Ile powierzchni Ziemi zajmuje woda?",
                    CorrectAnswer = "ok, 71%",
                    Answer1 = "ok. 66%",
                    Answer2 = "ok.75%",
                    Answer3 = "ok. 80%",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "Wyspa o największej powierzchni na świecie to:",
                    CorrectAnswer = "Grenlandia",
                    Answer1 = "Borneo",
                    Answer2 = "Madagaskar",
                    Answer3 = "Nowa Gwinea",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "Państwo o najmniejszej powierzchni to:",
                    CorrectAnswer = "Watykan",
                    Answer1 = "Monako",
                    Answer2 = "San Marino",
                    Answer3 = "Liechtenstein",
                },
                new Question()
                {
                    CategoryID = 3, 
                    Content = "Ile krawędzi posiada sześcian?",
                    CorrectAnswer = "12",
                    Answer1 = "6",
                    Answer2 = "8",
                    Answer3 = "10",
                },
                new Question()
                {
                    CategoryID = 3, 
                    Content = "Liczba PI wynosi w przybliżeniu:",
                    CorrectAnswer = "3,14",
                    Answer1 = "3",
                    Answer2 = "3,56",
                    Answer3 = "3,41",
                },
                new Question()
                {
                    CategoryID = 3, 
                    Content = "Prostokąt może nie być:",
                    CorrectAnswer = "Kwadratem",
                    Answer1 = "Trapezem",
                    Answer2 = "Równoległobokiem",
                    Answer3 = "Rombem",
                },
                new Question()
                {
                    CategoryID = 4, 
                    Content = "Albumem zespołu Pink Floyd nie jest:",
                    CorrectAnswer = "Otherside",
                    Answer1 = "Dark Side Of The Moon",
                    Answer2 = "The Wall",
                    Answer3 = "Animals",
                },
                new Question()
                {
                    CategoryID = 4, 
                    Content = "Autorem utworu \"Dla Elizy\" jest:",
                    CorrectAnswer = "Ludwig van Beethoven",
                    Answer1 = "Amadeus Mozart",
                    Answer2 = "Antonio Vivaldi",
                    Answer3 = "Richard Wagner",
                },
                new Question()
                {
                    CategoryID = 4, 
                    Content = "Z ilu dźwięków składa się oktawa?",
                    CorrectAnswer = "8",
                    Answer1 = "12",
                    Answer2 = "6",
                    Answer3 = "4",
                },
                new Question()
                {
                    CategoryID = 4, 
                    Content = "Dźwięk \"mi\" to inaczej:",
                    CorrectAnswer = "E",
                    Answer1 = "F",
                    Answer2 = "C",
                    Answer3 = "G",
                },
                new Question()
                {
                    CategoryID = 5, 
                    Content = "Twórca filmu \"Wściekłe psy\" to:",
                    CorrectAnswer = "Quentin Tarantino",
                    Answer1 = "Steven Spielberg",
                    Answer2 = "Patryk Vega",
                    Answer3 = "Ridley Scott",
                },
                new Question()
                {
                    CategoryID = 5, 
                    Content = "Złote maliny przyznaje się filmom:",
                    CorrectAnswer = "Najgorszym",
                    Answer1 = "Najdłuższym",
                    Answer2 = "Najbardziej kasowym",
                    Answer3 = "Najlepszym",
                },
                new Question()
                {
                    CategoryID = 5, 
                    Content = "Główny odtwórca roli Tony'ego Montany nazywa się:",
                    CorrectAnswer = "Al Pacino",
                    Answer1 = "Al Capone",
                    Answer2 = "Robert De Niro",
                    Answer3 = "Sylvester Stallone",
                },
                new Question()
                {
                    CategoryID = 5, 
                    Content = "Z ilu członków składała się tytułowa Drużyna Pierścieni z filmu \"Władca Pierścieni\"?",
                    CorrectAnswer = "9",
                    Answer1 = "8",
                    Answer2 = "12",
                    Answer3 = "5",
                },
                new Question()
                {
                    CategoryID = 2, 
                    Content = "W którym polskim parku narodowym leżą największe wydmy?",
                    CorrectAnswer = "W Słowińskim",
                    Answer1 = "W Biebrzańskim",
                    Answer2 = "W Wolińskim",
                    Answer3 = "W Wielkopolskim",
                },
                new Question()
                {
                    CategoryID = 3, 
                    Content = "Wykresem której funkcji jest parabola?",
                    CorrectAnswer = "Kwadratowej",
                    Answer1 = "Liniowej",
                    Answer2 = "Sześciennej",
                    Answer3 = "Hiperbolicznej",
                },
            };
            /*foreach (Question q in questions)
            {
                context.Questions.Add(q);
            }*/
            questions.ForEach(q => context.Questions.Add(q));
            context.SaveChanges();
        }
    }
}