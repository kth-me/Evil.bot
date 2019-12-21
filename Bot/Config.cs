using System;
using System.IO;
using Newtonsoft.Json;

namespace Bot
{
    class Config
    {
        private const string dataFolder = "Data";
        private const string configFile = "config.json";

        public static BotConfig bot;

        static Config()
        {
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            if (!File.Exists(dataFolder + "/" + configFile))
            {
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(dataFolder + "/" + configFile, json);
                Console.ReadKey();
            }
            else
            {
                string json = File.ReadAllText(dataFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
    }
}