using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizGameASP.Models
{
    public class Match
    {
        public const int ROUNDS_COUNT = 4;

        public int ID { get; set; }
        private static int id = 0;

        public List<Round> Rounds { get; set; }
        public Round CurrentRound { get; set; }
        private int roundCounter = 0;

        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public User CurrentPlayer { get; set; }

        public Match(User player1)
        {
            ID = ++Match.id;
            
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

        private void SwitchPlayers()
        {
            if (CurrentPlayer == Player1)
                CurrentPlayer = Player2;
            else CurrentPlayer = Player1;
        }
    }
}