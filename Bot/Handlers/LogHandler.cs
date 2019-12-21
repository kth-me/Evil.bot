using System;

namespace Bot.Handlers
{
    public class LogHandler
    {
        public void Log(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }
    }
}