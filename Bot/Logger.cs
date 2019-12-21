using System;

namespace Bot
{
    public class Logger
    {
        public void Log(string msg)
        {
            Console.WriteLine($"[{DateTime.Now:dd/m HH:mmtt}] - {msg}");
        }
    }
}