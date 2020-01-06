using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Bot.Handlers
{
    internal class DataHandler
    {
        private static readonly Dictionary<string, string> _alerts;

        static DataHandler()
        {
            var json = File.ReadAllText("Data/data.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            _alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            return _alerts.ContainsKey(key) ? _alerts[key] : "";
        }

        public static string GetFormattedAlert(string key, params object[] parameter)
        {
            return _alerts.ContainsKey(key) ? string.Format(_alerts[key], parameter) : "";
        }

        /*public static string GetFormattedAlert(string key, object parameter)
        {
            return GetFormattedAlert(key, new object[] { parameter });
        }*/
    }
}