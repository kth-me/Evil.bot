using Discord;
using Discord.WebSocket;
using System;

namespace Evil.bot.ConsoleApp.Handlers
{
    public class EmbedHandler
    {
        public static Embed Neutral(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null)
            => EmbedLogic(message, title, "", new Color(32, 34, 37), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Good(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, title, "✅", new Color(119, 178, 85), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Bad(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, title, "⛔", new Color(190, 25, 49), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Info(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, title, "ℹ", new Color(59, 136, 195), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Alert(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, title, "⚠", new Color(255, 204, 76), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Update(string message = null, string title = null, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null) 
            => EmbedLogic(message, title, "⚛️", new Color(146, 102, 204), user, userBefore, userAfter, oldUser, newUser);

        private static Embed EmbedLogic(string message, string title, string icon, Color color, SocketGuildUser user = null, 
            SocketGuildUser userBefore = null, SocketGuildUser userAfter = null, SocketUser oldUser = null, SocketUser newUser = null)
        {
            var embed = new EmbedBuilder();

            embed.WithColor(color);

            if (title == null)
            {
                embed.WithDescription($"{icon} {message}");
                return embed.Build();
            }

            else
            {
                embed.WithTitle($"{icon} {title}");
                embed.WithFooter($"Today at {DateTime.Now:HH:mm:ss}");

                if (title == "whois")
                {
                    embed.AddField($"Mention", user.Mention, false);
                    embed.AddField($"Username", $"{user.Username}#{user.Discriminator}", true);
                    if (user.Nickname != null)
                        embed.AddField($"Nickname", user.Nickname, true);
                    embed.AddField($"ID", user.Id, true);
                    embed.AddField($"Status", user.Status, false);
                    
                    foreach (var role in user.Roles)
                    {
                        embed.AddField($"Role", role, false);
                    }
                    
                    embed.AddField($"Joined Server", user.JoinedAt, true);
                    embed.AddField($"Joined Discord", user.CreatedAt, true);
                    embed.WithThumbnailUrl(user.GetAvatarUrl());
                    return embed.Build();
                }

                if (title == "Guild Member Updated")
                {
                    embed.WithDescription($"{userAfter.Mention}‎‎‎‎‏‏‎‎");
                    embed.AddField($"Old Status", userBefore.Status, true);
                    embed.AddField($"New Status", userAfter.Status, true);
                    return embed.Build();
                }

                if (title == "User Joined" || title == "User Left")
                {
                    embed.WithDescription($"{user.Mention}");
                    embed.AddField($"Joined Server", user.JoinedAt, true);
                    embed.AddField($"Joined Discord", user.CreatedAt, true);
                    embed.WithThumbnailUrl(user.GetAvatarUrl());
                    return embed.Build();
                }

                if (title == "User Updated")
                {
                    embed.WithDescription($"{newUser.Mention}‎‎‎‎‏‏‎‎");
                    embed.AddField($"Old Username", oldUser.Status, true);
                    embed.AddField($"New Username", newUser.Status, true);
                    return embed.Build();
                }

                if (title == "User Voice State Updated")
                {
                    embed.WithDescription($"{newUser.Mention}‎‎‎‎‏‏‎‎");
                    embed.AddField($"Old Username", oldUser.Status, true);
                    embed.AddField($"New Username", newUser.Status, true);
                    return embed.Build();
                }

                embed.WithDescription($"{icon} Unhandled Client Event‎");
                return embed.Build();
            }
        }
    }
}