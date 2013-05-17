namespace LabyrinthTests
{
    using System;
    using System.IO;
    using System.Text;
    using LabirynthGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void PrintScoreboardZeroPlayers()
        {
            ScoreBoard scoreBoard = new ScoreBoard();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                scoreBoard.PrintScore();

                string expected = "The scoreboard is empty.";
                var result = sw.ToString().TrimEnd();
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void PrintScoreboardTwoPlayers()
        {
            ScoreBoard scoreBoard = new ScoreBoard();

            Player player1 = new Player(2, 3);
            player1.Name = "Pesho";
            scoreBoard.UpdateScoreBoard(player1);

            Player player2 = new Player(3, 4);
            player2.Name = "Gosho";
            scoreBoard.UpdateScoreBoard(player2);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                scoreBoard.PrintScore();

                StringBuilder expected = new StringBuilder();
                expected.AppendLine("1. Pesho --> 0");
                expected.AppendLine("1. Gosho --> 0");

                StringBuilder actual = new StringBuilder();
                string[] splitLines = sw.ToString().TrimEnd().Split('\r', '\n');
                for (int i = 0; i < splitLines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(splitLines[i]))
                    {
                        actual.AppendLine(splitLines[i]);
                    }
                }

                Assert.AreEqual(expected.ToString(), actual.ToString());
            }
        }

        [TestMethod]
        public void PrintScoreboardMoreThanFivePlayersWithDifferentScores()
        {
            ScoreBoard scoreBoard = new ScoreBoard();

            Player player1 = new Player(2, 3);
            player1.Name = "Pesho";
            scoreBoard.UpdateScoreBoard(player1);

            Player player2 = new Player(3, 4);
            player2.MoveDown();
            player2.Name = "Gosho";
            scoreBoard.UpdateScoreBoard(player2);

            Player player3 = new Player(3, 4);
            player3.MoveDown();
            player3.MoveDown();
            player3.Name = "Gesho";
            scoreBoard.UpdateScoreBoard(player3);

            Player player4 = new Player(3, 4);
            player4.MoveDown();
            player4.MoveDown();
            player4.MoveDown();
            player4.Name = "Nesho";
            scoreBoard.UpdateScoreBoard(player4);

            Player player6 = new Player(3, 4);
            player6.MoveDown();
            player6.MoveDown();
            player6.MoveDown();
            player6.MoveDown();
            player6.MoveDown();
            player6.Name = "Tosho";
            scoreBoard.UpdateScoreBoard(player6);

            Player player5 = new Player(3, 4);
            player5.MoveDown();
            player5.MoveDown();
            player5.MoveDown();
            player5.MoveDown();
            player5.Name = "Losho";
            scoreBoard.UpdateScoreBoard(player5);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                scoreBoard.PrintScore();

                StringBuilder expected = new StringBuilder();
                expected.AppendLine("1. Pesho --> 0");
                expected.AppendLine("2. Gosho --> 1");
                expected.AppendLine("3. Gesho --> 2");
                expected.AppendLine("4. Nesho --> 3");
                expected.AppendLine("5. Losho --> 4");

                StringBuilder actual = new StringBuilder();
                string[] splitLines = sw.ToString().TrimEnd().Split('\r', '\n');
                for (int i = 0; i < splitLines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(splitLines[i]))
                    {
                        actual.AppendLine(splitLines[i]);
                    }
                }

                Assert.AreEqual(expected.ToString(), actual.ToString());
            }
        }
    }
}
