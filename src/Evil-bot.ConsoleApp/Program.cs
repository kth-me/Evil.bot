using System.Threading.Tasks;

namespace Bot
{
    internal class Program
    {
        private static Task Main()
            => new Client().InitializeAsync();
    }
}