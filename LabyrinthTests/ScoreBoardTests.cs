using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabirynthGame;
using System.IO;

namespace LabyrinthTests
{
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void AddPlayerTest()
        {
            Player player = new Player(4, 7);
            ScoreBoard scoreBoard = new ScoreBoard();

            scoreBoard.UpdateScoreBoard(player);

            

            Assert.AreEqual(1, 0);
        }

        [TestMethod]
        public void KeepOnlyTopPlayersTest()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.UpdateScoreBoard(new Player(4, 7));
            scoreBoard.UpdateScoreBoard(new Player(4, 3));
            scoreBoard.UpdateScoreBoard(new Player(5, 3));
            scoreBoard.UpdateScoreBoard(new Player( 7, 1));
            scoreBoard.UpdateScoreBoard(new Player(9, 3));

            //Verify that only the top players are kept
            //Error: No access to players list due to its protection level

            Assert.AreEqual(1, 0);
        }

        [TestMethod]
        public void PrintScoreboardTest()
        {
            Player player = new Player(4, 7);
            ScoreBoard scoreBoard = new ScoreBoard();
            scoreBoard.UpdateScoreBoard(player);

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(@"Scoreboard:
1. Name --> 0 moves"))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    // Act
                    scoreBoard.PrintScore();

                    // Assert
                    var result = sw.ToString();
                    Assert.IsFalse(result.Equals(@"Scoreboard:
1. Name --> 0 moves"));
                }
            }

        }
    }
}
