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

        Console.WriteLine($"Processing input from {inputFileName}");
        Console.WriteLine(Part1(lines));
        Console.WriteLine(Part2(lines));
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