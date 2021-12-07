using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AoC_2021_Day6
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
            Console.WriteLine($"\t{programName} Part 1 : {Part1(lines[0], 80)}");
            Console.WriteLine($"\t{programName} Part 2 : {Part2(lines[0], 256)}");
            Console.WriteLine();
        }

        public static int Part1(string line, int numGenerations)
        {
            List<int> lamprays = line.Split(',').Select(int.Parse).ToList();

            int numLamprays = 0;
            foreach (int lampray in lamprays)
            {
                numLamprays += Breed(lampray, numGenerations);
            }

            return numLamprays;
        }

        private static int Breed(int parentLampray, int numGenerations)
        {

            List<int> lamprays = new List<int>();
            lamprays.Add(parentLampray);
            List<int> nextGeneration = new List<int>();

            for (int generation = 0; generation < numGenerations; generation++)
            {
                Console.WriteLine($"{parentLampray} Gen {generation} Decentents: {lamprays.Count()}");
                foreach (int lampray in lamprays)
                {
                    if (lampray == 0)
                    {
                        nextGeneration.Add(6);
                        nextGeneration.Add(8);
                    }
                    else
                    {
                        nextGeneration.Add(lampray - 1);
                    }
                }
                lamprays.Clear();
                lamprays.AddRange(nextGeneration);
                nextGeneration.Clear();
            }

            Console.WriteLine($"{parentLampray} : {lamprays.Count()}");
            return lamprays.Count();
        }


        public static long Part2(string line, int days)
        {
            // since this is exponential, the solution for part 1 will not cope with part 2, so modelling every lampray is not viable and the machine will run
            // out of memory (as well as going painfully slow)
            // So instead, we need to keep a count of how many lamprays there are at each stage of gestation and adjust those counts as the days pass

            List<int> lamprays = line.Split(',').Select(int.Parse).ToList();

            List<long> daysTillBirth = new List<long>() { 0,0,0,0,0,0,0,0,0};
            foreach (int lampray in lamprays)
            {
                daysTillBirth[lampray]++;
            }

            for (int day = 0; day < days; day++)
            {
                daysTillBirth.Add(daysTillBirth[0]); // create children for any lamprays on day 0
                daysTillBirth[7] += daysTillBirth[0]; // reset the parents to start incubating again
                daysTillBirth = daysTillBirth.Skip(1).ToList(); // shift everyone down by 1 day
            }

            return daysTillBirth.Sum();
        }
    }
}
