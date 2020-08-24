using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CardJitsu.Models;

namespace CardJitsu.Extensions
{
    public static class ListExtensions
    {
        public static class ThreadSafeRandom
        {
            [ThreadStatic] private static Random Local;

            public static Random ThisThreadsRandom
            {
                get
                {
                    return Local ??= new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId));
                }
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static string ToSymbol(this CardElement element)
        {
            return element switch
            {
                CardElement.Fire => "F",
                CardElement.Water => "W",
                CardElement.Ice => "I",
                _ => ""
            };
        }
        public static string ToSymbol(this CardColor color)
        {
            return color switch
            {
                CardColor.Blue => "B",
                CardColor.Green => "G",
                CardColor.Orange => "O",
                CardColor.Purple => "P",
                CardColor.Red => "R",
                CardColor.Yellow => "Y",
                _ => ""
            };
        }

        public static string ToStringShown(this IList<Card> list)
        {
      string rT = @"┌──" + String.Concat(Enumerable.Repeat(@"───┐", list.Count)) + "\n";
            rT += @"│  " + String.Concat(list.Select(x => $"{x.Element.ToSymbol()}  │")) + "\n";
            rT += @"│  " + String.Concat(list.Select(x => $"{x.Number}  │")) + "\n";
            rT += @"│  " + String.Concat(list.Select(x => $"{x.Color.ToSymbol()}  │")) + "\n";
            rT += @"└──" + String.Concat(Enumerable.Repeat(@"───┘", list.Count));
            return rT;
        }
        public static string ToStringHidden(this IList<Card> list)
        {
            switch (list.Count)
            {
                case 0:
                    return @"┌─────┐  "+ "\n" +
                           @"│ OUT │  " + "\n" +
                           @"│  o' │  " + "\n" +
                           @"│CARDS│  " + "\n" +
                           @"└─────┘  ";
                case 1:
                    return @"┌─────┐  "+ "\n" +
                           @"│~~~~~│  " + "\n" +
                           @"│~~~~~│  " + "\n" +
                           @"│~~~~~│  " + "\n" +
                           @"└─────┘  ";
                case 2:
                    return @"┌┌─────┐ "+ "\n" +
                           @"││~~~~~│ " + "\n" +
                           @"││~~~~~│ " + "\n" +
                           @"││~~~~~│ " + "\n" +
                           @"└└─────┘ ";
                default:
                    return @"┌┌┌─────┐"+ "\n" +
                           @"│││~~~~~│" + "\n" +
                           @"│││~~~~~│" + "\n" +
                           @"│││~~~~~│" + "\n" +
                           @"└└└─────┘";
            }
        }
    }
}