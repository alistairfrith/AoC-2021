using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AoC_2021_Day4
{

    class Program
    {
        public static void Main(string[] args)
        {
            DoIt("inputExample.txt");
            DoIt("input.txt");

            Console.WriteLine("Hit any key to continue");
            Console.ReadKey();
        }

        public static void DoIt(string inputFileName)
        {
            string[] lines = File.ReadAllLines(inputFileName);

            Console.WriteLine($"Processing input from {inputFileName}");

            string programName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
            Console.WriteLine($"\t{programName} Part 1 : {Part1(lines)}");
            Console.WriteLine($"\t{programName} Part 2 : {Part2(lines)}");
            Console.WriteLine();
        }

        public static int Part1(string[] lines)
        {
            string[] calls = lines[0].Split(',');

            var cards = PopulateCards(lines.Skip(2));

            return Play(calls, cards);
        }

        private static int Play(string[] calls, List<BingoCard> cards)
        {
            foreach (string call in calls)
            {
                foreach (BingoCard card in cards)
                {
                    card.Mark(call);
                    if (card.CompleteLine())
                    {
                        Console.WriteLine(card.ToString());
                        return card.Score(call);
                    }
                }

            }

            return 0;
        }

        private static void PrintCards(List<BingoCard> cards)
        {
            foreach (var card in cards)
            {
                Console.WriteLine (card.ToString());
            }
        }

        private static List<BingoCard> PopulateCards(IEnumerable<string> lines)
        {
            List<BingoCard> cards = new List<BingoCard>();

            while (lines.Count() > 0)
            {
                cards.Add(new BingoCard(lines.Take(5)));
                lines = lines.Skip(6);
            }

            return cards;
        }

        public static int Part2(string[] lines)
        {
            return 0;
        }
    }
}