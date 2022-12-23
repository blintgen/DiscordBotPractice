using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicklingBot
{
    public class Deck<T> : IEnumerable<T>
    {
        private List<T> Cards { get; set; } = new List<T>();
        private List<T> CardsToShuffle = new List<T>();

        public Deck(List<T> initial)
        {
            Cards.AddRange(initial);
        }


        public void Shuffle()
        {
            Random random = new Random();

            foreach(T card in CardsToShuffle)
            {
                Cards.Add(card);
            }

            for (int i = Cards.Count; i > 0; i--)
            {
                int j = random.Next(i);
                T value = Cards[j];
                Cards.RemoveAt(j);
                Cards.Add(value);
            };
            CardsToShuffle.Clear();
        }

        public T Draw()
        {
            if (Cards.Count < 1)
                {
                    Shuffle();
                }
            var drawnCard = Cards[0];
            Cards.RemoveAt(0);
            CardsToShuffle.Add(drawnCard);
            return drawnCard;
        }

        public List<T> Draw(int count)
        {
            List<T> cardsDrawn = new List<T>();

            for (int i = 0; i < count; i++)
            {               
                cardsDrawn.Add(Draw());
            }
            return cardsDrawn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class Deck
    {
        public static Deck<string> CreateStandard52()
        {
            List<string> list = new List<string>();

            int cardsInSuit = 13;

            for (int i = 0; i < 4; i++)
            {
                string suit;

                if (i == 0)
                {
                    suit = "♠";
                }

                else if (i == 1)
                {
                    suit = "♣";
                }

                else if (i == 2)
                {
                    suit = "♥";
                }

                else if (i == 3)
                {
                    suit = "♦";
                }

                else
                {
                    suit = "you fucked up";
                }

                for (int j = 1; j < cardsInSuit + 1; j++)
                {
                    string value = j.ToString();

                    if (value == "1") { value = "A"; }
                    if (value == "11") { value = "J"; }
                    if (value == "12") { value = "Q"; }
                    if (value == "13") { value = "K"; }

                    list.Add(value + suit);
                }
            }

            return new Deck<string>(list);
        }
    }
}
