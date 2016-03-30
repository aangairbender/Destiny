using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Obj
    {
        public Point location;
        public String sprite;

        public Obj(Point location, String sprite)
        {
            this.location = location;
            this.sprite = sprite;
        }
        public bool activate()
        {
            return false;
        }
    }
}
