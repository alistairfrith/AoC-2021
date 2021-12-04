using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

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
        int gamma = 0;
        int epsilon = 0;
        for (int i=0; i<lines[1].Length; i++)
        {
            int ones = lines.Where(s => s.Substring(i,1) == "1").Count();
            int zeros = lines.Where(s => s.Substring(i, 1) == "0").Count();

            gamma = (gamma * 2) + ((ones > zeros) ? 1 : 0);
            epsilon = (epsilon * 2) + ((ones < zeros) ? 1 : 0);
        }
        return gamma * epsilon;
    }

    public static int Part2(string[] lines)
    {
        int o2Rating = GetRating(lines, "1", "0");
        int co2Rating = GetRating(lines, "0", "1");

        return o2Rating * co2Rating;
    }

    private static int GetRating(string[] lines, string onesChar, string zerosChar)
    {
        string[] rating = lines;
        for (int i = 0; i < lines[1].Length; i++)
        {
            int ones = rating.Where(s => s.Substring(i, 1) == "1").Count();
            int zeros = rating.Where(s => s.Substring(i, 1) == "0").Count();

            string character = (ones >= zeros) ? onesChar : zerosChar;
            rating = rating.Where(s => s.Substring(i, 1) == character).ToArray();

            if (rating.Length == 1)
            {
                break;
            }
        }

        return Convert.ToInt32(rating[0], 2);
    }
}