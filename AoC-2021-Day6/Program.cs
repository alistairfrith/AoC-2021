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
            Console.WriteLine($"\t{programName} Part 1 : {Part1(lines)}");
            Console.WriteLine($"\t{programName} Part 2 : {Part2(lines)}");
            Console.WriteLine();
        }

        public static int Part1(string[] lines)
        {
            return 0;
        }

        public static int Part2(string[] lines)
        {
            return 0;
        }
    }
}
