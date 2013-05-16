using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace LabirynthGame
{
    public class Labirynth
    {
        private const int SizeOfTheLabirynth = 7;
        private const int StartPositionX = 3;
        private const int StartPositionY = 3;
        private const int MinimumPercentageOfBlockedCells = 30;
        private const int MaximumPercentageOfBlockedCells = 50;

        const char BlockedCell = 'X';
        const char FreeCell = '-';
        const char PlayerSign = '*';

        private int playerPositionX;
        private int playerPositionY;

        private char[,] matrix;
        private ScoreBoard scoreBoard = new ScoreBoard();
        //private OrderedMultiDictionary<int, string> scoreBoard;

        public Labirynth()
        {
            this.playerPositionX = StartPositionX;
            this.playerPositionY = StartPositionY;
            this.matrix = this.GenerateMatrix();
            //this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        private void Move(int directionX, int directionY)
        {
            if (this.IsMoveValid(this.playerPositionX + directionX, this.playerPositionY + directionY) == false)
            {
                return;
            }

            if (this.matrix[playerPositionY + directionY, playerPositionX + directionX] == BlockedCell)
            {
                Console.WriteLine("Invalid Move!");
                Console.WriteLine("**Press a key to continue**");
                Console.ReadKey();
                return;
            }
            else
            {
                this.matrix[this.playerPositionY, this.playerPositionX] = FreeCell;
                this.matrix[this.playerPositionY + directionY, this.playerPositionX + directionX] = PlayerSign;
                this.playerPositionY += directionY;
                this.playerPositionX += directionX;
                return;
            }
        }

        private bool IsMoveValid(int positionX, int positionY)
        {
            if (positionX < 0 || positionX > SizeOfTheLabirynth - 1 ||
                positionY < 0 || positionY > SizeOfTheLabirynth - 1)
            {
                return false;
            }

            return true;
        }

        private void PrintLabirynth()
        {
            for (int row = 0; row < SizeOfTheLabirynth; row++)
            {
                for (int col = 0; col < SizeOfTheLabirynth; col++)
                {
                    Console.Write("{0,2}", this.matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private char[,] GenerateMatrix()
        {
            char[,] generatedMatrix = new char[SizeOfTheLabirynth, SizeOfTheLabirynth];
            Random rand = new Random();
            int percentageOfBlockedCells = rand.Next(MinimumPercentageOfBlockedCells, MaximumPercentageOfBlockedCells);

            for (int row = 0; row < SizeOfTheLabirynth; row++)
            {
                for (int col = 0; col < SizeOfTheLabirynth; col++)
                {
                    int num = rand.Next(0, 100);
                    if (num < percentageOfBlockedCells)
                    {
                        generatedMatrix[row, col] = BlockedCell;
                    }
                    else
                    {
                        generatedMatrix[row, col] = FreeCell;
                    }
                }
            }

            generatedMatrix[playerPositionY, playerPositionX] = PlayerSign;

            this.MakeAtLeastOneExitReachable(generatedMatrix);

            Console.WriteLine("Welcome to “Labirinth” game. Please try to escape. Use 'top' to view the top");
            Console.WriteLine("scoreboard, 'restart' to start a new game and 'exit' to quit the game.");

            return generatedMatrix;
        }

        private void MakeAtLeastOneExitReachable(char[,] generatedMatrix)
        {
            Random rand = new Random();
            int pathX = StartPositionX;
            int pathY = StartPositionY;
            int[] dirX = { 0, 0, 1, -1 };
            int[] dirY = { 1, -1, 0, 0 };
            int numberOfDirections = 4;
            int maximumTimesToChangeAfter = 2;

            while (this.IsGameOver(pathX, pathY) == false)
            {
                int num = rand.Next(0, numberOfDirections);
                int times = rand.Next(0, maximumTimesToChangeAfter);

                for (int d = 0; d < times; d++)
                {
                    if (pathX + dirX[num] >= 0 && pathX + dirX[num] < SizeOfTheLabirynth && pathY + dirY[num] >= 0 &&
                        pathY + dirY[num] < SizeOfTheLabirynth)
                    {
                        pathX += dirX[num];
                        pathY += dirY[num];

                        if (generatedMatrix[pathY, pathX] == PlayerSign)
                        {
                            continue;
                        }

                        generatedMatrix[pathY, pathX] = FreeCell;
                    }
                }
            }
        }

        private bool IsGameOver(int playerPositionX, int playerPositionY)
        {
            if ((playerPositionX > 0 && playerPositionX < SizeOfTheLabirynth - 1) &&
                (playerPositionY > 0 && playerPositionY < SizeOfTheLabirynth - 1))
            {
                return false;
            }

            return true;
        }

        //private int GetWorstScore()
        //{
        //    int worstScore = this.scoreBoard.Keys.Last();

        //    return worstScore;
        //}

        //private void PrintScore()
        //{
        //    int counter = 1;

        //    if (this.scoreBoard.Count == 0)
        //    {
        //        Console.WriteLine("The scoreboard is empty.");
        //    }
        //    else
        //    {
        //        foreach (var score in this.scoreBoard)
        //        {
        //            var foundScore = this.scoreBoard[score.Key];

        //            foreach (var equalScore in foundScore)
        //            {
        //                Console.WriteLine("{0}. {1} --> {2}", counter, equalScore, score.Key);
        //            }

        //            counter++;
        //        }
        //    }

        //    Console.WriteLine();
        //}

        public void PlayGame()
        {
            string command = string.Empty;
            int movesCounter = 0;
            while (command.Equals("EXIT") == false)
            {
                PrintLabirynth();
                string currentLine = string.Empty;

                if (this.IsGameOver(this.playerPositionX, this.playerPositionY))
                {
                    Console.WriteLine("Congratulations! You've exited the labirynth in {0} moves.", movesCounter);

                    scoreBoard.UpdateScoreBoard(movesCounter);
                    scoreBoard.PrintScore();
                    movesCounter = 0;
                    currentLine = "RESTART";
                }
                else
                {
                    Console.Write("Enter your move (L=left, R-right, U=up, D=down):");
                    currentLine = Console.ReadLine();
                }

                if (currentLine == string.Empty)
                {
                    continue;
                }

                command = currentLine.ToUpper();
                this.ExecuteCommand(command, ref movesCounter);
            }
        }

        private void ExecuteCommand(string command, ref int movesCounter)
        {
            switch (command.ToUpper())
            {
                case "L":
                    {
                        movesCounter++;
                        Move(-1, 0);
                        break;
                    }

                case "R":
                    {
                        movesCounter++;
                        Move(1, 0);
                        break;
                    }

                case "U":
                    {
                        movesCounter++;
                        Move(0, -1);
                        break;
                    }

                case "D":
                    {
                        movesCounter++;
                        Move(0, 1);
                        break;
                    }

                case "RESTART":
                    {
                        this.playerPositionX = StartPositionX;
                        this.playerPositionY = StartPositionY;
                        this.matrix = this.GenerateMatrix();

                        break;
                    }

                case "TOP":
                    {
                        scoreBoard.PrintScore();
                        break;
                    }

                case "EXIT":
                    {
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Invalid input!");
                        Console.WriteLine("**Press a key to continue**");
                        Console.ReadKey();
                        break;
                    }
            }
        }

        //private void UpdateScoreBoard(int currentNumberOfMoves)
        //{
        //    string userName = string.Empty;

        //    if (this.scoreBoard.Count < 5)
        //    {
        //        while (userName == string.Empty)
        //        {
        //            Console.WriteLine("**Please put down your name:**");
        //            userName = Console.ReadLine();
        //        }

        //        this.scoreBoard.Add(currentNumberOfMoves, userName);
        //    }
        //    else
        //    {
        //        int worstScore = this.GetWorstScore();
        //        if (currentNumberOfMoves <= worstScore)
        //        {
        //            if (this.scoreBoard.ContainsKey(currentNumberOfMoves) == false)
        //            {
        //                this.scoreBoard.Remove(worstScore);
        //            }

        //            while (userName == string.Empty)
        //            {
        //                Console.WriteLine("**Please put down your name:**");
        //                userName = Console.ReadLine();
        //            }

        //            this.scoreBoard.Add(currentNumberOfMoves, userName);
        //        }
        //    }
        //}
    }
}
