using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bot.Handlers
{
    internal class DataHandler
    {
        private static Dictionary<string, string> alerts;

        static DataHandler()
        {
            string json = File.ReadAllText("Data/alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key))
            {
                return alerts[key];
            }
            return "";
        }

        public static string GetFormattedAlert(string key, params object[] parameter)
        {
            if (alerts.ContainsKey(key))
            {
                return string.Format(alerts[key], parameter);
            }
            return "";
        }

        /*public static string GetFormattedAlert(string key, object parameter)
        {
            return GetFormattedAlert(key, new object[] { parameter });
        }*/
    }
}