using System;
using System.Collections.Generic;
using CardJitsu.Extensions;

namespace CardJitsu.Models
{
    public class Card
    {
        public CardColor Color { get; set; }
        public int Number { get; set; }
        public CardElement Element { get; set; }

        public Card(CardElement element, int number, CardColor color)
        {
            Color = color;
            Number = number;
            Element = element;
        }
        
        public static List<Card> StarterDeck() => new List<Card>
        {
            new Card(CardElement.Fire, 3, CardColor.Blue),
            new Card(CardElement.Fire, 6, CardColor.Purple),
            new Card(CardElement.Fire, 2, CardColor.Yellow),
            new Card(CardElement.Ice, 3, CardColor.Orange),
            new Card(CardElement.Ice, 2, CardColor.Red),
            new Card(CardElement.Ice, 7, CardColor.Yellow),
            new Card(CardElement.Water, 5, CardColor.Blue),
            new Card(CardElement.Water, 2, CardColor.Green),
            new Card(CardElement.Water, 4, CardColor.Purple)
        };

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            try { return (Card)obj == this; }
            catch (InvalidCastException) { return false; }
        }

        //public static bool operator ==(Card left, Card right) => (left == null && right == null) || left != null && right != null && left.Element == right.Element && left.Number == right.Number;
        public static bool operator ==(Card left, Card right)
        {
            try
            {
                return left.Element == right.Element && left.Number == right.Number;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }

        public static bool operator !=(Card left, Card right) => !(left == right);

        public static bool operator >(Card left, Card right)
        {
            if (left == null)
                throw new ArgumentNullException(nameof(left));
            if (right == null)
                throw new ArgumentNullException(nameof(right));
            if (left.Element == right.Element)
                return left.Number > right.Number;
            switch (left.Element)
            {
                case CardElement.Fire:
                    return right.Element == CardElement.Ice;
                case CardElement.Ice:
                    return right.Element == CardElement.Water;
                case CardElement.Water:
                    return right.Element == CardElement.Fire;
            }
            throw new Exception("This part should not be reachable");
        }
        public static bool operator <(Card left, Card right)
        {
            return !(left > right) && !(left == right);
        }

        public static bool operator >=(Card left, Card right) => left > right || left == right;
        public static bool operator <=(Card left, Card right) => left < right || left == right;

        public override string ToString()
        {
            return @"┌─────┐  "+ "\n" +
                   $"│  {Element.ToSymbol()}  │  " + "\n" +
                   $"│  {Number}  │  " + "\n" +
                   $"│  {Color.ToSymbol()}  │  " + "\n" +
                   @"└─────┘  ";
        }
    }
}