using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Spider:Monster
    {
        public Spider()
        {
            this.hp = 50;
            this.maxhp = hp;
            this.direction = 0;
            this.atk.dmg = 1;
            this.def.armor = 1;
            this.sprite = "spider";
        }
        public override void makeMove(World world)
        {

        }

    }
}
