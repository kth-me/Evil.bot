using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Handlers;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Modules.Misc
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

        [Command("secret")]
        public async Task RevealSecret([Remainder]string arg = "")
        {
            if (!UserIsSecretRole((SocketGuildUser)Context.User))
            {
                await Context.Channel.SendMessageAsync("", false, EmbedHandler.PermissionDenied(Context.User.Username));
                return;
            }
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(DataHandler.GetAlert("SECRET"));
            await Context.Channel.SendMessageAsync(DataHandler.GetAlert("SECRET"));
        }

        private bool UserIsSecretRole(SocketGuildUser user)
        {
            string targetRoleName = "SecretRole";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            ulong roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}