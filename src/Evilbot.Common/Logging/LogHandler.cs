using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Evilbot.Common.Models;
using Evilbot.ConsoleUI;

namespace Evilbot.Common.Logging
{
    public class LogHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly List<IEventLogger> _loggers;

        public LogHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
            _loggers = new List<IEventLogger>();
            _loggers.Add(new ConsoleTextLogger());
        }

        public async Task InitializeAsync()
        {
            foreach (var logger in _loggers)
            {
                _client.ChannelCreated += logger.ChannelCreated;
                _client.ChannelDestroyed += logger.ChannelDestroyed;
                _client.ChannelUpdated += logger.ChannelUpdated;
                _client.Connected += logger.Connected;
                _client.CurrentUserUpdated += logger.CurrentUserUpdated;
                _client.Disconnected += logger.Disconnected;
                _client.GuildAvailable += logger.GuildAvailable;
                _client.GuildMembersDownloaded += logger.GuildMembersDownloaded;
                _client.GuildMemberUpdated += logger.GuildMemberUpdated;
                _client.GuildUnavailable += logger.GuildUnavailable;
                _client.GuildUpdated += logger.GuildUpdated;
                _client.JoinedGuild += logger.JoinedGuild;
                _client.LatencyUpdated += logger.LatencyUpdated;
                _client.LeftGuild += logger.LeftGuild;
                _client.Log += logger.Log;
                _client.LoggedIn += logger.LoggedIn;
                _client.LoggedOut += logger.LoggedOut;
                _client.MessageDeleted += logger.MessageDeleted;
                _client.MessageReceived += logger.MessageReceived;
                _client.MessageUpdated += logger.MessageUpdated;
                _client.ReactionAdded += logger.ReactionAdded;
                _client.ReactionRemoved += logger.ReactionRemoved;
                _client.ReactionsCleared += logger.ReactionsCleared;
                _client.Ready += logger.Ready;
                _client.RecipientAdded += logger.RecipientAdded;
                _client.RecipientRemoved += logger.RecipientRemoved;
                _client.RoleCreated += logger.RoleCreated;
                _client.RoleDeleted += logger.RoleDeleted;
                _client.RoleUpdated += logger.RoleUpdated;
                _client.UserBanned += logger.UserBanned;
                _client.UserIsTyping += logger.UserIsTyping;
                _client.UserJoined += logger.UserJoined;
                _client.UserLeft += logger.UserLeft;
                _client.UserUnbanned += logger.UserUnbanned;
                _client.UserUpdated += logger.UserUpdated;
                _client.UserVoiceStateUpdated += logger.UserVoiceStateUpdated;
                _commands.Log += logger.CommandLog;
                _commands.CommandExecuted += logger.CommandExecuted;
            }
        }
    }
}