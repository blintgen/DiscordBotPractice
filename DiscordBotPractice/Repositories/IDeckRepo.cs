using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPicklerBot.Repositories
{
    internal interface IDeckRepo
    {
        Deck<string> GetDeck(ulong id);
    }
}
