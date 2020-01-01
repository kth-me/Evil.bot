using Discord;
using Discord.WebSocket;
using System;

namespace Bot.Handlers
{
    public class EmbedHandler
    {
        public static Embed Neutral(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null)
            => EmbedLogic(message, "", new Color(32, 34, 37), userBefore, userAfter, oldUser, newUser);

        public static Embed Good(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "✅", new Color(119, 178, 85), userBefore, userAfter, oldUser, newUser);

        public static Embed Bad(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⛔", new Color(190, 25, 49), userBefore, userAfter, oldUser, newUser);

        public static Embed Info(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "ℹ", new Color(59, 136, 195), userBefore, userAfter, oldUser, newUser);

        public static Embed Alert(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⚠", new Color(255, 204, 76), userBefore, userAfter, oldUser, newUser);

        public static Embed Update(string message, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⚛️", new Color(146, 102, 204), userBefore, userAfter, oldUser, newUser);

        private static Embed EmbedLogic(string message, string icon, Color color, SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null)
        {
            var embed = new EmbedBuilder();
            embed.WithColor(color);

            if (userBefore != null || newUser != null)
            {
                embed.WithTitle($"⚛️ {message}");
                embed.WithDescription($"{newUser.Mention}‎‎‎‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎‏‏‎ ‎");
                embed.WithFooter($"{DateTime.Now:HH:mm:ss}");

                var fieldName = userBefore == null ? "Username" : "Status";

                embed.AddField($"Old {fieldName}", userBefore.Status, true);
                embed.AddField($"New {fieldName}", userAfter.Status, true);
                return embed.Build();
            }

            embed.WithDescription($"{icon} {message}");
            return embed.Build();
        }
    }
}