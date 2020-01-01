namespace Bot
{
    // Structure of config.json file
    public class Config
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string Status { get; set; }
        public string LogChannelID { get; set; }
        public string AdminRoleID { get; set; }
        public string ModRoleID { get; set; }
    }
}