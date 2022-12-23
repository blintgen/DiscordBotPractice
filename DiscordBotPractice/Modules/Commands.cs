using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardPicklerBot.Repositories;

namespace DiscordTestBot.Modules
{
    public class CommandInteraction : InteractionModuleBase
    {
        Deck<string> activeDeck;

        public override Task BeforeExecuteAsync(ICommandInfo command)
        {
            IDeckRepo deckRepo = RepoFactory.GetDeckRepo();

            activeDeck = deckRepo.GetDeck(this.Context.User.Id);

            return base.BeforeExecuteAsync(command);
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
