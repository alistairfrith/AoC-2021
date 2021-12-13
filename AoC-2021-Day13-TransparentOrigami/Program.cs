using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day13_TransparentOrigami
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
            Console.WriteLine("================================");
            string[] lines = File.ReadAllLines(inputFileName);

            Console.WriteLine($"Processing input from {inputFileName}");
            string programName = Assembly.GetExecutingAssembly().FullName.Split(',')[0];
            Console.WriteLine($"\t{programName} Part 1 : {Part1(lines)}");
            Console.WriteLine($"\t{programName} Part 2 : {Part2(lines)}");
            Console.WriteLine();
        }

        public static long Part1(string[] lines)
        {
            var dots = new List<Tuple<int, int>>();

            foreach (string line in lines)
            {
                string[] coords = line.Split(',');
                if (coords.Count() == 2)
                {
                    dots.Add(new Tuple<int, int>(int.Parse(coords[0]), int.Parse(coords[1])));
                }
                else
                {
                    dots = Fold(dots, line);
                }
            }

            PrintDots(dots);
            return dots.Count();
        }

        public static void PrintDots(List<Tuple<int, int>> dots)
        {
            int y = 0;
            int x = 0;

            //Console.Clear();
            foreach (var dot in dots.OrderBy(d => d.Item2 * 10000 + d.Item1).Distinct())
            {
                while (y < dot.Item2)
                {
                    Console.WriteLine();
                    y++;
                    x = 0;
                }
                while (x < dot.Item1)
                {
                    Console.Write(" ");
                    x++;
                }
                Console.Write('#');
                x++;
            }

            Console.WriteLine();
        }

        public static List<Tuple<int, int>> Fold(List<Tuple<int, int>> dots, string foldLine)
        {
            List<Tuple<int, int>> foldedDots = new List<Tuple<int, int>>();

            if (!foldLine.StartsWith("fold along "))
            {
                return dots;
            }

            char foldAxis = foldLine[11];
            int foldLocation = int.Parse(foldLine.Substring(13));

            foreach (var dot in dots)
            {
                if (foldAxis == 'x' && dot.Item1 > foldLocation)
                {
                    int newX = foldLocation - (dot.Item1 - foldLocation);
                    foldedDots.Add(new Tuple<int, int>(newX, dot.Item2));
                }
                else if (foldAxis == 'y' && dot.Item2 > foldLocation)
                {
                    int newY = foldLocation - (dot.Item2 - foldLocation);
                    foldedDots.Add(new Tuple<int, int>(dot.Item1, newY));
                }
                else
                {
                    foldedDots.Add(dot);
                }
            }

            return foldedDots;
        }

        public static long Part2(string[] lines)
        {
            return 0;
        }
    }
}
