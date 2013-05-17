namespace LabirynthGame
{
    using System;

    public class Player : IComparable
    {
        private string name;

        public Player(int positionX, int positionY)
        {
            this.Moves = 0;
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("name", "The Player's name cannot be null or whitespace!");
                }

                this.name = value;
            }
        }

        public int Moves { get; private set; }

        public int PositionX { get; private set; }

        public int PositionY { get; private set; } // TODO: setter should be checked

        public void MoveLeft()
        {
            this.Moves++;
            this.PositionX--;
        }

        public void MoveUp()
        {
            this.Moves++;
            this.PositionY--;
        }

        public void MoveRight()
        {
            this.Moves++;
            this.PositionX++;
        }

        public void MoveDown()
        {
            this.Moves++;
            this.PositionY++;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            Player otherPlayer = obj as Player;
            if (otherPlayer != null)
            {
                return this.Moves.CompareTo(otherPlayer.Moves);
            }
            else
            {
                throw new ArgumentException("Object is not a Player");
            }
        }
    }
}
