namespace LabirynthGame
{
    using System;

    public class Engine
    {
        private const int StartPositionX = 3;
        private const int StartPositionY = 3;

        private const char BlockedCell = 'X';
        private const char FreeCell = '-';
        private const char PlayerSign = '*';

        private int sizeOfTheLabirynth;

        private Labyrinth labyrinth;
        private ScoreBoard scoreBoard;
        private Player player;

        public Engine(int sizeOfTheLabirynth)
        {
            if (sizeOfTheLabirynth < 1)
            {
                throw new ArgumentException("The size of the labyrinth cannot be less than 1", "sizeOfTheLabyrinth");
            }

            this.sizeOfTheLabirynth = sizeOfTheLabirynth;
            this.labyrinth = new Labyrinth(sizeOfTheLabirynth);
            this.scoreBoard = new ScoreBoard();
            this.player = new Player(StartPositionX, StartPositionY);
            this.IntroduceTheGame();
        }

        public void PlayGame()
        {
            string command = string.Empty;
            while (command.Equals("EXIT") == false)
            {
                this.labyrinth.PrintLabirynth();
                string currentLine = string.Empty;

                if (this.HasWon(this.player.PositionX, this.player.PositionY))
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

        private void IntroduceTheGame()
        {
            Console.WriteLine("Welcome to “Labyrinth” game. Please try to escape. Use 'top' to view the top");
            Console.WriteLine("scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
        }

        private void Move(int directionX, int directionY)
        {
            if (this.IsMoveValid(this.player.PositionX + directionX, this.player.PositionY + directionY) == false)
            {
                return;
            }

            if (this.labyrinth[this.player.PositionX + directionX, this.player.PositionY + directionY] == BlockedCell)
            {
                this.PrintInvalidInput();
                return;
            }
            else
            {
                this.labyrinth[this.player.PositionX, this.player.PositionY] = FreeCell;
                this.labyrinth[this.player.PositionX + directionX, this.player.PositionY + directionY] = PlayerSign;

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
                    this.player.MoveUp();
                }
                else
                {
                    this.player.MoveDown();
                }
            }
            else
            {
                if (directionX < 0)
                {
                    this.player.MoveLeft();
                }
                else
                {
                    this.player.MoveRight();
                }
            }
        }

        private bool IsMoveValid(int positionX, int positionY)
        {
            if (positionX < 0 || positionX > this.sizeOfTheLabirynth - 1 ||
                positionY < 0 || positionY > this.sizeOfTheLabirynth - 1)
            {
                return false;
            }

            return true;
        }

        private bool HasWon(int positionX, int positionY)
        {
            if ((positionX > 0 && positionX < this.sizeOfTheLabirynth - 1) &&
                (positionY > 0 && positionY < this.sizeOfTheLabirynth - 1))
            {
                return false;
            }

            return true;
        }

        private void CelebrateVictory()
        {
            Console.WriteLine("Congratulations! You've exited the labyrinth in {0} moves.", this.player.Moves);

            string userName = string.Empty;

            while (userName == string.Empty)
            {
                Console.WriteLine("**Please put down your name:**");
                userName = Console.ReadLine();
            }

            this.player.Name = userName;

            this.scoreBoard.UpdateScoreBoard(this.player);
            this.scoreBoard.PrintScore();
        }

        private void ExecuteCommand(string command)
        {
            switch (command.ToUpper())
            {
                case "L":
                    {
                        this.Move(0, -1);
                        break;
                    }

                case "R":
                    {
                        this.Move(0, 1);
                        break;
                    }

                case "U":
                    {
                        this.Move(-1, 0);
                        break;
                    }

                case "D":
                    {
                        this.Move(1, 0);
                        break;
                    }

                case "RESTART":
                    {
                        this.player = new Player(StartPositionX, StartPositionY);
                        this.labyrinth = new Labyrinth(this.sizeOfTheLabirynth);
                        break;
                    }

                case "TOP":
                    {
                        this.scoreBoard.PrintScore();
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
