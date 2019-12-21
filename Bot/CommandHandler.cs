using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot
{
    internal class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private readonly IServiceProvider _services;
        private Config _config;
        private readonly Logger _logger;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client;
            _service = new Discord.Commands.CommandService();
            await _service.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
            _client.MessageReceived += HandleCommandAsync;
            
            // Get config data from config.json
            _config = new ConfigHandler().GetConfig();
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null)
            {
                return;
            }
            var context = new SocketCommandContext(_client, msg);
            int argPos = 0;
            if (msg.HasStringPrefix(_config.Prefix, ref argPos)
                || msg.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, null);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}