using Bot.Handlers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using EventHandler = Bot.Handlers.EventHandler;

namespace Bot
{
    internal class Client
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly Config _config;
        private readonly LogHandler _logger;
        private IServiceProvider _services;

        // Initialize client and config
        public Client(CommandService commands = null, Config config = null, LogHandler logger = null)
        {
            // Create new DiscordClient
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            // Create new CommandService (setting RunMode to async by default on all commands)
            _commands = commands ?? new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Verbose
            });

            // Get config data from config.json
            _config = config ?? new ConfigHandler().GetConfig();

            // Set up logHandler
            _logger = logger ?? new LogHandler();
        }

        public async Task InitializeAsync()
        {
            // Check for presence of bot token
            if (string.IsNullOrEmpty(_config.Token))
                return;

            //Set up services
            _services = ConfigureServices();

            // Login with the client
            await _client.LoginAsync(TokenType.Bot, _config.Token);

            // Start the client
            await _client.StartAsync();

            // Hook up events
            HookEvents();

            // Initialize CommandHandler
            await _services.GetRequiredService<CommandHandler>().InitializeAsync();

            // Initialize EventHandler
            _services.GetRequiredService<EventHandler>().InitializeEvents();

            // Prevent bot from shutting down instantly
            await Task.Delay(-1);
        }

        // Hook up any command specific events
        private void HookEvents()
        {
            _client.Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        // When client sends event indicating it is ready, set the Now Playing to what is in config.json
        private async Task OnReadyAsync()
        {
            await _client.SetGameAsync(_config.Status);
        }

        // Display log messages to the console.
        private Task LogAsync(LogMessage log)
        {
            _logger.Neutral(log.Message);
            return Task.CompletedTask;
        }

        // Used to add any services to the DI Service Provider
        // These services are then injected wherever they're needed
        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton<CommandHandler>()
                .AddSingleton<EventHandler>()
                .AddSingleton<ConfigHandler>()
                .AddSingleton<LogHandler>()
                .BuildServiceProvider();
        }
    }
}