using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using PicklingBot;

namespace CardPicklerBot
{
    internal class Bot
    {
        private DiscordSocketClient _client;
        private InteractionService _interactionService;


        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        Deck<string> activeDeck = Deck.CreateStandard52();

        public async Task Connect()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += Client_Ready;
            _interactionService = new InteractionService(_client.Rest);

            var token = File.ReadAllText("token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {

            var modules = await _interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), null);
            ///var guild = _client.GetGuild(1016076815992889414);
            ulong guild = 1016076815992889414;
            var commands = await _interactionService.RegisterCommandsToGuildAsync(guild, true);


            _client.InteractionCreated += async interaction =>
            {
                var ctx = new SocketInteractionContext(_client, interaction);
                await _interactionService.ExecuteCommandAsync(ctx, null);
            };
        }
    }
}
