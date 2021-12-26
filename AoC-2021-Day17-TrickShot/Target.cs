using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day17_TrickShot
{
    internal class Target
    {

        public long Top
        { get; set; }
        public long Bottom
        { get; set; }
        public long Left
        { get; set; }
        public long Right
        { get; set; }

        public Target (long top, long left, long right, long bottom)
        {
            Top = top; 
            Bottom = bottom; 
            Left = left; 
            Right = right; 
        }
    }
}
