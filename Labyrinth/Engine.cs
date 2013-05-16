using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabirynthGame
{
    public class Engine
    {
        private int sizeOfTheLabirynth;
        private const int StartPositionX = 3;
        private const int StartPositionY = 3;

        private const char BlockedCell = 'X';
        private const char FreeCell = '-';
        private const char PlayerSign = '*';

        private int playerPositionX;
        private int playerPositionY;

        private Labirynth labirynth;
        private ScoreBoard scoreBoard;

        public Engine(int sizeOfTheLabirynth)
        {
            this.playerPositionX = StartPositionX;
            this.playerPositionY = StartPositionY;
            this.sizeOfTheLabirynth = sizeOfTheLabirynth;
            this.labirynth = new Labirynth(sizeOfTheLabirynth);
            this.scoreBoard = new ScoreBoard();
        }

        private void Move(int directionX, int directionY)
        {
            if (this.IsMoveValid(this.playerPositionX + directionX, this.playerPositionY + directionY) == false)
            {
                return;
            }

            if (this.labirynth[playerPositionY + directionY, playerPositionX + directionX] == BlockedCell)
            {
                Console.WriteLine("Invalid Move!");
                Console.WriteLine("**Press a key to continue**");
                Console.ReadKey();
                return;
            }
            else
            {
                this.labirynth[this.playerPositionY, this.playerPositionX] = FreeCell;
                this.labirynth[this.playerPositionY + directionY, this.playerPositionX + directionX] = PlayerSign;
                this.playerPositionY += directionY;
                this.playerPositionX += directionX;
                return;
            }
        }

        private bool IsMoveValid(int positionX, int positionY)
        {
            if (positionX < 0 || positionX > sizeOfTheLabirynth - 1 ||
                positionY < 0 || positionY > sizeOfTheLabirynth - 1)
            {
                return false;
            }

            return true;
        }

        public void PlayGame()
        {
            string command = string.Empty;
            int movesCounter = 0;
            while (command.Equals("EXIT") == false)
            {
                labirynth.PrintLabirynth();
                string currentLine = string.Empty;

                if (labirynth.HasSolution(this.playerPositionX, this.playerPositionY))
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
                        this.labirynth = new Labirynth(this.sizeOfTheLabirynth);
                        this.PlayGame();

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
    }
}
