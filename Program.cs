using System;
using System.Linq;
using System.Security.Cryptography;
using CardJitsu.Extensions;
using CardJitsu.Models;

namespace CardJitsu
{
    class Program
    {
        static void Main(string[] args)
        {
            Player person = new Player();
            Player cpu = new Player();
            person.Shuffle();
            cpu.Shuffle();

            person.DrawCards(3);
            cpu.DrawCards(3);
            while (person.HasCardsLeftInHand && cpu.HasCardsLeftInHand)
            {
                Console.WriteLine(cpu.Deck.ToStringHidden());
                Console.Write(string.Concat(person.Deck.ToStringHidden().Split('\n').Select(x => "                    "+x+"\n")));
                Console.WriteLine(person.Hand.ToStringShown());

                Console.Write("Which card? ");
                string command = Console.ReadLine();
                
                int cardindex = -1;

                while (!int.TryParse(command, out cardindex))
                {
                    Console.Write("I didn't understand. Which card? ");
                    command = Console.ReadLine();
                }

                Card playercard = person.PlayCard(cardindex);
                
                if (cpu.Hand.Count == 1)
                    cardindex = 0;
                else
                    cardindex = RandomNumberGenerator.GetInt32(0, cpu.Hand.Count - 1);
                Card cpucard = cpu.PlayCard(cardindex);

                string playercardString = playercard.ToString();
                string cpucardString = cpucard.ToString();
                Console.WriteLine("  YOU         CPU");
                Console.WriteLine(string.Concat(playercardString.Split('\n').Zip(cpucardString.Split('\n'), (x, y) => (x, y)).Select(x => x.x+"   "+x.y+"\n")));
                Console.Write($"You play {playercard.Element} {playercard.Number}. Your opponent plays {cpucard.Element} {cpucard.Number}. ");
                if(playercard == cpucard)
                    Console.WriteLine("It's a tie!");
                else if(playercard > cpucard)
                    Console.WriteLine(("You win the round!"));
                else if(playercard < cpucard)
                    Console.WriteLine(("You lose the round."));
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                if(person.HasCardsLeft)
                    person.DrawCard();
                if(cpu.HasCardsLeft)
                    cpu.DrawCard();
            }
        }
    }
}