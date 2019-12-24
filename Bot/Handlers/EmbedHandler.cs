using Discord;
using Discord.WebSocket;
using System;

namespace Bot.Handlers
{
    public class EmbedHandler
    {
        private static string _icon;
        private static Color _color;

        private static EmbedBuilder _embed;

        private enum EmbedType
        {
            Neutral,
            Good,
            Bad,
            Info,
            Alert,
            Update
        }

        public static Embed Neutral(string message) => EmbedLogic(EmbedType.Neutral, message);
        public static Embed Good(string message) => EmbedLogic(EmbedType.Good, message);
        public static Embed Bad(string message) => EmbedLogic(EmbedType.Bad, message);
        public static Embed Info(string message) => EmbedLogic(EmbedType.Info, message);
        public static Embed Alert(string message) => EmbedLogic(EmbedType.Alert, message);
        
        public static Embed Update(string message) => EmbedLogic(EmbedType.Update, message);
        public static Embed Update(string message, SocketUser oldUser, SocketUser newUser) => UpdateLogic(message, oldUser, newUser);

        private static Embed EmbedLogic(EmbedType embedType, string message)
        {
            switch (embedType)
            {
                case EmbedType.Neutral:
                    _icon = "";
                    _color = new Color(32, 34, 37);
                    break;
                case EmbedType.Good:
                    _icon = "✅";
                    _color = new Color(119, 178, 85);
                    break;
                case EmbedType.Bad:
                    _icon = "⛔";
                    _color = new Color(190, 25, 49);
                    break;
                case EmbedType.Info:
                    _icon = "ℹ";
                    _color = new Color(59, 136, 195);
                    break;
                case EmbedType.Alert:
                    _icon = "⚠";
                    _color = new Color(255, 204, 76);
                    break;
                case EmbedType.Update:
                    _icon = "⚛️";
                    _color = new Color(146, 102, 204);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(embedType), embedType, null);
            }

            _embed = new EmbedBuilder();
            _embed.WithDescription($"{_icon} {message}");
            _embed.WithColor(_color);
            return _embed.Build();
        }

        private static Embed UpdateLogic(string message, SocketUser oldUser, SocketUser newUser)
        {
            _embed = new EmbedBuilder();
            _embed.WithTitle($"⚛️ {message}");
            _embed.WithDescription($"{newUser.Mention}");
            _embed.AddField("Old", oldUser, true);
            _embed.AddField("New", newUser, true);
            _embed.WithFooter($"{DateTime.Now:HH:mm:ss}");
            _embed.WithColor(new Color(146, 102, 204));
            return _embed.Build();
        }
    }
}