using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day4
{
    public class BingoCard
    {
        private string[,] numbers = new string[5, 5];
        private bool[,] matches = new bool[5, 5];
        private int score = 0;

        public BingoCard(IEnumerable<string> lines)
        {
            string[] separators = { " ", "  " };
            string[] rows = lines.ToArray();
            for (int rowNum = 0; rowNum < 5; rowNum++)
            {
                var row = rows[rowNum].Split(separators, StringSplitOptions.RemoveEmptyEntries);

                for (int colNum = 0; colNum < 5; colNum++)
                {
                    numbers[rowNum, colNum] = row[colNum];
                }
            }

        }

        override public string ToString()
        {
            string result = "";

            for (int rowNum = 0; rowNum < 5; rowNum++)
            {
                for (int colNum = 0; colNum < 5; colNum++)
                {
                    char matchChar = ' ';
                    if (matches[rowNum, colNum])
                    {
                        matchChar = '*';
                    }
                    result += $"  {matchChar}{numbers[rowNum, colNum],2}{matchChar}  ";
                }
                result += '\n';
            }

            return result;
        }
       
        private void CalculateScore(string winningCall)
        {
            int sumUnmarked = 0;
            for (int rowNum = 0; rowNum < 5; rowNum++)
            {
                for (int colnum = 0; colnum < 5; colnum++)
                {
                    if (matches[rowNum, colnum] == false)
                    {
                        sumUnmarked += int.Parse(numbers[rowNum, colnum]);
                    }
                }
            }

            score = sumUnmarked * int.Parse(winningCall);
        }

        internal int Score()
        {
            return score;
        }

        internal void Mark(string call)
        {
            // don't mark if we've already won
            if (score > 0)
                return;

            for (int rowNum = 0; rowNum < 5; rowNum++)
            {
                for (int colnum = 0; colnum < 5; colnum++)
                {
                    if (numbers[rowNum, colnum] == call)
                    {
                        matches[rowNum, colnum] = true;
                        if (CompleteLine())
                        {
                            CalculateScore(call);
                        }
                        return;
                    }
                }
            }

            return;
        }

        internal bool CompleteLine()
        {
            for (int rowNum = 0; rowNum < 5; rowNum++)
            {
                if (matches[rowNum, 0] && matches[rowNum, 1] && matches[rowNum, 2] && matches[rowNum, 3] && matches[rowNum, 4])
                    return true;
            }

            for (int colnum = 0; colnum < 5; colnum++)
            {
                if (matches[0, colnum] && matches[1, colnum] && matches[2, colnum] && matches[3, colnum] && matches[4, colnum])
                    return true;
            }

            return false;
        }
    }
}
