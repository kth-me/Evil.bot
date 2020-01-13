using Evil.bot.ConsoleApp.Handlers;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Evil.bot.ConsoleApp.Models;

namespace Evil.bot.ConsoleApp
{
    internal class Client
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ConfigModel _config;
        private readonly LogHandler _logger;
        private readonly IServiceProvider _services;

        public Client(CommandService commands = null, ConfigModel configModel = null, LogHandler logger = null)
        {
            // Create new DiscordClient
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            // Create new CommandService
            _commands = commands ?? new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Verbose
            });

            // Set up configModel, logger, and services
            _config = configModel ?? new ConfigHandler().GetConfig();
            _logger = logger ?? new LogHandler();
            _services = ConfigureServices();
        }

        public async Task InitializeAsync()
        {
            HookEvents();

            // Login with client and start
            await _client.LoginAsync(TokenType.Bot, _config.Token);
            await _client.StartAsync();

            // Initialize CommandHandler and ClientEventHandler and prevent bot from shutting down instantly
            await _services.GetRequiredService<CommandHandler>().InitializeAsync();
            await _services.GetRequiredService<ClientEventHandler>().InitializeEvents();
            await Task.Delay(-1);
        }

        // Hook up events
        private void HookEvents()
        {
            _client.Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        // When client sends event indicating it is ready, set the Now Playing to what is in configModel.json
        private async Task OnReadyAsync()
        {
            await _client.SetGameAsync(name: _config.PlayingStatus);
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
                .AddSingleton<ClientEventHandler>()
                .AddSingleton<ConfigHandler>()
                .AddSingleton<LogHandler>()
                .BuildServiceProvider();
        }
    }
}