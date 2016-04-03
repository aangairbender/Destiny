using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Utils
    {
        static Random rnd=new Random();
        public static int random(int top)
        {
            return rnd.Next(1, top);
        }
        public static Point movePoint(Point old, int dir)
        {
            Point np = new Point(old.X,old.Y);
            
            if (dir == 0)
                --np.Y;
            if (dir == 1)
                ++np.X;
            if (dir == 2)
                ++np.Y;
            if (dir == 3)
                --np.X;
            return np;
        }
        public static String getSlotName(int i)
        {
            if (i == 0) return "Оружие";
            if (i == 1) return "Броня";
            return "Неизвестно";
        }

        public static int dist(Point u1, Point u2)
        {
            return Math.Abs(u1.X - u2.X) + Math.Abs(u1.Y - u2.Y);
        }
        public static int getDirection(Point offset)
        {
            if (offset.X == 1 && offset.Y == 0) return 1;
            else if (offset.X == -1 && offset.Y == 0) return 3;
            else if (offset.X == 0 && offset.Y == 1) return 2;
            else if (offset.X == 0 && offset.Y == -1) return 0;
            else return 4;
        }
    }
}
