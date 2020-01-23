namespace Evilbot.ConsoleUI.Handlers
{
    using Discord;
    using Discord.Commands;
    using Discord.WebSocket;
    using Evilbot.ConsoleUI.Models;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ConfigModel _config;
        private readonly LogHandler _logger;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Allows us to get everything need from DI when class is instantiated.
        /// </summary>
        /// <returns></returns>
        public CommandHandler(IServiceProvider services, DiscordSocketClient client, CommandService commands, LogHandler logger)
        {
            _client = client;
            _commands = commands;
            _config ??= new ConfigHandler().GetConfig();
            _services = services;
            _logger = logger;
        }

        // Task called to create command service
        public async Task InitializeAsync()
        {
            _client.MessageReceived += OnMessageReceivedAsync;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), _services);
        }

        private async Task OnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // Don't process command if it was anything other than a user message (ie. system message)
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

            // Delete command message
            await message.DeleteAsync();

            // Execute command with command context along with service provider for precondition checks
            // Result indicates an object stating if command executed successfully (not a return value)
            var result = await _commands.ExecuteAsync(context: context, argPos: argPos, services: _services);
        }
    }
}