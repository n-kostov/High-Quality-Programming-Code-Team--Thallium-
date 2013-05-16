namespace LabirynthGame
{
    using System;

    public class LabirynthTest
    {
        private const int SizeOfTheLabirynth = 7;

        public static void Main()
        {
            Engine labirynthEngine = new Engine(SizeOfTheLabirynth);
            labirynthEngine.PlayGame();
        }
    }
}