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
            this.def.hp = 30;
            this.def.maxhp = def.hp;
            this.direction = 0;
            this.atk.dmg = 2;
            this.def.armor = 0;
            this.sprite = "spider";
        }
        public override void makeMove(World world)
        {
            int d = Utils.dist(world.hero.location, this.location);
            if (d > 5) return;

            if (d == 1 && Utils.getDirection(new Point(world.hero.location.X - this.location.X, world.hero.location.Y - this.location.Y)) == this.direction)
            {
                attack(world.hero);
            } else 
            {
                for (int i = -1; i <= 1; ++i)
                    for (int j = -1; j <= 1; ++j)
                        if (Math.Abs(i) + Math.Abs(j) == 1)
                        {

                            if (Math.Abs(world.hero.location.X - this.location.X - i) + Math.Abs(world.hero.location.Y - this.location.Y - j) == d - 1 && (world.map[this.location.X + i, this.location.Y + j].passable || world.map[this.location.X + i, this.location.Y + j].unitStanding == world.hero))
                            {
                                world.map[this.location.X, this.location.Y].unitStanding = null;
                                world.map[this.location.X, this.location.Y].passable = true;
                                Point pos = Utils.movePoint(this.location, this.direction);
                                if (Utils.dist(pos, new Point(this.location.X + i, this.location.Y + j)) == 0 && world.map[this.location.X + i, this.location.Y + j].passable)
                                    this.setLocation(new Point(this.location.X + i, this.location.Y + j));
                                else
                                {
                                    this.direction = Utils.getDirection(new Point(i, j));
                                    this.setLocation(this.location);
                                }
                                world.map[this.location.X, this.location.Y].unitStanding = this;
                                world.map[this.location.X, this.location.Y].passable = false;
                                return;
                            }
                        }
            }
            this.setLocation(this.location);
        }

    }
}
