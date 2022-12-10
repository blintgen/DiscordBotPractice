using Discord;
using Discord.WebSocket;

namespace CardPicklerBot
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            Bot bot = new();

            await bot.Connect();
        }
    }
}