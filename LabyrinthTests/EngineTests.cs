namespace LabyrinthTests
{
    using System;
    using System.IO;
    using System.Text;
    using LabirynthGame;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EngineTests
    {
        private char[,] staticLabyrinth = 
        { 
                                          { 'X', '-', '-', '-', '-', '-', 'X' }, 
                                          { 'X', '-', 'X', '-', '-', '-', '-' }, 
                                          { 'X', 'X', '-', '-', '-', 'X', '-' }, 
                                          { 'X', '-', 'X', '*', 'X', '-', '-' }, 
                                          { '-', '-', '-', '-', 'X', '-', '-' }, 
                                          { '-', '-', 'X', '-', '-', 'X', 'X' }, 
                                          { '-', '-', 'X', '-', '-', 'X', '-' } 
        };

        private Labyrinth labyrinth = new Labyrinth(7);

        [TestMethod]
        public void PlayGameTestPathOnlyDown()
        {
            this.InitializeData();

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

                    Engine engine = new Engine(this.labyrinth);

                    engine.PlayGame();

                    StringBuilder expected = new StringBuilder();
                    string terminateLine = "1. Pesho --> 3";
                    int lineOfTermination = 0;
                    string[] lines;
                    char[] delims = { '\r', '\n' };
                    using (var stream = new StreamReader("..\\..\\expected.txt"))
                    {
                        while (!stream.EndOfStream)
                        {
                            lines = stream.ReadToEnd().Split(delims, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(lines[i]))
                                {
                                    expected.AppendLine(lines[i]);
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

                    string[] splitLines = sw.ToString().Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < splitLines.Length && i <= lineOfTermination; i++)
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

        [TestMethod]
        public void PlayGameTestDifferentMoves()
        {
            this.InitializeData();

            StringBuilder input = new StringBuilder();
            input.AppendLine("u");
            input.AppendLine("l");
            input.AppendLine("r");
            input.AppendLine("d");
            input.AppendLine("u");
            input.AppendLine("u");
            input.AppendLine("u");
            input.AppendLine("Pesho");
            input.AppendLine("exit");

            using (var sw = new StringWriter())
            {
                using (var sr = new StringReader(input.ToString()))
                {
                    Console.SetOut(sw);
                    Console.SetIn(sr);

                    Engine engine = new Engine(this.labyrinth);

                    engine.PlayGame();

                    StringBuilder expected = new StringBuilder();
                    string terminateLine = "1. Pesho --> 7";
                    int lineOfTermination = 0;
                    string[] lines;
                    char[] delims = { '\r', '\n' };
                    using (var stream = new StreamReader("..\\..\\expected2.txt"))
                    {
                        while (!stream.EndOfStream)
                        {
                            lines = stream.ReadToEnd().Split(delims, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(lines[i]))
                                {
                                    expected.AppendLine(lines[i]);
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

                    string[] splitLines = sw.ToString().Split(delims, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < splitLines.Length && i <= lineOfTermination; i++)
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

        private void InitializeData()
        {
            for (int i = 0; i < this.labyrinth.Size; i++)
            {
                for (int j = 0; j < this.labyrinth.Size; j++)
                {
                    this.labyrinth[i, j] = this.staticLabyrinth[i, j];
                }
            }
        }
    }
}