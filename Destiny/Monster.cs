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
        public int hp;
        public int maxhp;
        
        public Monster()
        {
            atk = new AttackInfo();
            def = new DefendInfo();
        }
    }
}
