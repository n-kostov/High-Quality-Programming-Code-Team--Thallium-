﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabirynthGame
{
    public class Player : IComparable
    {
        public string Name { get; set; }
        public int Moves { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        // TODO: setter should be checked

        public Player(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }

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
            if (obj == null) return 1;

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