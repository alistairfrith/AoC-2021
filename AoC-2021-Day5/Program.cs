using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day5
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
            var vents = MapVents(lines, true);
            PrintMap(vents);

            return vents.Where(v => v.Value > 1).Count();
        }

        public static int Part2(string[] lines)
        {
            var vents = MapVents(lines, false);
            PrintMap(vents);

            return vents.Where(v => v.Value > 1).Count();
        }

        private static void PrintMap(Dictionary<string, int> vents)
        {
            for (int yCoord = 0; yCoord < 10; yCoord++)
            {
                for (int xCoord = 0; xCoord < 10; xCoord++)
                {
                    string location = $"{xCoord},{yCoord}";

                    if (vents.Keys.Contains(location))
                    {
                        Console.Write($" {vents[location]}");
                    }
                    else
                    {
                        Console.Write(" .");
                    }
                }
                Console.WriteLine();
            }
        }

        private static Dictionary<string, int> MapVents(string[] lines, bool onlyOrthogonal)
        {
            Dictionary<string, int> vents = new Dictionary<string, int>();

            foreach (string line in lines)
            {
                string[] separators = { " -> " };
                var ends = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                var start=ends[0].Split(',');
                var end = ends[1].Split(',');

                if (onlyOrthogonal)
                {
                    if (LineIsOrthoganol(start, end))
                    {
                        vents = MapLine(vents, start, end);
                    }
                }
                else
                {
                    vents = MapLine(vents, start, end);
                }
            }

            return vents;
        }

        private static Dictionary<string, int> MapLine(Dictionary<string, int> vents, string[] start, string[] end)
        {
            int[] startCoords = CoordFromStrings(start);
            int[] endCoords = CoordFromStrings(end);

            int xIncrement = calcIncrement(startCoords[0], endCoords[0]);
            int yIncrement = calcIncrement(startCoords[1], endCoords[1]);

            int xCoord = startCoords[0];
            int yCoord = startCoords[1];
            vents = AddVent(vents, xCoord, yCoord);

            do
            {
                xCoord += xIncrement;
                yCoord += yIncrement;
                vents = AddVent(vents, xCoord, yCoord);
            } while (xCoord != endCoords[0] || yCoord != endCoords[1]);

            return vents;
        }

        private static int calcIncrement(int start, int end)
        {
            if (start == end) return 0;
            if (start > end) return -1;
            if (start < end) return 1;

            throw new Exception("cannot calculate coordinate increment");
        }

        private static bool LineIsOrthoganol(string[] start, string[] end)
        {
            return (start[0] == end[0] || start[1] == end[1]);  
        }

        private static Dictionary<string, int> AddVent(Dictionary<string, int> vents, int xCoord, int yCoord)
        {
            string location = $"{xCoord},{yCoord}";
            if (vents.Keys.Contains(location))
            {
                vents[location] += 1;
            }
            else
            {
                vents.Add(location, 1);
            }
            return vents;
        }

        private static int[] CoordFromStrings(string[] coordString)
        {
            int[] result = { int.Parse(coordString[0]), int.Parse(coordString[1]) };
            return result;
        }

    }
}
