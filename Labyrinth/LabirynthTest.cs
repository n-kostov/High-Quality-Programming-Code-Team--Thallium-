using System;

namespace LabirynthGame
{
    class LabirynthTest
    {
        private static int SizeOfTheLabirynth = 7;
        public static void Main()
        {
            Engine labirynthEngine = new Engine(SizeOfTheLabirynth);
            labirynthEngine.PlayGame();
        }
    }
}