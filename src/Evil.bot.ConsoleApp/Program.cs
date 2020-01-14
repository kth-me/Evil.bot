namespace Evil.bot.ConsoleApp
{
    using System.Threading.Tasks;

    internal class Program
    {
        private static Task Main()
            => new Client().InitializeAsync();
    }
}