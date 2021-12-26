using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2021_Day17_TrickShot
{
    internal class Shot
    {
        private Target ShotTarget
        { get; set; }
        private long velocityX
        { get; set; }
        public long VelocityY
        { get; set; }
        private bool Hit
        { get; set;  }

        private List<long> xCoords = new List<long>();
        private List<long> yCoords = new List<long>();


        public Shot (long velocityX, long velocityY, Target target)
        {
            Hit = false;
            ShotTarget = target;
            this.velocityX = velocityX;
            this.VelocityY = velocityY;

            yCoords.Add(0);
            xCoords.Add(0);
        }

        public bool YVelocityHitsTarget ()
        {
            long yCoord = yCoords.Last();
            long velocityY = VelocityY;
            while (yCoord >= ShotTarget.Bottom)
            {
                yCoord = yCoords.Last() + velocityY;
                velocityY--;

                if (yCoord <= ShotTarget.Top && yCoord >= ShotTarget.Bottom)
                {
                    Hit = true;
                }

                if (yCoord >= ShotTarget.Bottom)
                {
                    yCoords.Add(yCoord);
                }
            }
            

            return Hit;
        }

        public bool XVelocityAlsoHitsTarget(long xVelocity)
        {
            long xCoord = 0;
            while (xVelocity > 0 && xCoord <= ShotTarget.Right )
            {
                xCoord += xVelocity;
                xVelocity--;

                xCoords.Add(xCoord);
            }

            for (int i = 0; i<Math.Min(xCoords.Count, yCoords.Count); i++)
            {
                if (xCoords[i] >= ShotTarget.Left && xCoords[i] <= ShotTarget.Right &&
                    yCoords[i] >= ShotTarget.Bottom && yCoords[i] <= ShotTarget.Top)
                {
                    return true;
                }
            }
            return false;
        }

        public long MaxHeight()
        {
            return yCoords.Max();
        }
    }
}
