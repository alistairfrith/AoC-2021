using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day17_TrickShot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            DoIt("inputExample.txt");
            //DoIt("input.txt");


            Console.WriteLine("Hit any key to continue");
            Console.ReadKey();
        }

        public static void DoIt(string inputFileName)
        {
            Console.WriteLine("================================");
            Console.WriteLine($"Processing {inputFileName}");
            string[] lines = File.ReadAllLines(inputFileName);

            Console.WriteLine($"Part 1 : {Part1(lines, true)}");
            Console.WriteLine($"Part 2 : {Part2(lines)}");
        }

        public static long Part1(string[] lines, bool verbose = false)
        {
            string[] parts = lines[0].Split(' ');

            string xLimits = parts[2].Substring(2);
            long xLeft = long.Parse(xLimits.Split('.')[0]);
            long xRight = long.Parse(xLimits.Split('.')[2]);


            string yLimits = parts[3].Substring(2);
            long yBottom = long.Parse(yLimits.Split('.')[0]);
            long yTop = long.Parse(yLimits.Split('.')[2]);

            Target target = new Target(yTop, xLeft, xRight, yBottom);

            List<Shot> shots = new List<Shot>();

            for (long yVelocity = yBottom; yVelocity < 1000; yVelocity++)
            {
                Shot shot = new Shot(0, yVelocity, target);
                if (shot.YVelocityHitsTarget())
                {
                    shots.Add(shot);
                }
            }



            return shots.Last().MaxHeight();
        }

        public static long Part2(string[] lines, bool verbose = false)
        {
            string[] parts = lines[0].Split(' ');

            string xLimits = parts[2].Substring(2);
            long xLeft = long.Parse(xLimits.Split('.')[0]);
            long xRight = long.Parse(xLimits.Split('.')[2]);


            string yLimits = parts[3].Substring(2);
            long yBottom = long.Parse(yLimits.Split('.')[0]);
            long yTop = long.Parse(yLimits.Split('.')[2]);

            Target target = new Target(yTop, xLeft, xRight, yBottom);

            List<Shot> shots = new List<Shot>();

            for (long yVelocity = yBottom; yVelocity < 1000; yVelocity++)
            {
                Shot shot = new Shot(0, yVelocity, target);
                if (shot.YVelocityHitsTarget())
                {
                    shots.Add(shot);
                }
            }


            long validTrajectoryCount = 0;

            foreach (Shot shot in shots)
            {
                for (long xVelocity = 1; xVelocity <= xRight; xVelocity++)
                {
                    if (shot.XVelocityAlsoHitsTarget(xVelocity))
                    {
                        Console.WriteLine($"{xVelocity}:{shot.VelocityY}");
                        validTrajectoryCount++;
                    }
                }
            }


            return validTrajectoryCount;
        }

    }
}
