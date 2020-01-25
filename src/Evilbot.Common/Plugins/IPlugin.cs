namespace Evilbot.Common.Plugins
{
    public interface IPlugin
    {
        string Name { get; }

        string Explanation { get; }

        void StartPlugin();
    }
}