using System;

namespace Bot.Handlers
{
    public class LogHandler
    {
        public static string CleanResult(string result)
        {
            var indexOfSpace = $"{result}".IndexOf(' ');
            var substringResult = $"{result}".Substring(indexOfSpace + 1);
            return substringResult.Remove(substringResult.Length - 1);
        }

        public void Default(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }

        public void Good(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }

        public void Bad(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }

        public void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }

        public void Alert(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm}]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($" {msg}");
        }
    }
}