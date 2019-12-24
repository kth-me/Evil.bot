using Discord;
using Discord.WebSocket;
using System;

namespace Bot.Handlers
{
    public class EmbedHandler
    {
        public static Embed Neutral(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "", new Color(32, 34, 37), oldUser, newUser);

        public static Embed Good(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "✅", new Color(119, 178, 85), oldUser, newUser);

        public static Embed Bad(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⛔", new Color(190, 25, 49), oldUser, newUser);

        public static Embed Info(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "ℹ", new Color(59, 136, 195), oldUser, newUser);

        public static Embed Alert(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⚠", new Color(255, 204, 76), oldUser, newUser);

        public static Embed Update(string message, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, "⚛️", new Color(146, 102, 204), oldUser, newUser);

        private static Embed EmbedLogic(string message, string icon, Color color, SocketUser oldUser = null, SocketUser newUser = null)
        {
            var embed = new EmbedBuilder();
            embed.WithColor(color);

            if (oldUser == null || newUser == null)
            {
                embed.WithDescription($"{icon} {message}");
                return embed.Build();
            }

            embed.WithTitle($"⚛️ {message}");
            embed.WithDescription($"{newUser.Mention}");
            embed.AddField("Old Username", oldUser, true);
            embed.AddField("New Username", newUser, true);
            embed.WithFooter($"{DateTime.Now:HH:mm:ss}");
            return embed.Build();
        }
    }
}