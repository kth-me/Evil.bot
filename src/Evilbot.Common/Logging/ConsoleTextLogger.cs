using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Evilbot.Common.Models;
using Evilbot.ConsoleUI;

namespace Evilbot.Common.Logging
{
    public class ConsoleTextLogger : IEventLogger
    {
        private readonly ConfigModel _config;
        private readonly TextLogFormatter _formatter;
        
        public ConsoleTextLogger()
        {
            _config ??= new ConfigHandler().GetConfig();
            _formatter = new TextLogFormatter();
        }

        public async Task ChannelCreated(SocketChannel channel)
        {
            _formatter.Log(logStyle: LogStyle.Good, message: $"Channel {channel} created");
        }

        public async Task ChannelDestroyed(SocketChannel channel)
        {
            _formatter.Log(logStyle: LogStyle.Good, message: $"Channel {channel} destroyed");
        }

        public async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
            _formatter.Log(logStyle: LogStyle.Update, message: $"Channel {channelBefore} updated to {channelAfter}");
        }

        public async Task Connected()
        {
            _formatter.Log(logStyle: LogStyle.Good, message: "Connected");
        }

        public async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
            _formatter.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated to {userAfter}");
        }

        public async Task Disconnected(Exception exception)
        {
            _formatter.Log(logStyle: LogStyle.Bad, message: $"Disconnected due to {exception}");
        }

        public async Task GuildAvailable(SocketGuild guild)
        {
            _formatter.Log(logStyle: LogStyle.Info, message: $"Guild {guild} available");
        }

        public async Task GuildMembersDownloaded(SocketGuild guild)
        {
            _formatter.Log(logStyle: LogStyle.Info, message: $"Guild members for {guild} downloaded");
        }

        public async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
            if (userBefore.Status != userAfter.Status)
            {
                _formatter.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated from {userBefore.Status} to {userAfter.Status}");
            }

            if (userBefore.Nickname != userAfter.Nickname)
            {
                _formatter.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated from {userBefore.Nickname} to {userAfter.Nickname}");
            }
            _formatter.Log(logStyle: LogStyle.Update, message: $"User {userBefore} updated from {userBefore.VoiceState.ToString()} to {userAfter.VoiceState.ToString()}");
        }

        public async Task GuildUnavailable(SocketGuild guild)
        {
            _formatter.Log(logStyle: LogStyle.Info, message: $"Guild {guild} unavailable");
        }

        public async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
            _formatter.Log(logStyle: LogStyle.Update, message: $"Guild {guildBefore} updated to {guildAfter}");
        }

        public async Task JoinedGuild(SocketGuild guild)
        {
            _formatter.Log(logStyle: LogStyle.Good, message: $"Joined {guild}");
        }

        public async Task LatencyUpdated(int latencyBefore, int latencyAfter)
        {
            _formatter.Log(logStyle: LogStyle.Update, message: $"Latency {latencyBefore} updated to {latencyAfter}");
        }

        public async Task LeftGuild(SocketGuild guild)
        {
            _formatter.Log(logStyle: LogStyle.Bad, message: $"Left {guild}");
        }

        public async Task Log(LogMessage logMessage)
        {
            _formatter.Log(LogStyle.Info, logMessage: logMessage);
        }

        public async Task LoggedIn()
        {
            _formatter.Log(logStyle: LogStyle.Good, message: "Logged In");
        }

        public async Task LoggedOut()
        {
            _formatter.Log(logStyle: LogStyle.Bad, message: "Logged Out");
        }

        public async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
            _formatter.Log(logStyle: LogStyle.Info, message: $"Message '{cacheMessage.Value}' deleted from #{channel}");
        }

        public async Task MessageReceived(SocketMessage message)
        {
            _formatter.Log(logStyle: LogStyle.Info, message: $"Message '{message}' received in #{message.Channel}");
        }

        public async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter, ISocketMessageChannel channel)
        {
            _formatter.Log(logStyle: LogStyle.Update, message: $"Message updated from '{cacheMessageBefore.Value}' to '{messageAfter}' in #{channel}");
        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        public async Task Ready()
        {
        }

        public async Task RecipientAdded(SocketGroupUser user)
        {
        }

        public async Task RecipientRemoved(SocketGroupUser user)
        {
        }

        public async Task RoleCreated(SocketRole role)
        {
        }

        public async Task RoleDeleted(SocketRole role)
        {
        }

        public async Task RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
        }

        public async Task UserBanned(SocketUser user, SocketGuild guild)
        {
        }

        public async Task UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
        }

        public async Task UserJoined(SocketGuildUser user)
        {
        }

        public async Task UserLeft(SocketGuildUser user)
        {
        }

        public async Task UserUnbanned(SocketUser user, SocketGuild guild)
        {
        }

        public async Task UserUpdated(SocketUser oldUser, SocketUser newUser)
        {
        }

        public async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore, SocketVoiceState voiceStateAfter)
        {
        }
        
        public async Task CommandLog(LogMessage logMessage)
        {
            _formatter.Log(logStyle: LogStyle.Update, logMessage: logMessage);

        }
        
        public async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // Command failed. Notify user and log to console
            if (!result.IsSuccess)
            {
                var cleanResult = ResultCleaner($"{result}");
            
                if (command.IsSpecified)
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
                    _formatter.Log(logStyle: LogStyle.Alert, message: $"[Command Error] {context.User.Username}#{context.User.Discriminator} used {_config.Prefix}{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                else
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Bad(ResultCleaner($"{result}")));
                    _formatter.Log(logStyle: LogStyle.Bad, message: $"[Command Fail] {context.User.Username}#{context.User.Discriminator} used unknown command in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                return;
            }
            
            // Command succeeded. Log to console
            _formatter.Log(logStyle: LogStyle.Good, message: $"[Command Success] {context.User.Username}#{context.User.Discriminator} used .{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name}");
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