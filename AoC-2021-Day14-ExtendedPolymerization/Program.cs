using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day14_ExtendedPolymerization
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DoIt("inputExample.txt");
            //DoIt("input.txt");


            Console.WriteLine("Hit any key to continue");
            Console.ReadKey();
        }

        public static void DoIt(string inputFileName)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Processing {inputFileName}");
            string[] lines = File.ReadAllLines(inputFileName);

            Console.WriteLine($"Processing input from {inputFileName}");
            //Console.WriteLine($"testing : {Part1(lines, 5, true)}");
            //Console.WriteLine($"Part 1 : {Part1(lines, 10)}");
            Console.WriteLine($"Part 2 : {Part2(lines, 1)}");
            //Console.WriteLine($"Part 2 : {Part2(lines, 40)}");
        }

        public static long Part1(string[] lines, int insertions, bool verbose = false)
        {
            string sequence = lines[0];
            Dictionary<string, string> replacements = CreateReplacements(lines.Skip(2).ToArray());

            for (int insertion = 0; insertion < insertions; insertion++)
            {
                Console.WriteLine($"Insertion {insertion}. sequence is {sequence.Length} characters long");

                string newLine = "";

                for (int i = 0; i < sequence.Length - 1; i++)
                {
                    newLine += sequence[i];
                    if (replacements.Keys.Contains(sequence.Substring(i, 2)))
                    {
                        newLine += replacements[sequence.Substring(i, 2)];
                    }
                }
                newLine += sequence.Last();
                sequence = newLine;

                if (verbose)
                {
                    Console.WriteLine(sequence);
                }
            }

            Dictionary<char, int> charCounts = sequence.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return charCounts.Values.Max() - charCounts.Values.Min();
        }

        public static Dictionary<string, string> CreateReplacements(string[] lines)
        {
            Dictionary<string, string> results = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                results.Add(parts[0], parts[2]);
            }

            Console.WriteLine($"There are {results.Count} possible pair insertions");
            return results;
        }

        public static long Part2(string[] lines, int insertions)
        {
            string sequence = lines[0];
            Dictionary<string, string> replacements = CreateReplacements(lines.Skip(2).ToArray());
            Dictionary<string, long> charPairCounts = InitialiseCharPairCounts(sequence, replacements);

            // iterate by going through each replacement, decrementing the start & incrementing the 2 replcements.
            // at the end, count the first character of each pair & add one to the count of the character that comes last?

            for (int insertion = 0; insertion < insertions; insertion++)
            {
                Console.WriteLine($"Insertion {insertion}.");

                for (int i= 0 ; i< charPairCounts.Count ; i++)
                {
                    var pair = charPairCounts.ElementAt(i).Key;
                    if (charPairCounts[pair] > 0)
                    {
                        if (replacements.Keys.Contains(pair))
                        {
                            charPairCounts[pair]--;
                            string pair1 = $"{pair.First()}{replacements[pair]}";
                            string pair2 = $"{replacements[pair]}{pair.Last()}";
                            charPairCounts[pair1]++;
                            charPairCounts[pair2]++;
                        }
                    }
                }
            }

            return Score(charPairCounts);
        }

        private static long Score(Dictionary<string, long> charPairCounts)
        {
            Dictionary<char, long> charCounts = new Dictionary<char, long>();

            foreach (var charPair in charPairCounts)
            {
                if (charCounts.ContainsKey(charPair.Key[0]))
                {
                    charCounts[charPair.Key[0]] += charPair.Value;
                }
                else
                {
                    charCounts.Add(charPair.Key[0], charPair.Value);
                }
            }

            return charCounts.Values.Max() - charCounts.Values.Min();
        }

        public static Dictionary<string, long> InitialiseCharPairCounts(string sequence, Dictionary<string, string> replacements)
        {
            var result = new Dictionary<string, long>();

            foreach (var replacement in replacements.Keys)
            {
                result.Add(replacement, 0);
            }

            for (int i = 0; i < sequence.Length - 1; i++)
            {
                string pair = sequence.Substring(i, 2);
                if (result.Keys.Contains(pair))
                {
                    result[pair]++;
                }
                else
                {
                    result.Add(pair, 1);
                }
            }

            return result;
        }
    }
}
