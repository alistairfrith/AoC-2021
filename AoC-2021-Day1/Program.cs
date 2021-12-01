using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    public static void Main(string[] args)
    {
        DoIt("inputExample.txt");
        DoIt("input.txt");
    }

    public static void DoIt(string inputFileName)
    {
        string[] lines = File.ReadAllLines(inputFileName);
        int[] depths = Array.ConvertAll(lines, l => int.Parse(l));

        Console.WriteLine($"Processing input from {inputFileName}");
        Console.WriteLine(CountIncreases(depths));
        Console.WriteLine(CountIncreases(SmoothDepths(depths)));
    }

    public static int CountIncreases(int[] depths)
    {
        int count = 0;
        for (int i = 1; i < depths.Length; i++)
        {
            if (depths[i - 1] < depths[i])
            {
                count++;
            }
        }

        return count;
    }

    public static int[] SmoothDepths(int[] depths)
    {
        List<int> averages = new List<int>();
        for (int i = 2; i < depths.Length; i++)
        {
            averages.Add(depths[i] + depths[i - 1] + depths[i - 2]);
        }

        return averages.ToArray();
    }
}