namespace Bot
{
    internal class Program
    {
        private static void Main()
            => new Client().StartAsync().GetAwaiter().GetResult();
    }
}