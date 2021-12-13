using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day10_SyntaxScoring
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
            Console.WriteLine($"Part 1 : {Part1(lines)}");
            Console.WriteLine($"Part 2 : {Part2(lines)}");
        }

        public static long Part1(string[] lines)
        {

            long score = 0;
            foreach (string line in lines)
            {
                score += ErrorScore(line);
            }

            return score;
        }

        private static long ErrorScore(string line)
        {
            Stack stack = new Stack();

            foreach (char c in line)
            {
                if ("({<[".Contains(c))
                {
                    stack.Push(c);
                }
                else
                {
                    string pair = $"{stack.Peek()}{c}";
                    if ("(){}[]<>".Contains(pair))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return (ErrorValue(c));
                    }
                }
            }

            return 0;
        }

        private static long ErrorValue(char c)
        {
            long result = 0;

            switch (c)
            {
                case ')':
                    result = 3;
                    break;
                case ']':
                    result = 57;
                    break;
                case '}':
                    result = 1197;
                    break;
                case '>':
                    result = 25137;
                    break;
                default:
                    throw new Exception($"Invalid error character: {c}");
            }
            return result;
        }


        public static long Part2(string[] lines)
        {
            List<long> completionScores = new List<long>();
            foreach (string line in lines)
            {
                if (ErrorScore(line) == 0)
                {
                    completionScores.Add(CompletionScore(line));
                }
            }

            completionScores.Sort();
            PrintCompletionScores(completionScores);
            return completionScores[((completionScores.Count + 1) / 2) - 1];
        }

        private static long CompletionScore(string line)
        {
            Stack<char> stack = new Stack<char>();

            foreach (char c in line)
            {
                if ("({<[".Contains(c))
                {
                    stack.Push(c);
                }
                else
                {
                    string pair = $"{stack.Peek()}{c}";
                    if ("(){}[]<>".Contains(pair))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        throw new Exception($"Invalid closing character: {c}");
                    }
                }
            }

            long score = 0;
            char openingChar;
            while (stack.Count() > 0)
            {
                openingChar = stack.Pop();

                score = score * 5;
                if (openingChar == '(') score += 1;
                if (openingChar == '[') score += 2;
                if (openingChar == '{') score += 3;
                if (openingChar == '<') score += 4;
            }

            //Console.WriteLine($"Score={score}  Line={line}");

            return score;
        }

        private static void PrintCompletionScores(List<long> completionScores)
        {
            int item = 1;
            foreach (long score in completionScores)
            {
                Console.WriteLine($"Row:{item++}  -  Score: {score}");
            }
        }
    }
}
