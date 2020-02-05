using System;
using System.Linq;
using Discord;
using Discord.Commands;

namespace Evilbot.Common.Logging
{
    public class TextLogFormatter
    {
        public void Log(LogStyle logStyle, string message = null, LogMessage logMessage = new LogMessage())
        {
            var color = logStyle switch
            {
                LogStyle.Good => ConsoleColor.Green,
                LogStyle.Bad => ConsoleColor.Red,
                LogStyle.Info => ConsoleColor.Cyan,
                LogStyle.Alert => ConsoleColor.Yellow,
                LogStyle.Update => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };
            
            var style = logStyle switch
            {
                LogStyle.Good => "Good",
                LogStyle.Bad => "Bad",
                LogStyle.Info => "Info",
                LogStyle.Alert => "Alert",
                LogStyle.Update => "Update",
                _ => "Neutral"
            };


            if (message != null)
            {
                Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
                Console.ForegroundColor = color;
                Console.Write($" [{style}]");
                Console.ResetColor();
                Console.WriteLine($" {message}");
                return;
            }
            
            if (logMessage.Source == "Rest")
            {
                return;
            }
            
            if (logMessage.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{logMessage.Severity}] {cmdException.Command.Aliases.First()}"
                                  + $" failed to execute in {cmdException.Context.Channel}.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(cmdException);
                Console.ResetColor();
            }
            else
            {
                Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
                Console.ForegroundColor = color;
                Console.Write($" [{logMessage.Severity}/{logMessage.Source}]");
                Console.ResetColor();
                Console.WriteLine($" {logMessage.Message}");
            }
        }
    }
}