using Evil.bot.ConsoleApp.Handlers;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;

namespace Evil.bot.ConsoleApp.Modules.Misc
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private readonly Config _config;
        public DiscordSocketClient Client { get; set; }

        public SocketGuild Guild
        {
            get
            {
                var guild = Client.GetGuild(446832659377946625);
                return guild;
            }
        }

        public SocketTextChannel LogChannel
        {
            get
            {
                var channel = Guild.GetTextChannel(Convert.ToUInt64(_config.LogChannelID));
                return channel;
            }
        }

        public SocketUser Owner => Guild.Owner;

        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync(embed: EmbedHandler.Neutral(message));
        }


        [Command("whois")]
        [Remarks("Get detailed user information")]
        public async Task WhoIs(SocketGuildUser user)
        {

            // Need to better integrate this with EmbedHandler without 
            // code becoming too tightly coupled

            var embed = new EmbedBuilder();

            embed.WithTitle($"ℹ WHOIS");
            embed.WithFooter($"Today at {DateTime.Now:HH:mm:ss}");
            embed.WithColor(new Color(59, 136, 195));
            embed.AddField($"Mention", user.Mention, false);
            embed.AddField($"Username", $"{user.Username}#{user.Discriminator}", true);
            if (user.Nickname != null)
                embed.AddField($"Nickname", user.Nickname, true);
            embed.AddField($"ID", user.Id, true);
            embed.AddField($"Status", user.Status, false);

            foreach (var role in user.Roles)
            {
                embed.AddField($"Role", role, false);
            }

            embed.AddField($"Joined Server", user.JoinedAt, true);
            embed.AddField($"Joined Discord", user.CreatedAt, true);
            embed.WithThumbnailUrl(user.GetAvatarUrl());
            await Context.Channel.SendMessageAsync(embed: embed.Build());


        }

        [Command("pick")]
        [Remarks("Picks between given options")]
        //[RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Pick([Remainder]string message)
        {
            var options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            var r = new Random();
            var selection = options[r.Next(0, options.Length)];

            //await ReplyAsync(embed: EmbedHandler.Neutral(selection));
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