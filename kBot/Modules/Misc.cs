using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace kBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync("", false, EmbedHandler.Default(message));
        }

        [Command("pick")]
        public async Task Pick([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            await Context.Channel.SendMessageAsync("", false, EmbedHandler.Default(selection));
        }
    }

}