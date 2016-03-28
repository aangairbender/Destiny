using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Monster:Unit
    {
        int hp;
        int attack;
        public Monster(Point location)
        {
            this.location = location;
            
            
        }
    }
}
