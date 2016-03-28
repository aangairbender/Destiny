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
        protected int hp;
        protected int attack;
        public Monster(Point location)
        {
            this.location = location;
            
            
        }
    }
}
