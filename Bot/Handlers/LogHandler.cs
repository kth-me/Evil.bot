using System;

namespace Bot.Handlers
{
    public class LogHandler
    {
        public void Log(string msg)
        {
            Console.WriteLine($"[{DateTime.Now:dd/m HH:mmtt}] - {msg}");
        }
    }
}