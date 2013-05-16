using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace LabirynthGame
{
    public class Labirynth
    {
        private int SizeOfTheLabirynth;
        private const int StartPositionX = 3;
        private const int StartPositionY = 3;
        private const int MinimumPercentageOfBlockedCells = 30;
        private const int MaximumPercentageOfBlockedCells = 50;

        private const char BlockedCell = 'X';
        private const char FreeCell = '-';
        private const char PlayerSign = '*';

        private char[,] matrix;

        public char this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= SizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("row", "The labirynth does not have such row!");
                }
                else if (col < 0 || col >= SizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("col", "The labirynth does not have such col!");
                }
                else
                {
                    return matrix[row, col];
                }
            }
            set
            {
                if (row < 0 || row >= SizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("row", "The labirynth does not have such row!");
                }
                else if (col < 0 || col >= SizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("col", "The labirynth does not have such col!");
                }
                else
                {
                    matrix[row, col] = value;
                }
            }
        }

        public Labirynth(int sizeOfTheLabirynth)
        {
            this.SizeOfTheLabirynth = sizeOfTheLabirynth;
            this.matrix = this.GenerateMatrix();
        }

        public void PrintLabirynth()
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

            generatedMatrix[StartPositionX, StartPositionY] = PlayerSign;

            this.MakeAtLeastOneExitReachable(generatedMatrix);

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

            while (this.HasSolution(pathX, pathY) == false)
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

                        if (generatedMatrix[pathX, pathY] == PlayerSign)
                        {
                            continue;
                        }

                        generatedMatrix[pathX, pathY] = FreeCell;
                    }
                }
            }
        }

        public bool HasSolution(int row, int col)
        {
            if ((row > 0 && row < SizeOfTheLabirynth - 1) &&
                (col > 0 && col < SizeOfTheLabirynth - 1))
            {
                return false;
            }

            return true;
        }
    }
}
