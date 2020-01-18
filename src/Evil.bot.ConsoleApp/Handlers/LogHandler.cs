namespace Evil.bot.ConsoleApp.Handlers
{
    using System;
    using System.Threading.Tasks;

    using Discord;
    using Discord.WebSocket;
    using Discord.Commands;

    using Evil.bot.ConsoleApp.Models;

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

        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ConfigModel _config;

        public LogHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
            _config ??= new ConfigHandler().GetConfig();
        }

        public async Task InitializeAsync()
        {
            _client.ChannelCreated += ChannelCreated;
            _client.ChannelDestroyed += ChannelDestroyed;
            _client.ChannelUpdated += ChannelUpdated;
            _client.Connected += Connected;
            _client.CurrentUserUpdated += CurrentUserUpdated;
            _client.Disconnected += Disconnected;
            _client.GuildAvailable += GuildAvailable;
            _client.GuildMembersDownloaded += GuildMembersDownloaded;
            _client.GuildMemberUpdated += GuildMemberUpdated;
            _client.GuildUnavailable += GuildUnavailable;
            _client.GuildUpdated += GuildUpdated;
            _client.JoinedGuild += JoinedGuild;
            _client.LatencyUpdated += LatencyUpdated;
            _client.LeftGuild += LeftGuild;
            _client.Log += Log;
            _commands.Log += Log;
            _client.LoggedIn += LoggedIn;
            _client.LoggedOut += LoggedOut;
            _client.MessageDeleted += MessageDeleted;
            _client.MessageReceived += MessageReceived;
            _client.MessageUpdated += MessageUpdated;
            _client.ReactionAdded += ReactionAdded;
            _client.ReactionRemoved += ReactionRemoved;
            _client.ReactionsCleared += ReactionsCleared;
            _client.Ready += Ready;
            _client.RecipientAdded += RecipientAdded;
            _client.RecipientRemoved += RecipientRemoved;
            _client.RoleCreated += RoleCreated;
            _client.RoleDeleted += RoleDeleted;
            _client.RoleUpdated += RoleUpdated;
            _client.UserBanned += UserBanned;
            _client.UserIsTyping += UserIsTyping;
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
            _client.UserUnbanned += UserUnbanned;
            _client.UserUpdated += UserUpdated;
            _client.UserVoiceStateUpdated += UserVoiceStateUpdated;
        }

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

        private async Task ChannelCreated(SocketChannel channel)
        {
            ConsoleLog($"Channel {channel} created", ConsoleColor.Cyan);
            DiscordLog($"Channel {channel} created", ConsoleColor.Cyan);
        }

        private async Task ChannelDestroyed(SocketChannel channel)
        {
            //_logger.Info($"Channel {channel} destroyed");
        }

        private async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
        }

        private async Task Connected()
        {
        }

        private async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
        }

        private async Task Disconnected(Exception exception)
        {
        }

        private async Task GuildAvailable(SocketGuild guild)
        {
        }

        private async Task GuildMembersDownloaded(SocketGuild guild)
        {
        }

        private async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
        }

        private async Task GuildUnavailable(SocketGuild guild)
        {
        }

        private async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
        }

        private async Task JoinedGuild(SocketGuild guild)
        {
        }

        private async Task LatencyUpdated(int latencyBefore, int latencyAfter)
        {
        }

        private async Task LeftGuild(SocketGuild guild)
        {
        }

        private async Task Log(LogMessage logMessage)
        {
            Neutral(logMessage.Message);
        }

        private async Task LoggedIn()
        {
        }

        private async Task LoggedOut()
        {
        }

        private async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        private async Task MessageReceived(SocketMessage message)
        {
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter, ISocketMessageChannel channel)
        {
        }

        private async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        private async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        private async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        private async Task Ready()
        {
        }

        private async Task RecipientAdded(SocketGroupUser user)
        {
        }

        private async Task RecipientRemoved(SocketGroupUser user)
        {
        }

        private async Task RoleCreated(SocketRole role)
        {
        }

        private async Task RoleDeleted(SocketRole role)
        {
        }

        private async Task RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
        }

        private async Task UserBanned(SocketUser user, SocketGuild guild)
        {
        }

        private async Task UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
        }

        private async Task UserJoined(SocketGuildUser user)
        {
        }

        private async Task UserLeft(SocketGuildUser user)
        {
        }

        private async Task UserUnbanned(SocketUser user, SocketGuild guild)
        {
        }

        private async Task UserUpdated(SocketUser oldUser, SocketUser newUser)
        {
        }

        private async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore, SocketVoiceState voiceStateAfter)
        {
        }
    }
}