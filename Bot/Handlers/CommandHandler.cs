using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly Config _config;
        private readonly CommandService _commands;
        private readonly LogHandler _logger;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Allows us to get everything need from DI when  class is instantiated.
        /// </summary>
        /// <returns></returns>
        public CommandHandler(IServiceProvider services, DiscordSocketClient client, CommandService commands, LogHandler logger)
        {
            // Set everything we need from DI
            _commands = commands;
            _client = client;
            _services = services;
            _logger = logger;
            _config ??= new ConfigHandler().GetConfig();
        }

        // Task called to create command service
        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), _services);

            // Hook up events
            HookEvents();
        }

        // Hook up any command specific events
        private void HookEvents()
        {
            _client.MessageReceived += OnMessageReceivedAsync;
            _commands.CommandExecuted += OnCommandExecutedAsync;
            _commands.Log += LogAsync;
        }

        // Display any log messages to the console.
        private Task LogAsync(LogMessage log)
        {
            _logger.Default(log.Message);
            return Task.CompletedTask;
        }

        private async Task OnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // Don't process command if it was a system message
            if (!(socketMessage is SocketUserMessage message))
                return;

            // Create number to track where prefix ends and command begins
            var argPos = 0;

            // Determine if message is command based on prefix and make sure no bots trigger commands
            if (!(message.HasStringPrefix(_config.Prefix, ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                  message.Author.IsBot)
                return;

            // Create WebSocket-based command context based on message
            var context = new SocketCommandContext(_client, message);

            // Execute command with command context just created,
            // along with service provider for precondition checks

            // Result does not indicate a return value, but an
            // object stating if command executed successfully
            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);
        }

        public async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // Command failed. Log to console and notify user
            if (!result.IsSuccess)
            {
                var cleanResult = LogHandler.CleanResult($"{result}");

                if (command.IsSpecified)
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
                    _logger.Alert($"[Command Error] {context.User.Username}#{context.User.Discriminator} used {_config.Prefix}{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                else
                {
                    await context.Channel.SendMessageAsync(embed: EmbedHandler.Bad(LogHandler.CleanResult($"{result}")));
                    _logger.Bad($"[Command Fail] {context.User.Username}#{context.User.Discriminator} used unknown command in {context.Guild.Name}: #{context.Channel.Name} ({cleanResult})");
                }
                return;
            }

            // Command succeeded. Log to console
            _logger.Good($"[Command Success] {context.User.Username}#{context.User.Discriminator} used .{command.Value.Name} in {context.Guild.Name}: #{context.Channel.Name}");
            return;
        }
    }
}