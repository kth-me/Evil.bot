namespace Evil.bot.ConsoleApp.Handlers
{
    using System;

    using Discord;
    using Discord.WebSocket;

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

        public void Neutral(string message) => ConsoleLog(message, ConsoleColor.Gray);

        public void Good(string message) => ConsoleLog(message, ConsoleColor.Green);

        public void Bad(string message) => ConsoleLog(message, ConsoleColor.Red);

        public void Info(string message) => ConsoleLog(message, ConsoleColor.Cyan);

        public void Alert(string message) => ConsoleLog(message, ConsoleColor.Yellow);

        public void Update(string message) => ConsoleLog(message, ConsoleColor.Magenta);

        private void ConsoleLog(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }

        private void DiscordLog(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }

        public void ChannelCreated(SocketChannel channel)
        {
            ConsoleLog($"Channel {channel} created", ConsoleColor.Cyan);
            DiscordLog($"Channel {channel} created", ConsoleColor.Cyan);
        }

        public void ChannelDestroyed(SocketChannel channel)
        {
            //_logger.Info($"Channel {channel} destroyed");
        }

        public void ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
        }

        public void Connected()
        {
        }

        public void CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
        }

        public void Disconnected(Exception exception)
        {
        }

        public void GuildAvailable(SocketGuild guild)
        {
        }

        public void GuildMembersDownloaded(SocketGuild guild)
        {
        }

        public void GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
        }

        public void GuildUnavailable(SocketGuild guild)
        {
        }

        public void GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
        }

        public void JoinedGuild(SocketGuild guild)
        {
        }

        public void LatencyUpdated(int latencyBefore, int latencyAfter)
        {
        }

        public void LeftGuild(SocketGuild guild)
        {
        }

        public void Log(LogMessage logMessage)
        {
        }

        public void LoggedIn()
        {
        }

        public void LoggedOut()
        {
        }

        public void MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        public void MessageReceived(SocketMessage message)
        {
        }

        public void MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter, ISocketMessageChannel channel)
        {
        }

        public void ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public void ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public void ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        public void Ready()
        {
        }

        public void RecipientAdded(SocketGroupUser user)
        {
        }

        public void RecipientRemoved(SocketGroupUser user)
        {
        }

        public void RoleCreated(SocketRole role)
        {
        }

        public void RoleDeleted(SocketRole role)
        {
        }

        public void RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
        }

        public void UserBanned(SocketUser user, SocketGuild guild)
        {
        }

        public void UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
        }

        public void UserJoined(SocketGuildUser user)
        {
        }

        public void UserLeft(SocketGuildUser user)
        {
        }

        public void UserUnbanned(SocketUser user, SocketGuild guild)
        {
        }

        public void UserUpdated(SocketUser oldUser, SocketUser newUser)
        {
        }

        public void UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore, SocketVoiceState voiceStateAfter)
        {
        }
    }
}