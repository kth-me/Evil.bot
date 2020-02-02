using Discord;

namespace Evilbot.Common.Logging
{
    public interface IEventLogger
    {
        void Log(LogStyle logStyle, string message = null, LogMessage logMessage = new LogMessage());
    }
}