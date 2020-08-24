using System;
using System.Collections.Generic;
using System.Linq;
using CardJitsu.Extensions;

namespace CardJitsu.Models
{
    public class Player
    {
        public List<Card> Deck { get; set; } = Card.StarterDeck();

        public bool HasCardsLeft => Deck.Count > 0;
        
        public List<Card> Hand { get; set; } = new List<Card>();
        public bool HasCardsLeftInHand => Hand.Count > 0;

        public void DrawCard()
        {
            try
            {
                Hand.Add(Deck[0]);
                Deck.RemoveAt(0);
            }
            catch (IndexOutOfRangeException)
            {
                throw new OutOfCardsException();
            }
        }
        public void DrawCards(int x)
        {
            bool tooMany = x > Deck.Count;
            if (tooMany)
                x = Deck.Count;
            while (x > 0)
            {
                DrawCard();
                x--;
            }
            if(tooMany)
                throw new OutOfCardsException();
        }

        public Card PlayCard(int index)
        {
            try
            {
                Card card = Hand[index];
                Hand.RemoveAt(index);
                return card;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }
        public Card PlayCard(Card card)
        {
            if (!Hand.Contains(card)) return null;
            Hand.Remove(card);
            return card;
        }
        public Card PlayCard(CardElement element, int number)
        {
            Card card = Hand.FirstOrDefault(x => x.Element == element && x.Number == number);
            if (card != null)
                Hand.Remove(card);
            return card;
        }

        public void Shuffle()
        {
            Deck.Shuffle();
        }
    }

    public class OutOfCardsException : Exception {}
}