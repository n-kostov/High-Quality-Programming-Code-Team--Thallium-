namespace LabyrinthTests
{
    using System;
    using System.IO;
    using System.Text;
    using LabirynthGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LabyrinthTests
    {
        [TestMethod]
        public void CreateLabyrinthTest()
        {
            Labyrinth labyrinth = new Labyrinth(1);
            Assert.AreEqual(1, labyrinth.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateLabyrinthZeroDimentions()
        {
            Labyrinth lab = new Labyrinth(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateLabyrinthNegativeDimentions()
        {
            Labyrinth lab = new Labyrinth(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AccessLabyrinthTest()
        {
            Labyrinth labyrinth = new Labyrinth(5);
            labyrinth[labyrinth.Size, labyrinth.Size] = '2';
        }

        [TestMethod]
        public void PrintLabyrithTest()
        {
            char[,] staticLabyrinth = 
            { 
                                      { '-', '-', 'X', 'X', 'X', 'X', '-' }, 
                                      { '-', 'X', '-', '-', '-', '-', 'X' }, 
                                      { '-', 'X', '-', 'X', 'X', '-', 'X' }, 
                                      { '-', 'X', '-', '*', 'X', '-', 'X' }, 
                                      { '-', 'X', '-', 'X', '-', '-', '-' }, 
                                      { '-', 'X', '-', '-', '-', 'X', 'X' }, 
                                      { 'X', '-', 'X', '-', '-', 'X', 'X' } 
                                      };

            Labyrinth labyrinth = new Labyrinth(7);
            for (int i = 0; i < labyrinth.Size; i++)
            {
                for (int j = 0; j < labyrinth.Size; j++)
                {
                    labyrinth[i, j] = staticLabyrinth[i, j];
                }
            }

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                labyrinth.PrintLabirynth();

                StringBuilder actual = new StringBuilder();

                string[] splitLines = sw.ToString().TrimEnd().Split('\r', '\n');
                for (int i = 0; i < splitLines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(splitLines[i]))
                    {
                        actual.AppendLine(splitLines[i]);
                    }
                }

                StringBuilder expected = new StringBuilder();

                for (int i = 0; i < staticLabyrinth.GetLength(0); i++)
                {
                    for (int j = 0; j < staticLabyrinth.GetLength(1); j++)
                    {
                        expected.AppendFormat("{0,2}", staticLabyrinth[i, j].ToString());
                    }

                    expected.AppendLine();
                }

                Assert.AreEqual(expected.ToString(), actual.ToString());
            }
        }
    }
}
