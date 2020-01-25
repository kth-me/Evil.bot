using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic;

namespace Evilbot.Common.Plugins
{
    public class PluginLoader
    {
        public static List<IPlugin> Plugins { get; set; }

        public void LoadPlugins()
        {
            Plugins = new List<IPlugin>();

            // Load assemblies from Plugins directory
            if (Directory.Exists("Plugins"))
            {
                string[] files = Directory.GetFiles("Plugins");
                foreach (string file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            // Fetch all types that implement the interface IPlugin and are a class
            Type interfaceType = typeof(IPlugin);
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();
            
            // Create new instance of all found types
            foreach (Type type in types)
            {
                Plugins.Add((IPlugin)Activator.CreateInstance(type));
            }
        }
    }
}