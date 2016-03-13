namespace LabirynthGame
{
    using System;

    public class LabyrinthGameDemo
    {
        private const int SizeOfTheLabyrinth = 7;

        public static void Main()
        {
            throw new ArgumentException("Faild the build")
            Engine labyrinthEngine = new Engine(SizeOfTheLabyrinth);
            labyrinthEngine.PlayGame();
        }
    }
}