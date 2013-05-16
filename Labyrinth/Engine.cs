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

        private Labirynth labirynth;
        private ScoreBoard scoreBoard;
        private Player player;

        public Engine(int sizeOfTheLabirynth)
        {
            this.sizeOfTheLabirynth = sizeOfTheLabirynth;
            this.labirynth = new Labirynth(sizeOfTheLabirynth);
            this.scoreBoard = new ScoreBoard();
            this.player = new Player(StartPositionX, StartPositionY);
            this.IntroduceTheGame();
        }

        private void IntroduceTheGame()
        {
            Console.WriteLine("Welcome to “Labirinth” game. Please try to escape. Use 'top' to view the top");
            Console.WriteLine("scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
        }

        private void Move(int directionX, int directionY)
        {
            if (this.IsMoveValid(this.player.PositionX + directionX, this.player.PositionY + directionY) == false)
            {
                return;
            }

            if (this.labirynth[this.player.PositionX + directionX, this.player.PositionY + directionY] == BlockedCell)
            {
                this.PrintInvalidInput();
                return;
            }
            else
            {
                this.labirynth[this.player.PositionX, this.player.PositionY] = FreeCell;
                this.labirynth[this.player.PositionX + directionX, this.player.PositionY + directionY] = PlayerSign;

                this.ChoosePlayerMove(directionX, directionY);

                return;
            }
        }

        private void ChoosePlayerMove(int directionX, int directionY)
        {
            if (directionX == 0)
            {
                if (directionY < 0)
                {
                    player.MoveUp();
                }
                else
                {
                    player.MoveDown();
                }
            }
            else
            {
                if (directionX < 0)
                {
                    player.MoveLeft();
                }
                else
                {
                    player.MoveRight();
                }
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
            while (command.Equals("EXIT") == false)
            {
                labirynth.PrintLabirynth();
                string currentLine = string.Empty;

                if (HasWon(this.player.PositionX, this.player.PositionY))
                {
                    this.CelebrateVictory();
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
                this.ExecuteCommand(command);
            }
        }

        private bool HasWon(int row, int col)
        {
            if ((row > 0 && row < this.sizeOfTheLabirynth - 1) &&
                (col > 0 && col < this.sizeOfTheLabirynth - 1))
            {
                return false;
            }

            return true;
        }

        private void CelebrateVictory()
        {
            Console.WriteLine("Congratulations! You've exited the labirynth in {0} moves.", player.Moves);

            string userName = string.Empty;

            while (userName == string.Empty)
            {
                Console.WriteLine("**Please put down your name:**");
                userName = Console.ReadLine();
            }

            this.player.Name = userName;

            scoreBoard.UpdateScoreBoard(player);
            scoreBoard.PrintScore();
            
        }

        private void ExecuteCommand(string command)
        {
            switch (command.ToUpper())
            {
                case "L":
                    {
                        Move(0, -1);
                        break;
                    }

                case "R":
                    {
                        Move(0, 1);
                        break;
                    }

                case "U":
                    {
                        Move(-1, 0);
                        break;
                    }

                case "D":
                    {
                        Move(1, 0);
                        break;
                    }

                case "RESTART":
                    {
                        this.player = new Player(StartPositionX, StartPositionY);
                        this.labirynth = new Labirynth(this.sizeOfTheLabirynth);
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
                        this.PrintInvalidInput();
                        break;
                    }
            }
        }

        private void PrintInvalidInput()
        {
            Console.WriteLine("Invalid input!");
            Console.WriteLine("**Press a key to continue**");
            Console.ReadKey();
        }
    }
}
