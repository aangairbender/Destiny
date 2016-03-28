using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Hero:Unit
    {
        int hp;
        int attack;
        public Hero(Point location)
        {
            this.location = location;
            this.hp = 100;
            this.attack = 5;
        }
    }
}
