using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace LabirynthGame
{
    public class ScoreBoard
    {
        private OrderedMultiDictionary<int, Player> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, Player>(true);
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
                    var playersWithEqualScore = this.scoreBoard[score.Key];

                    foreach (var player in playersWithEqualScore)
                    {
                        Console.WriteLine("{0}. {1} --> {2}", counter, player.Name, score.Key);
                    }

                    counter++;
                }
            }

            Console.WriteLine();
        }

        public void UpdateScoreBoard(Player player)
        {
            if (this.scoreBoard.Count < 5)
            {
                this.scoreBoard.Add(player.Moves, player);
            }
            else
            {
                int worstScore = this.GetWorstScore();
                if (player.Moves <= worstScore)
                {
                    if (this.scoreBoard.ContainsKey(player.Moves) == false)
                    {
                        this.scoreBoard.Remove(worstScore);
                    }

                    this.scoreBoard.Add(player.Moves, player);
                }
            }
        }
    }
}
