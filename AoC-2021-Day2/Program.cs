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
        var forwards = lines.Where(line => line.StartsWith("forward"));
        var downs = lines.Where(line => line.StartsWith("down"));
        var ups = lines.Where(line => line.StartsWith("up"));

        int forward = 0;
        foreach (var forwardCmd in forwards)
        {
            forward += int.Parse(forwardCmd.Split(' ').Last());
        }

        int down = 0;
        foreach (var downCmd in downs)
        {
            down += int.Parse(downCmd.Split(' ').Last());
        }

        int up = 0;
        foreach (var upCmd in ups)
        {
            up += int.Parse(upCmd.Split(' ').Last());
        }

        return forward * Math.Abs(up - down);
    }

    public static int Part2(string[] lines)
    {
        int aim = 0;
        int depth = 0;
        int position = 0;

        foreach (string line in lines)
        {
            var cmds = line.Split(' ');
            int value = int.Parse(cmds[1]);

            switch (cmds[0])
            {
                case "forward":
                    position += value;
                    depth += aim * value;
                    break;

                case "up":
                    aim -= value;
                    break;

                case "down":
                    aim += value;
                    break;

                default:
                    throw new Exception($"invalid command ");
            }
        }

        return position * depth;
    }
}