using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace A0C_2021_Day7
{
    internal class Program
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
            var costs = Part1(lines[0]);
            Console.WriteLine($"\t{programName} Part 1 : {costs.Item1}");
            Console.WriteLine($"\t{programName} Part 2 : {costs.Item2}");
            Console.WriteLine();
        }

        public static Tuple<int,int> Part1(string line)
        {
            int[] crabPositions = line.Split(',').Select(int.Parse).ToArray();

            int min = crabPositions.Min();
            int max = crabPositions.Max();

            int minCost1 = int.MaxValue;
            int minCost2 = int.MaxValue;
            for (int targetPos = min; targetPos <= max; targetPos++)
            {
                int cost1 = TotalCost1(crabPositions, targetPos);
                minCost1 = Math.Min(minCost1, cost1);

                int cost2 = TotalCost2(crabPositions, targetPos);
                minCost2 = Math.Min(minCost2, cost2);

                Console.WriteLine($"Position {targetPos} costs {cost1} or {cost2}");
            }

            return new Tuple<int, int>(minCost1, minCost2);
        }

        private static int TotalCost2(int[] crabPositions, int targetPos)
        {
            return crabPositions.Select(c => Accumulate(Math.Abs(targetPos - c))).Sum();
        }

        private static int Accumulate(int steps)
        {
            int total = 0;

            while (steps > 0)
            {
                total += steps--;
            }
            return total;
        }

        private static int TotalCost1(int[] crabPositions, int targetPos)
        {
            return crabPositions.Select(c => Math.Abs(targetPos - c)).Sum();
        }

        public static int Part2(string line)
        {
            return 0;
        }
    }
}
