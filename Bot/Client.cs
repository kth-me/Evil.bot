using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Bot.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Bot
{
    internal class Client
    {
        private DiscordSocketClient _client;
        private CommandHandler _commands;
        private IServiceProvider _services;
        private Config _config;
        private LogHandler _logHandler;

        // Initialize client and config
        public Client(CommandHandler commands = null, DiscordSocketClient client = null, Config config = null, LogHandler logHandler = null)
        {
            // Create new DiscordClient (setting LogSeverity to Verbose)
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            });

            // Create new CommandHandler

            // Get config data from config.json
            _config = config ?? new ConfigHandler().GetConfig();

            // Set up logHandler
            _logHandler = logHandler ?? new LogHandler();
        }

        public async Task StartAsync()
        {
            // Check for presence of bot token
            if (string.IsNullOrEmpty(_config.Token))
                return;

            // Login with the client
            await _client.LoginAsync(TokenType.Bot, _config.Token);
            
            // Start the client
            await _client.StartAsync();
            
            // Hook up events
            HookEvents();

            // Initialize CommandHandler Handler
            _commands = new CommandHandler();
            await _commands.InitializeAsync(_client);

            // Prevent bot from shutting down instantly
            await Task.Delay(-1);
        }

        // Hook up any events we want to use
        private void HookEvents()
        {
            _client.Log += LogAsync;
            _client.Ready += OnReadyAsync;
        }

        //When client sends event indicating it is ready, set the Now Playing to what is in config.json
        private async Task OnReadyAsync()
        {
            // await _client.SetGameAsync(_config.GameStatus);
            await _client.SetGameAsync("Test Status");
        }

        private async Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton<CommandHandler>()
                .AddSingleton<ConfigHandler>()
                .AddSingleton<LogHandler>()
                .BuildServiceProvider();
        }
    }
}