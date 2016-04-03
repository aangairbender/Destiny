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
            int d = Utils.dist(world.hero, this);
            if (d > 5) return;
            for (int i = -1; i <= 1; ++i)
                for (int j = -1; j <= 1; ++j)
                    if(Math.Abs(i)+Math.Abs(j)==1)
                    {
                      
                        if(Math.Abs(world.hero.location.X-i)+Math.Abs(world.hero.location.Y-j)==d-1 && world.map[this.location.X+i, this.location.Y+j].passable)
                        {
                            this.setLocation(new Point(this.location.X + i, this.location.Y + j));
                            return;
                        }
                    }
        }

    }
}
