using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Evilbot.Common.Plugins;

namespace Evilbot.GeneralPlugin
{
    public class General : ModuleBase<SocketCommandContext>, IPlugin
    {
        public string Name { get; } = "General";
        public string Explanation { get; } = "This is the General Plugin.";
        public void StartPlugin()
        {
            Console.WriteLine($"{Name} plugin loaded");
        }

        [Command("test"), Summary("Repeat entered text")]
        public async Task Test([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync($">>> {message}");
        }
    }
}