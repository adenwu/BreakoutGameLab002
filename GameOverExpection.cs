using System;

namespace BreakoutGameLab001
{
    public class GameOverException : Exception
    {
        public GameOverException(string message) : base(message)
        {
        }
    }
}
