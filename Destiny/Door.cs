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
        public Door(Point location, String sprite) : base(location, sprite)
        {
            passable = false;
        }
        public override bool activate()
        {
            if (sprite.Contains("closed"))
            {
                sprite = sprite.Replace("closed", "opened");
                passable = true;
            }
            else
            {
                sprite = sprite.Replace("opened", "closed");
                passable = false;
            }
            return true;
        }
    }
}
