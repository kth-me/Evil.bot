using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Evilbot.Common.Logging
{
    public interface IEventLogger
    {
        public async Task ChannelCreated(SocketChannel channel)
        {
        }

        public async Task ChannelDestroyed(SocketChannel channel)
        {
        }

        public async Task ChannelUpdated(SocketChannel channelBefore, SocketChannel channelAfter)
        {
        }

        public async Task Connected()
        {
        }

        public async Task CurrentUserUpdated(SocketSelfUser userBefore, SocketSelfUser userAfter)
        {
        }

        public async Task Disconnected(Exception exception)
        {
        }

        public async Task GuildAvailable(SocketGuild guild)
        {
        }

        public async Task GuildMembersDownloaded(SocketGuild guild)
        {
        }

        public async Task GuildMemberUpdated(SocketGuildUser userBefore, SocketGuildUser userAfter)
        {
        }

        public async Task GuildUnavailable(SocketGuild guild)
        {
        }

        public async Task GuildUpdated(SocketGuild guildBefore, SocketGuild guildAfter)
        {
        }

        public async Task JoinedGuild(SocketGuild guild)
        {
        }

        public async Task LatencyUpdated(int latencyBefore, int latencyAfter)
        {
        }

        public async Task LeftGuild(SocketGuild guild)
        {
        }

        public async Task Log(LogMessage logMessage)
        {
        }

        public async Task LoggedIn()
        {
        }

        public async Task LoggedOut()
        {
        }

        public async Task MessageDeleted(Cacheable<IMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        public async Task MessageReceived(SocketMessage message)
        {
        }

        public async Task MessageUpdated(Cacheable<IMessage, ulong> cacheMessageBefore, SocketMessage messageAfter, ISocketMessageChannel channel)
        {
        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public async Task ReactionRemoved(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel, SocketReaction reaction)
        {
        }

        public async Task ReactionsCleared(Cacheable<IUserMessage, ulong> cacheMessage, ISocketMessageChannel channel)
        {
        }

        public async Task Ready()
        {
        }

        public async Task RecipientAdded(SocketGroupUser user)
        {
        }

        public async Task RecipientRemoved(SocketGroupUser user)
        {
        }

        public async Task RoleCreated(SocketRole role)
        {
        }

        public async Task RoleDeleted(SocketRole role)
        {
        }

        public async Task RoleUpdated(SocketRole roleBefore, SocketRole roleAfter)
        {
        }

        public async Task UserBanned(SocketUser user, SocketGuild guild)
        {
        }

        public async Task UserIsTyping(SocketUser user, ISocketMessageChannel channel)
        {
        }

        public async Task UserJoined(SocketGuildUser user)
        {
        }

        public async Task UserLeft(SocketGuildUser user)
        {
        }

        public async Task UserUnbanned(SocketUser user, SocketGuild guild)
        {
        }

        public async Task UserUpdated(SocketUser oldUser, SocketUser newUser)
        {
        }

        public async Task UserVoiceStateUpdated(SocketUser user, SocketVoiceState voiceStateBefore, SocketVoiceState voiceStateAfter)
        {
        }
        
        public async Task CommandLog(LogMessage logMessage)
        {
        }

        public async Task CommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
        }
    }
}