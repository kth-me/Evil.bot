using System;

namespace Bot.Handlers
{
    public class LogHandler
    {
        public static string CleanResult(string result)
        {
            var spaceIndex = $"{result}".IndexOf(' ');
            return $"{result}".Substring(spaceIndex);
        }

        public void Default(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }
        public void GoodLog(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }
        public void BadLog(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }
    }
}