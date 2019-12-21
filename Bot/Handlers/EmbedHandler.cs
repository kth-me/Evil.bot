using Discord;

namespace Bot.Handlers
{
    internal class EmbedHandler
    {
        private const string _goodIcon = "✅";
        private const string _badIcon = "⛔";
        private const string _infoIcon = "ℹ";
        private const string _alertIcon = "⚠";
        private const string _deniedIcon = "⛔";

        public static EmbedBuilder Embed { get; } = new EmbedBuilder();

        public static dynamic Default(string message)
        {
            Embed.WithDescription(message);
            // Need to specify colour here or will keep colour declared in other methods
            return Embed.Build();
        }

        public static Embed Good(string message)
        {
            Embed.WithDescription($"{_goodIcon} {message}");
            Embed.WithColor(new Color(119, 178, 85));
            return Embed.Build();
        }

        public static Embed Bad(string message)
        {
            Embed.WithDescription($"{_badIcon} {message}");
            Embed.WithColor(new Color(190, 25, 49));
            return Embed.Build();
        }

        public static Embed Info(string message)
        {
            Embed.WithDescription($"{_infoIcon} {message}");
            Embed.WithColor(new Color(59, 136, 195));
            return Embed.Build();
        }

        public static Embed Alert(string message)
        {
            Embed.WithDescription($"{_alertIcon} {message}");
            Embed.WithColor(new Color(255, 204, 76));
            return Embed.Build();
        }

        public static Embed PermissionDenied(string user)
        {
            Embed.WithDescription($"{_deniedIcon} Permission denied");
            Embed.WithColor(new Color(190, 25, 49));
            Embed.WithFooter(user);
            return Embed.Build();
        }
    }
}