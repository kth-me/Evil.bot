using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Evilbot.Common.Models;
using Evilbot.ConsoleUI;

namespace Evilbot.Common.Logging
{
    public partial class LogHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ConfigModel _config;
        private readonly IEventLogger _consoleLogger;

        public LogHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
            _config ??= new ConfigHandler().GetConfig();
            _consoleLogger = new ConsoleLogger();
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
            
            _commands.Log += CommandLog;
            _commands.CommandExecuted += CommandExecuted;
        }

        private async Task ChannelCreated(SocketChannel channel)
        {
            _consoleLogger.Log(logStyle: LogStyle.Good, message: $"Channel {channel} created");
        }

        private async Task ChannelDestroyed(SocketChannel channel)
        {
            _consoleLogger.Log(logStyle: LogStyle.Good, message: $"Channel {channel} destroyed");
        }

        private async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, message: $"Channel {channelBefore} updated to {channelAfter}");
        }

        private async Task Connected()
        {
            _consoleLogger.Log(logStyle: LogStyle.Good, message: "Connected");
        }

        private async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated to {userAfter}");
        }

        private async Task Disconnected(Exception exception)
        {
            _consoleLogger.Log(logStyle: LogStyle.Bad, message: $"Disconnected due to {exception}");
        }

        private async Task GuildAvailable(SocketGuild guild)
        {
            _consoleLogger.Log(logStyle: LogStyle.Info, message: $"Guild {guild} available");
        }

        private async Task GuildMembersDownloaded(SocketGuild guild)
        {
            _consoleLogger.Log(logStyle: LogStyle.Info, message: $"Guild members for {guild} downloaded");
        }

        private async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated to {userAfter}");
            if (userBefore.Activity.ToString() != "Rider" &&  userAfter.Activity.ToString() == "Rider")
            {
                Console.WriteLine($"Add Coding Now! role to {userAfter}");
            }
            if (userBefore.Activity.ToString() == "Rider" && userAfter.Activity.ToString() != "Rider")
            {
                Console.WriteLine($"Removing Coding Now! role from {userAfter}");
            }
        }

        private async Task GuildUnavailable(SocketGuild guild)
        {
            _consoleLogger.Log(logStyle: LogStyle.Info, message: $"Guild {guild} unavailable");
        }

        private async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, message: $"Guild {guildBefore} updated to {guildAfter}");
        }

        private async Task JoinedGuild(SocketGuild guild)
        {
            _consoleLogger.Log(logStyle: LogStyle.Good, message: $"Joined {guild}");
        }

        private async Task LatencyUpdated(int latencyBefore, int latencyAfter)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, message: $"Latency {latencyBefore} updated to {latencyAfter}");
        }

        private async Task LeftGuild(SocketGuild guild)
        {
            _consoleLogger.Log(logStyle: LogStyle.Bad, message: $"Left {guild}");
        }

        private async Task Log(LogMessage logMessage)
        {
            _consoleLogger.Log(LogStyle.Info, logMessage: logMessage);
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
        
        private async Task CommandLog(LogMessage logMessage)
        {
            _consoleLogger.Log(logStyle: LogStyle.Update, logMessage: logMessage);

        }
        
        private async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // Command failed. Notify user and log to console
            if (!result.IsSuccess)
            {
                var cleanResult = ResultCleaner($"{result}");
            
                if (command.IsSpecified)
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
                    _consoleLogger.Log(logStyle: LogStyle.Alert, message: $"[Command Error] {context.User.Username}#{context.User.Discriminator} used {_config.Prefix}{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                else
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Bad(ResultCleaner($"{result}")));
                    _consoleLogger.Log(logStyle: LogStyle.Bad, message: $"[Command Fail] {context.User.Username}#{context.User.Discriminator} used unknown command in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                return;
            }
            
            // Command succeeded. Log to console
            _consoleLogger.Log(logStyle: LogStyle.Good, message: $"[Command Success] {context.User.Username}#{context.User.Discriminator} used .{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name}");
            return;
        }

        private string ResultCleaner(string result)
        {
            var indexOfSpace = $"{result}".IndexOf(' ');
            var substringResult = $"{result}".Substring(indexOfSpace + 1);
            return substringResult.Remove(substringResult.Length - 1);
        }
    }
}