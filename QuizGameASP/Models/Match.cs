using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace QuizGameASP.Models
{
    public class Match
    {
        //for testing
        private static readonly Random random = new Random();

        public const int ROUNDS_COUNT = 4;

        public int ID { get; set; }
        private static int _id = 0;

        public List<Round> Rounds { get; set; }
        public Round CurrentRound { get; set; }
        private int roundCounter = 0;

        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public User CurrentPlayer { get; set; }
        //CurrentPlayer is player who starts CurrentRound by choosing category

        public Match(User player1)
        {
            ID = ++Match._id;
            
            Player1 = player1;
            CurrentPlayer = Player1;
            Player2 = null;

            Rounds = new List<Round>(ROUNDS_COUNT);
            for (int i = 0; i < ROUNDS_COUNT; i++)
            {
                Rounds.Add(new Round(i + 1));
            }
            CurrentRound = Rounds[roundCounter];
        }

        //NextRoundWithTestBot() is invoked when player chooses categories
        //NextRoundWithTestBot() is invoked when bot chooses categories
        //invoked in /Game/RoundResult/
        public void NextRoundWithTestBot()
        {
            //generate and add random results for bot
            var testBot = Player1.GetType() == typeof (TestBot) ? Player1 : Player2;
            var results = new RoundResult()
            {
                Question1 = random.Next(2),
                Question2 = random.Next(2),
                Question3 = random.Next(2),
            };
            CurrentRound.Results.Add(testBot.ID, results);

            //change CurrentRound to next round
            NextRound();
            //previous round ended

            //solve bot's part of new round
            /*results = new RoundResult()
            {
                Question1 = random.Next(2),
                Question2 = random.Next(2),
                Question3 = random.Next(2),
            };

            SwitchPlayers();*/
        }

        public void NextRoundWithTestBot(IEnumerable<Question> questions)
        {
            NextRoundWithTestBot();

            CurrentRound.CategoryName = "WORK IN PROGRESS";
            CurrentRound.AddQuestions(questions);

            var testBot = Player1.GetType() == typeof(TestBot) ? Player1 : Player2;
            var results = new RoundResult()
            {
                Question1 = random.Next(2),
                Question2 = random.Next(2),
                Question3 = random.Next(2),
            };
            CurrentRound.Results.Add(testBot.ID, results);

            SwitchPlayers();
        }

        //NextRound() should be invoked when both players have ended their rounds and have their results updated
        public void NextRound()
        {
            CurrentRound = Rounds[++roundCounter];
            SwitchPlayers();
        }

        private void SwitchPlayers()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }
    }
}