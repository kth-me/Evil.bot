using Discord;

namespace Bot
{
    class EmbedHandler
    {
        public static EmbedBuilder Embed { get; } = new EmbedBuilder();

        public static dynamic Default(string message)
        {
            Embed.WithDescription(message);
            return Embed.Build();
        }

        public static dynamic Good(string message)
        {
            string icon = "✅";
            Embed.WithDescription($"{icon} {message}");
            Embed.WithColor(new Color(119, 178, 85));
            return Embed.Build();
        }

        public static dynamic Bad(string message, string user)
        {
            string icon = "⛔";
            Embed.WithDescription($"{icon} {message}");
            Embed.WithColor(new Color(190, 25, 49));
            Embed.WithFooter(user);
            return Embed.Build();
        }

        public static dynamic Info(string message)
        {
            string icon = "ℹ";
            Embed.WithDescription($"{icon} {message}");
            Embed.WithColor(new Color(59, 136, 195));
            return Embed.Build();
        }

        public static dynamic Alert(string message)
        {
            string icon = "⚠";
            Embed.WithDescription($"{icon} {message}");
            Embed.WithColor(new Color(255, 204, 76));
            return Embed.Build();
        }

        public static dynamic PermissionDenied(string user)
        {
            string icon = "⛔";
            Embed.WithDescription($"{icon} Permission denied");
            Embed.WithColor(new Color(190, 25, 49));
            Embed.WithFooter(user);
            return Embed.Build();
        }
    }
}