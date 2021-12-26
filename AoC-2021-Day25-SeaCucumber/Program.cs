using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day25_SeaCucumber
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            //DoIt("inputExample.txt");
            DoIt("input.txt");

            Console.WriteLine("Hit any key to continue");
            Console.ReadKey();
        }

        public static void DoIt(string inputFileName)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Processing {inputFileName}");
            string[] lines = File.ReadAllLines(inputFileName);

            Console.WriteLine($"Part 1 : {Part1(lines, true)}");
            // Console.WriteLine($"Part 2 : {Part2(lines)}");
        }

        public static long Part1(string[] lines, bool verbose = false)
        {
            List<Cucumber> cucumbers = new List<Cucumber>();

            long trenchWidth = lines[0].Length;
            long trenchLength = lines.Count();

            SeedCucumbers(lines, cucumbers);
            //PrintCucumbers(cucumbers, trenchWidth, trenchLength);

            long moves = 0;
            while (MoveCucumbers(cucumbers, trenchWidth, trenchLength)>0)
            {
                moves++;
                Console.WriteLine($"Move {moves}");
                //PrintCucumbers(cucumbers, trenchWidth, trenchLength);
            }

            return moves + 1;
        }

        private static long MoveCucumbers(List<Cucumber> cucumbers, long trenchWidth, long trenchLength)
        {
            long movedCucumbers = 0;
            List<Cucumber> currentState = Clone(cucumbers);
            foreach (var cucumber in cucumbers.Where(c => c.Direction=='>'))
            {
                movedCucumbers += MoveRight(cucumbers, currentState, trenchWidth, cucumber);
            }

            currentState = Clone(cucumbers);
            foreach (var cucumber in cucumbers.Where(c => c.Direction=='v'))
            {
                movedCucumbers += MoveDown(cucumbers, currentState, trenchLength, cucumber);
            }

            cucumbers.Sort();

            return movedCucumbers;
        }

        private static long MoveRight(List<Cucumber> cucumbers, List<Cucumber> currentState, long trenchWidth, Cucumber cucumber)
        {
            long targetX = (cucumber.X + 1) % trenchWidth;
            long targety = (cucumber.Y);
            return MoveCucumber(cucumbers, currentState, cucumber, targetX, targety);
        }

        private static long MoveDown(List<Cucumber> cucumbers, List<Cucumber> currentState, long trenchLength, Cucumber cucumber)
        {
            long targetX = (cucumber.X);
            long targety = (cucumber.Y + 1) % trenchLength;
            return MoveCucumber(cucumbers, currentState, cucumber, targetX, targety);
        }

        private static long MoveCucumber(List<Cucumber> cucumbers, List<Cucumber> currentState, Cucumber cucumber, long targetX, long targety)
        {
            if (currentState.Where(c => c.X == targetX && c.Y == targety).Count() == 0)
            {
                cucumber.X = targetX;
                cucumber.Y = targety;
                return 1;
            }
            return 0;
        }

        private static void SeedCucumbers(string[] lines, List<Cucumber> cucumbers)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (">v".Contains(lines[y][x]))
                    {
                        cucumbers.Add(new Cucumber(lines[y][x], x, y));
                    }
                }
            }
        }

        private static List<Cucumber> Clone(List<Cucumber> cucumbers)
        {
            List<Cucumber> clone = new List<Cucumber>();

            foreach (var c in cucumbers)
            {
                clone.Add(new Cucumber(c.Direction, c.X, c.Y));
            }
            return clone;
        }

        private static void PrintCucumbers(List<Cucumber> cucumbers, long trenchWidth, long trenchLength)
        {
            long line = 0;
            long column = 0;

            foreach (Cucumber c in cucumbers)
            {
                while (line < c.Y)
                {
                    while (column < trenchWidth)
                    {
                        Console.Write('.');
                        column++;
                    }
                    Console.WriteLine();
                    line++;
                    column = 0;
                }

                while (column < c.X)
                {
                    Console.Write('.');
                    column++;
                }

                Console.Write(c.Direction);
                column++;
            }

            Console.WriteLine();
            Console.WriteLine();
        }


        public static long Part2(string[] lines, bool verbose = false)
        {
            return 0;
        }
    }


    internal class Cucumber : IComparable<Cucumber>
    {
        public char Direction { get; set; }

        public long X { get; set; }

        public long Y { get; set; }

        public Cucumber (char direction, long x, long y)
        {
            Direction = direction;
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{Direction} : X={X} Y={Y}";
        }

        public int CompareTo(Cucumber c)
        {
            ValidateInputs(c);

            if (this.Y > c.Y || (this.Y==c.Y && this.X > c.X))
            {
                return 1;
            }

            if (this.Y == c.Y && this.X == c.X)
            {
                return 0;
            }

            return -1;
        }

        private static void ValidateInputs(Cucumber a)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
        }
    }
}
