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
        private readonly CommandService _commands;
        private readonly Config _config;
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
            _client.MessageReceived += HandlerMessageAsync;
            _commands.CommandExecuted += CommandExecutedAsync;
            _commands.Log += LogAsync;
        }

        // Display any log messages to the console.
        private Task LogAsync(LogMessage log)
        {
            _logger.Log(log.Message);
            return Task.CompletedTask;
        }

        private async Task HandlerMessageAsync(SocketMessage socketMessage)
        {
            // Don't process command if it was a system message
            if (!(socketMessage is SocketUserMessage message))
                return;

            // Create number to track where prefix ends and command begins
            int argPos = 0;

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

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            int spaceIndex;
            string cleanResult;

            // Command failed because it isn't found in bot (ignore it)
            if (!command.IsSpecified)
            {
                _logger.Log(($"Command Error: {result}"));
                spaceIndex = $"{result}".IndexOf(' ');
                cleanResult = $"{result}".Substring(spaceIndex);
                await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
                return;
            }

            // Command Worked. Log to console who used it and what command was
            if (result.IsSuccess)
            {
                Console.WriteLine($"Command: {context.User.Username} used {command.Value.Name}");
                return;
            }

            // Command failed. Notify user that something happened
            _logger.Log($"Command Error: {result}");
            spaceIndex = $"{result}".IndexOf(' ');
            cleanResult = $"{result}".Substring(spaceIndex);
            await context.Channel.SendMessageAsync(embed: EmbedHandler.Alert(cleanResult));
        }
    }
}