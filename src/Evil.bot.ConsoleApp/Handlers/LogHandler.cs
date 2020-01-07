using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Evil.bot.ConsoleApp.Handlers
{
    public class LogHandler
    {
        //public LogHandler(DiscordSocketClient client, CommandService command)
        //{
        //    client.Log += LogAsync;
        //    command.Log += LogAsync;
        //}

        //private Task LogAsync(LogMessage message)
        //{
        //    if (message.Exception is CommandException cmdException)
        //    {
        //        Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
        //                          + $" failed to execute in {cmdException.Context.Channel}.");
        //        Console.WriteLine(cmdException);
        //    }
        //    else
        //        Console.WriteLine($"[General/{message.Severity}] {message}");

        //    return Task.CompletedTask;
        //}

        public void Neutral(string message) => LogLogic(message, ConsoleColor.Gray);

        public void Good(string message) => LogLogic(message, ConsoleColor.Green);

        public void Bad(string message) => LogLogic(message, ConsoleColor.Red);

        public void Info(string message) => LogLogic(message, ConsoleColor.Cyan);

        public void Alert(string message) => LogLogic(message, ConsoleColor.Yellow);

        public void Update(string message) => LogLogic(message, ConsoleColor.Magenta);

        private static void LogLogic(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }
    }
}