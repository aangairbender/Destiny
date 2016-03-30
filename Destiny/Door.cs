using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Door : Obj
    {
        private String state;
        public Door(Point location, String sprite) : base(location, sprite)
        {
            state = "closed";
        }
        public bool activate()
        {
            if (state == "closed") sprite.Replace("closed", "opened");
            else sprite.Replace("opened", "closed");
            return true;
        }
    }
}
