using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace LabirynthGame
{
    public class Labirynth
    {
        private int sizeOfTheLabirynth;
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
                if (row < 0 || row >= sizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("row", "The labirynth does not have such row!");
                }
                else if (col < 0 || col >= sizeOfTheLabirynth)
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
                if (row < 0 || row >= sizeOfTheLabirynth)
                {
                    throw new ArgumentOutOfRangeException("row", "The labirynth does not have such row!");
                }
                else if (col < 0 || col >= sizeOfTheLabirynth)
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
            this.sizeOfTheLabirynth = sizeOfTheLabirynth;
            matrix = new char[sizeOfTheLabirynth, sizeOfTheLabirynth];
            this.GenerateMatrix();
        }

        public void PrintLabirynth()
        {
            for (int row = 0; row < sizeOfTheLabirynth; row++)
            {
                for (int col = 0; col < sizeOfTheLabirynth; col++)
                {
                    Console.Write("{0,2}", this.matrix[row, col]);
                }

                Console.WriteLine();
            }
        }

        private void GenerateMatrix()
        {
            GenerateMatrixBlockedCells();

            matrix[StartPositionX, StartPositionY] = PlayerSign;

            this.MakeAtLeastOneExitReachable(matrix);

        }

        private void GenerateMatrixBlockedCells()
        {
            Random rand = new Random();
            int percentageOfBlockedCells = rand.Next(MinimumPercentageOfBlockedCells, MaximumPercentageOfBlockedCells);

            for (int row = 0; row < sizeOfTheLabirynth; row++)
            {
                for (int col = 0; col < sizeOfTheLabirynth; col++)
                {
                    int num = rand.Next(0, 100);
                    if (num < percentageOfBlockedCells)
                    {
                        matrix[row, col] = BlockedCell;
                    }
                    else
                    {
                        matrix[row, col] = FreeCell;
                    }
                }
            }
        }

        private void MakeAtLeastOneExitReachable(char[,] generatedMatrix)
        {
            Random rand = new Random();
            int row = StartPositionX;
            int col = StartPositionY;
            int[] dirX = { 0, 0, 1, -1 };
            int[] dirY = { 1, -1, 0, 0 };
            int numberOfDirections = 4;
            int maximumTimesToChangeAfter = 2;

            while (this.HasSolution(row, col) == false)
            {
                int num = rand.Next(0, numberOfDirections);
                int times = rand.Next(0, maximumTimesToChangeAfter);

                for (int i = 0; i < times; i++)
                {
                    if (IsInTheLabirynth(row + dirX[num], col + dirY[num]))
                    {
                        row += dirX[num];
                        col += dirY[num];

                        if (matrix[row, col] == PlayerSign)
                        {
                            continue;
                        }

                        matrix[row, col] = FreeCell;
                    }
                }
            }
        }

        private bool IsInTheLabirynth(int row, int col)
        {
            bool isInTheLabirynth = row >= 0 && row < this.sizeOfTheLabirynth &&
                col >= 0 && col < this.sizeOfTheLabirynth;

            return isInTheLabirynth;
        }

        public bool HasSolution(int row, int col)
        {
            if ((row > 0 && row < sizeOfTheLabirynth - 1) &&
                (col > 0 && col < sizeOfTheLabirynth - 1))
            {
                return false;
            }

            return true;
        }
    }
}
