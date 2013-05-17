using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LabirynthGame;
using System.IO;
using System.Text;

namespace LabyrinthTests
{
    [TestClass]
    public class EngineTests
    {
        char[,] staticLabyrinth = {     {'X', '-', '-', '-', '-', '-', 'X'},
                                        {'X', '-', 'X', '-', '-', '-', '-'},
                                        {'X', 'X', '-', '-', '-', 'X', '-'},
                                        {'X', '-', 'X', '*', 'X', '-', '-'},
                                        {'-', '-', '-', '-', 'X', '-', '-'},
                                        {'-', '-', 'X', '-', '-', 'X', 'X'},
                                        {'-', '-', 'X', '-', '-', 'X', '-'}};

        Labyrinth labyrinth = new Labyrinth(7);

        private void InitializeData()
        {
            for (int i = 0; i < labyrinth.Size; i++)
            {
                for (int j = 0; j < labyrinth.Size; j++)
                {
                    labyrinth[i, j] = staticLabyrinth[i, j];
                }
            }
        }

        [TestMethod]
        public void PlayGameTest()
        {
            InitializeData();

            StringBuilder input = new StringBuilder();
            input.AppendLine("r");
            input.AppendLine("a");
            input.AppendLine("d");
            input.AppendLine("d");
            input.AppendLine("d");
            input.AppendLine("Pesho");
            input.AppendLine("exit");

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input.ToString()))
                {


                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    Engine engine = new Engine(labyrinth);

                    engine.PlayGame();



                    StringBuilder expected = new StringBuilder();
                    string terminateLine = "1. Pesho --> 3";
                    int lineOfTermination = 0;
                    string[] lines;
                    using (var stream = new StreamReader("..\\..\\expected.txt"))
                    {
                        while (!stream.EndOfStream)
                        {
                            //expected.Append(stream.ReadToEnd().TrimEnd());
                            //expected.AppendLine(stream.ReadLine().TrimEnd());

                            lines = stream.ReadToEnd().Trim().Split('\r', '\n');
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(lines[i]))
                                {
                                    expected.AppendLine(lines[i].Trim());
                                    if (lines[i] == terminateLine)
                                    {
                                        lineOfTermination = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    StringBuilder actual = new StringBuilder();

                    string[] splitLines = sw.ToString().TrimEnd().Split('\r', '\n');
                    for (int i = 0; i < splitLines.Length && i < lineOfTermination; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(splitLines[i]))
                        {
                            actual.AppendLine(splitLines[i]);
                        }
                    }

                    //StringBuilder expected = new StringBuilder();
                    //expected.AppendLine("Welcome to “Labyrinth” game. Please try to escape. Use 'top' to view the top");
                    //expected.AppendLine("scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
                    //expected.AppendLine("Invalid input!");
                    //expected.AppendLine("**Press a key to continue**");
                    //expected.AppendLine("Enter your move (L=left, R-right, U=up, D=down):");
                    //expected.AppendLine("Enter your move (L=left, R-right, U=up, D=down):");
                    //expected.AppendLine("Enter your move (L=left, R-right, U=up, D=down):");
                    //expected.AppendLine("Congratulations! You've exited the labyrinth in 3 moves.");
                    //expected.AppendLine("**Please put down your name:**");

                    //for (int i = 0; i < staticLabyrinth.GetLength(0); i++)
                    //{
                    //    for (int j = 0; j < staticLabyrinth.GetLength(1); j++)
                    //    {
                    //        expected.AppendFormat("{0,2}", staticLabyrinth[i, j].ToString());
                    //    }
                    //    expected.AppendLine();
                    //}

                    for (int i = 0; i < lineOfTermination; i++)
                    {
                        if (expected[i] != actual[i])
                        {
                            string ex = expected[i].ToString();
                            string ac = actual[i].ToString();
                        }
                    }

                    if (expected.ToString() == actual.ToString())
                    {
                        Console.WriteLine();
                    }

                    Assert.AreEqual(expected.ToString(), actual.ToString());
                }
            }
        }
    }
}
