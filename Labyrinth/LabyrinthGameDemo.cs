namespace LabirynthGame
{
    using System;

    public class LabyrinthGameDemo
    {
        private const int SizeOfTheLabyrinth = 7;

        public static void Main()
        {
            Engine labyrinthEngine = new Engine(SizeOfTheLabyrinth);
            labyrinthEngine.PlayGame();
        }
    }
}