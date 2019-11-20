using System.Threading.Tasks;
using Discord.Commands;

namespace kBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync("", false, EmbedHandler.Normal(message));
        }
    }
}
