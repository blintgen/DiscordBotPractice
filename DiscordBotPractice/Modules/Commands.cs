using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicklingBot;

namespace DiscordTestBot.Modules
{
    public Deck<string>? activeDeck = null;
    public Deck<string>? previousDeck = null;

    public class CommandInteraction : InteractionModuleBase
    {
        [SlashCommand("create52", "Generates and sets the current deck as a standard 52 card deck")]
        public async Task Create52()
        {
            if (activeDeck != null)
            {
                this.previousDeck = this.activeDeck;
            }

            this.activeDeck = Deck.CreateStandard52();
            await RespondAsync("Standard 52 card deck generated");
        }


        [SlashCommand("draw", "draws cards from the current deck.")]
        public async Task Draw( [Summary(description:"Draw more than 1 card")] [MaxValue(52)][MinValue(1)] int count = 1)
        {
            var drew = this.activeDeck.Draw(count);
            var drewMessage = string.Join("\n", drew);
            await RespondAsync(drewMessage);
        }

        [SlashCommand("shuffle", "Adds back any drawn cards and shuffles the deck.")]
        public async Task Shuffle()
        {
            this.activeDeck.Shuffle();
            await RespondAsync("Shuffled!");
        }
    }
}
