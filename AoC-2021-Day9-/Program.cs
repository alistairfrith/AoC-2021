using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day9_
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
            int[,] heights = new int[lines.Length, lines[0].Length];
            int totalRisk = 0;

            int rowNum = 0;
            foreach (string line in lines)
            {
                int[] row = Array.ConvertAll(line.ToCharArray(), c => (int)Char.GetNumericValue(c)).ToArray();
                for (int cellNum = 0; cellNum < row.Length; cellNum++)
                {
                    heights[rowNum,cellNum] = row[cellNum];
                }
                rowNum++;
            }

            for (rowNum=1; rowNum<heights.GetLength(0)-1; rowNum++)
            {
                for (int colNum = 1;colNum < heights.GetLength(1)-1; colNum++)
                {
                    if (CellIsLowPoint(heights[rowNum, colNum], heights[rowNum-1, colNum], heights[rowNum+1, colNum], heights[rowNum, colNum+1], heights[rowNum, colNum-1]))
                    {
                        totalRisk += heights[rowNum, colNum] + 1;
                    }
                }
            }
            return totalRisk;
        }

        private static bool CellIsLowPoint(int cell, int above, int below, int right, int left)
        {
            return cell < Math.Min(above, below) && cell < Math.Min(left, right);
        }

        public static int Part2(string[] lines)
        {
            return 0;
        }
    }
}
