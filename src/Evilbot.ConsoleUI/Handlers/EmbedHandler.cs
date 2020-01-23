namespace Evilbot.ConsoleUI.Handlers
{
    using System;

    using Discord;
    using Discord.WebSocket;

    public class EmbedHandler
    {
        //public EmbedHandler(
        //    string severity = "neutral",
        //    string title = null,
        //    string message = null,
        //    SocketChannel channelBefore = null,
        //    SocketChannel channelAfter = null,
        //    SocketSelfUser selfBefore = null,
        //    SocketSelfUser selfAfter = null,
        //    Exception exception = null,
        //    SocketGuild guildBefore = null,
        //    SocketGuild guildAfter = null,
        //    SocketGuildUser userBefore = null,
        //    SocketGuildUser userAfter = null,
        //    Cacheable<IMessage, ulong> cacheMessage = null,

        //    SocketUser oldUser = null,
        //    SocketUser newUser = null)
        //    => EmbedLogicNew(
        //        severity,
        //        title,
        //        message,
        //        string.Empty,
        //        new Color(32, 34, 37),
        //        user,
        //        userBefore,
        //        userAfter,
        //        oldUser,
        //        newUser);

        public static Embed Neutral(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, string.Empty, new Color(32, 34, 37), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Good(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, "✅", new Color(119, 178, 85), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Bad(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, "⛔", new Color(190, 25, 49), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Info(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, "ℹ", new Color(59, 136, 195), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Alert(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, "⚠", new Color(255, 204, 76), user, userBefore, userAfter, oldUser, newUser);

        public static Embed Update(
            string message = null,
            string title = null,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
            => EmbedLogic(message, title, "⚛️", new Color(146, 102, 204), user, userBefore, userAfter, oldUser, newUser);

        //private static Embed EmbedLogicNew(
        //    string severity,
        //    string title,
        //    string message,
        //    string icon,
        //    Color color,
        //    SocketGuildUser user = null,
        //    SocketGuildUser userBefore = null,
        //    SocketGuildUser userAfter = null,
        //    SocketUser oldUser = null,
        //    SocketUser newUser = null)
        //{
        //}

        private static Embed EmbedLogic(
            string message,
            string title,
            string icon,
            Color color,
            SocketGuildUser user = null,
            SocketGuildUser userBefore = null,
            SocketGuildUser userAfter = null,
            SocketUser oldUser = null,
            SocketUser newUser = null)
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
                embed.WithCurrentTimestamp();

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
                    embed.WithTitle($"{icon} {title}");
                    embed.WithDescription(user.Mention);
                    embed.AddField($"Username", $"{user.Username}#{user.Discriminator}", true);
                    embed.AddField($"ID", user.Id, true);
                    embed.AddField($"Joined Server", user.JoinedAt, false);
                    embed.AddField($"Joined Discord", user.CreatedAt, false);
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