using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace LabirynthGame
{
    public class ScoreBoard
    {
        private OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public int GetWorstScore()
        {
            int worstScore = this.scoreBoard.Keys.Last();

            return worstScore;
        }

        public void PrintScore()
        {
            int counter = 1;

            if (this.scoreBoard.Count == 0)
            {
                Console.WriteLine("The scoreboard is empty.");
            }
            else
            {
                foreach (var score in this.scoreBoard)
                {
                    var foundScore = this.scoreBoard[score.Key];

                    foreach (var equalScore in foundScore)
                    {
                        Console.WriteLine("{0}. {1} --> {2}", counter, equalScore, score.Key);
                    }

                    counter++;
                }
            }

            Console.WriteLine();
        }

        public void UpdateScoreBoard(int currentNumberOfMoves)
        {
            string userName = string.Empty;

            if (this.scoreBoard.Count < 5)
            {
                while (userName == string.Empty)
                {
                    Console.WriteLine("**Please put down your name:**");
                    userName = Console.ReadLine();
                }

                this.scoreBoard.Add(currentNumberOfMoves, userName);
            }
            else
            {
                int worstScore = this.GetWorstScore();
                if (currentNumberOfMoves <= worstScore)
                {
                    if (this.scoreBoard.ContainsKey(currentNumberOfMoves) == false)
                    {
                        this.scoreBoard.Remove(worstScore);
                    }

                    while (userName == string.Empty)
                    {
                        Console.WriteLine("**Please put down your name:**");
                        userName = Console.ReadLine();
                    }

                    this.scoreBoard.Add(currentNumberOfMoves, userName);
                }
            }
        }
    }
}
