namespace LabirynthGame
{
    using System;

    public class LabyrinthGameDemo
    {
        private const int SizeOfTheLabyrinth = 7;

        public static void Main()
        {
            throw new ArgumentException("Failed build exception!"); 
            Engine labyrinthEngine = new Engine(SizeOfTheLabyrinth);
            labyrinthEngine.PlayGame();
        }
    }
}