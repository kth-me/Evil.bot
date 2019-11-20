using System;
using Discord;

namespace kBot
{
    class EmbedHandler
    {
        static EmbedBuilder embed = new EmbedBuilder();

        public static dynamic Normal(string message)
        {
            embed.WithDescription(message);
            return embed.Build();
        }

        public static dynamic Good(string message)
        {
            string icon = "✅";
            embed.WithDescription(String.Format("{0} {1}", icon, message));
            embed.WithColor(new Color(119, 178, 85));
            return embed.Build();
        }

        public static dynamic Bad(string message)
        {
            string icon = "⛔";
            embed.WithDescription(String.Format("{0} {1}", icon, message));
            embed.WithColor(new Color(190, 25, 49));
            return embed.Build();
        }

        public static dynamic Info(string message)
        {
            string icon = "ℹ";
            embed.WithDescription(String.Format("{0} {1}", icon, message));
            embed.WithColor(new Color(59, 136, 195));
            return embed.Build();
        }

        public static dynamic Alert(string message)
        {
            string icon = "⚠";
            embed.WithDescription(String.Format("{0} {1}", icon, message));
            embed.WithColor(new Color(255, 204, 76));
            return embed.Build();
        }
    }
}
