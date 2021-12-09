using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day8_SevenSegmentSearch
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
            foreach(string line in lines)
            {
               
                int value = Decode(line);
            }
            return 0;
        }

        private static int Decode(string line)
        {
            string[] parts = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            string[] readings = parts[0].Split(new char[] { ' ' });
            string[] digitCodes = new string[10];

            digitCodes[1] = readings.Where(r => r.Length == 2).First();
            digitCodes[4] = readings.Where(r => r.Length == 4).First();
            digitCodes[7] = readings.Where(r => r.Length == 3).First();
            digitCodes[8] = readings.Where(r => r.Length == 7).First();

            //   0
            // 1   2
            //   3
            // 4   5
            //   6

            char[] segmentCodes = new char[7];
            segmentCodes[0] = UniqueChars(new List<string>() { digitCodes[1], digitCodes[7]}).First();


            throw new NotImplementedException();
        }

        private static List<char> UniqueChars(List<string> list)
        {
            List<char> allCodes = new List<char>();

            foreach (string s in list)
            {
                allCodes.AddRange(s.ToList());
            }

            var result =
                from x in allCodes
                group allCodes by x into grp
                where grp.Count() == 1
                select grp.Key;

            return result.ToList() ;
        }
    }
}
