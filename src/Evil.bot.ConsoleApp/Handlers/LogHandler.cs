
namespace Evil.bot.ConsoleApp.Handlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Discord;
    using Discord.WebSocket;
    using Discord.Commands;
    using Models;

    public class LogHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ConfigModel _config;

        private enum Style
        {
            Good,
            Bad,
            Info,
            Alert,
            Update
        }

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
            _commands.CommandExecuted += OnCommandExecutedAsync;
        }

        private void LogToConsole(Style style, string message = null, LogMessage logMessage = new LogMessage())
        {
            var color = style switch
            {
                Style.Good => ConsoleColor.Green,
                Style.Bad => ConsoleColor.Red,
                Style.Info => ConsoleColor.Cyan,
                Style.Alert => ConsoleColor.Yellow,
                Style.Update => ConsoleColor.Magenta,
                _ => ConsoleColor.Gray
            };

            if (message != null)
            {
                Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
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
        
        private void LogToDiscord(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write($"[{DateTime.Now:dd/M/yyyy HH:mm:ss}]");
            Console.ResetColor();
            Console.WriteLine($" {message}");
        }

        private async Task ChannelCreated(SocketChannel channel)
        {
            // LogToConsole($"Channel {channel} created", ConsoleColor.Cyan);
            // LogToDiscord($"Channel {channel} created", ConsoleColor.Cyan);
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
            LogToConsole(style: Style.Info, logMessage: logMessage);
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
            LogToConsole(style: Style.Update, logMessage: logMessage);

        }
        
        private async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // Command failed. Notify user and log to console
            if (!result.IsSuccess)
            {
                var cleanResult = ResultCleaner($"{result}");
            
                if (command.IsSpecified)
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
                    LogToConsole(style: Style.Alert, message: $"[Command Error] {context.User.Username}#{context.User.Discriminator} used {_config.Prefix}{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                else
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Bad(ResultCleaner($"{result}")));
                    LogToConsole(style: Style.Bad, message: $"[Command Fail] {context.User.Username}#{context.User.Discriminator} used unknown command in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                return;
            }
            
            // Command succeeded. Log to console
            LogToConsole(style: Style.Good, message: $"[Command Success] {context.User.Username}#{context.User.Discriminator} used .{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name}");
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