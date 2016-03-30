﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Utils
    {
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
    }
}