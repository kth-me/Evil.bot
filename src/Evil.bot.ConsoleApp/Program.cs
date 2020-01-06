using System.Threading.Tasks;

namespace Evil.bot.ConsoleApp
{
    internal class Program
    {
        private static Task Main()
            => new Client().InitializeAsync();
    }
}