using System;
using System.Linq;
using System.Threading.Tasks;

namespace Evilbot.Common.Plugins
{
    public class PluginHandler
    {
        public async Task InitializeAsync()
        {
            Console.WriteLine("Loading plugins...");
            try
            {
                PluginLoader loader = new PluginLoader();
                loader.LoadPlugins();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Plugins could not be loaded: {e.Message}");
            }

            // Initialize plugins
            foreach (var plugin in PluginLoader.Plugins)
            {
                plugin.StartPlugin();
            }
        }
    }
}