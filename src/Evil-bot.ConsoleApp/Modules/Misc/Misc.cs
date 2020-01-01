using Bot.Handlers;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Modules.Misc
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordSocketClient _client;
        private readonly DiscordSocketClient _guild;
        private readonly Config _config;
        private readonly LogHandler _logger;


        public SocketTextChannel LogChannel
        {
            get
            {
                var guild = _client.GetGuild(446832659377946625);
                var channel = guild.GetTextChannel(Convert.ToUInt64(_config.LogChannelID));
                return channel;
            }
        }


        public SocketGuild Guild
        {
            get
            {
                var guild = _client.GetGuild(446832659377946625);
                return guild;
            }
        }

        public SocketUser Owner => Guild.Owner;

        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync(embed: EmbedHandler.Update(message));
        }

        // [Command("test")]
        // public async Task Test()
        // {
        //     var message = "User Updated";
        //     await LogChannel.SendMessageAsync(embed: EmbedHandler.Update(message, Owner, Owner));
        // }

        [Command("pick")]
        public async Task Pick([Remainder]string message)
        {
            var options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var r = new Random();
            var selection = options[r.Next(0, options.Length)];

            await ReplyAsync(embed: EmbedHandler.Neutral(selection));
        }

        // [Command("secret")]
        // public async Task RevealSecret([Remainder]string arg = "")
        // {
        //     if (!UserIsSecretRole((SocketGuildUser)Context.User))
        //     {
        //         await ReplyAsync(embed: EmbedHandler.NoPermission(Context.User.Username));
        //         return;
        //     }
        //     var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
        //     await dmChannel.SendMessageAsync(DataHandler.GetAlert("SECRET"));
        //     await ReplyAsync(DataHandler.GetAlert("SECRET"));
        // }

        private bool UserIsSecretRole(SocketGuildUser user)
        {
            var targetRoleName = "SecretRole";
            var result = from r in user.Guild.Roles
                         where r.Name == targetRoleName
                         select r.Id;
            var roleID = result.FirstOrDefault();
            if (roleID == 0) return false;
            var targetRole = user.Guild.GetRole(roleID);
            return user.Roles.Contains(targetRole);
        }
    }
}