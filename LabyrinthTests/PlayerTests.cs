using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabirynthGame;

namespace LabyrinthTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void CreatePlayerNameTest()
        {
            Player player = new Player(3, 5);
            player.Name = "Pesho";
            string expected = "Pesho";
            Assert.AreEqual(expected, player.Name);
        }

        [TestMethod]
        public void CreatePlayerPositionTest()
        {
            Player player = new Player(4, 9);

            Assert.IsTrue(player.PositionX == 4 && player.PositionY == 9);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePlayerNullName()
        {
            Player player = new Player(5, 7);
            player.Name = null;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePlayerWhitespaceName()
        {
            Player player = new Player(5, 7);
            player.Name = "  ";
        }

        [TestMethod]
        public void TestMoveLeft()
        {
            Player player = new Player(5, 7);
            player.MoveLeft();

            Assert.IsTrue(player.PositionX == 4 && player.PositionY == 7);
        }

        [TestMethod]
        public void TestMoveUp()
        {
            Player player = new Player(5, 7);
            player.MoveUp();

            Assert.IsTrue(player.PositionX == 5 && player.PositionY == 6);
        }

        [TestMethod]
        public void TestMoveRight()
        {
            Player player = new Player(5, 7);
            player.MoveRight();

            Assert.IsTrue(player.PositionX == 6 && player.PositionY == 7);
        }

        [TestMethod]
        public void TestMoveDown()
        {
            Player player = new Player(5, 7);
            player.MoveDown();

            Assert.IsTrue(player.PositionX == 5 && player.PositionY == 8);
        }

        [TestMethod]
        public void CompareWithNull()
        {
            Player player = new Player(5, 7);
            int actual = player.CompareTo(null);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CompareWithOtherObject()
        {
            Player player = new Player(5, 7);
            ScoreBoard scoreBoard = new ScoreBoard();

            player.CompareTo(scoreBoard);
        }

        [TestMethod]
        public void CompareWithOtherPlayerWithSameMoves()
        {
            Player player = new Player(5, 7);
            Player player2 = new Player(2, 3);

            int actual = player.CompareTo(player2);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void CompareWithOtherPlayerWithLessMoves()
        {
            Player player = new Player(5, 7);
            player.MoveDown();

            Player player2 = new Player(2, 3);

            int actual = player.CompareTo(player2);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareWithOtherPlayerWithMoreMoves()
        {
            Player player = new Player(5, 7);

            Player player2 = new Player(2, 3);
            player2.MoveDown();

            int actual = player.CompareTo(player2);

            Assert.AreEqual(-1, actual);
        }
    }
}
