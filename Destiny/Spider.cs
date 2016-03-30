﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Spider:Monster
    {
        public Spider(Point location):base(location)
        {
            this.attack = 1;
            this.hp = 1;
            this.direction = 0;
            this.atk.dmg = this.attack;
            this.def.armor = 1;
            this.sprite = "spider";
        }
        public override void makeMove(World world)
        {

        }

    }
}
