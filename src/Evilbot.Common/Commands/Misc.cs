using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.CommandsExtension;
using Discord.Commands;
using Discord.WebSocket;
using Evilbot.Common.Models;
using Evilbot.ConsoleUI;

namespace Evilbot.Common.Commands
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private readonly ConfigModel _config;
        private readonly CommandService _commandService;
        
        public Misc(CommandService commandService)
        {
            _commandService = commandService;
            _config ??= new ConfigHandler().GetConfig();
        }

        [Command("help"), Summary("Show help menu")]
        public async Task Help([Remainder] string command = null)
        {
            var helpEmbed = _commandService.GetDefaultHelpEmbed(command, _config.Prefix);
            await Context.Channel.SendMessageAsync(embed: helpEmbed);
        }

        [Command("echo"), Summary("Repeat entered text")]
        public async Task Echo([Remainder]string message)
        {
            await Context.Channel.SendMessageAsync($">>> {message}");
        }

        [Command("whois")]
        [Remarks("Get detailed user information")]
        public async Task WhoIs(SocketGuildUser user)
        {
            // Need to better integrate this with EmbedHandler without
            // code becoming too tightly coupled

            var embed = new EmbedBuilder();

            embed.WithTitle($"ℹ WHOIS");
            embed.WithColor(new Color(59, 136, 195));
            embed.AddField($"Mention", user.Mention, false);
            embed.AddField($"Username", $"{user.Username}#{user.Discriminator}", true);
            if (user.Nickname != null)
                embed.AddField($"Nickname", user.Nickname, true);
            embed.AddField($"ID", user.Id, true);
            embed.AddField($"Status", user.Status, false);
            embed.WithCurrentTimestamp();

            var rolesList = new List<string>();
            foreach (var role in user.Roles)
            {
                rolesList.Add(role.Name);
            }
            // rolesList = rolesList.Where(role =>

            // var rolesString = String.Join(", ", rolesList.ToArray());
            // embed.AddField($"Role", rolesString, false);

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

        // private bool UserIsSecretRole(SocketGuildUser user)
        // {
        //     var targetRoleName = "SecretRole";
        //     var result = from r in user.Guild.Roles
        //                  where r.Name == targetRoleName
        //                  select r.Id;
        //     var roleID = result.FirstOrDefault();
        //     if (roleID == 0) return false;
        //     var targetRole = user.Guild.GetRole(roleID);
        //     return user.Roles.Contains(targetRole);
        // }

        
    }
}