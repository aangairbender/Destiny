using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny
{
    class HeroController
    {
        private World world;//asd
        public HeroController(World world)
        {
            this.world = world;
        }

        public bool makeMove(Keys keyCode)
        {
            if (!world.moveAvailable) return false;
            bool moveMade = true;
            int px = world.hero.location.X;
            int py = world.hero.location.Y;
            int dir = world.hero.direction;
            world.map[px, py].unitStanding = null; 
            world.map[px, py].passable = true;
            int npx = px, npy = py, ndir = dir;
            Point np = new Point(px, py);

            switch (keyCode)
            {
                case Keys.Up:
                    {
                        if (dir == 0)
                            npy--;
                        else ndir = 0;
                        break;
                    }
                case Keys.Right:
                    {
                        if (dir == 1)
                            npx++;
                        else ndir = 1;
                        break;
                    }
                case Keys.Down:
                    {
                        if (dir == 2)
                            npy++;
                        else ndir = 2;
                        break;
                    }
                case Keys.Left:
                    {
                        if (dir == 3)
                            npx--;
                        else ndir = 3;
                        break;
                    }
                case Keys.R:
                {
                    Spider fef = new Spider();
                    world.placeUnit(fef);
                    break;
                }
                case Keys.G:
                {
                    world.hero.inventory.Add(new SwordElucidator());
                    break;
                }
                case Keys.B:
                {
                    world.hero.inventory.Add(new HealthPotion());
                    break;
                }
                case Keys.Y:
                {
                    if(world.hero.inventory.Count==0)break;
                    if (world.hero.inventory.Last().canEquip(world.hero))
                    {
                        world.hero.inventory.Last().equip(world.hero);
                        world.hero.inventory.Remove(world.hero.inventory.Last());
                    }
                    break;
                }
                case Keys.U:
                {
                    if (world.hero.slot[0] == null) break;
                    world.hero.slot[0].unequip(world.hero);
                    break;
                }
                case Keys.A:
                    {
                        np = Utils.movePoint(world.hero.location, world.hero.direction);
                        Monster M=(Monster)world.map[np.X,np.Y].unitStanding;
                       // if(M!=null)Hero.att
                        if(M==null)break;
                        {
                            M.hp -= world.calcDmg(world.hero.heroBaseAttack, M.def);
                            if (M.hp <= 0)
                            {
                                world.map[np.X, np.Y].unitStanding = null;
                                world.map[np.X, np.Y].passable = true;
                                world.units.Remove(M);
                            }
                        }
                        break;
                    }
                case Keys.E:
                    {
                        np = Utils.movePoint(world.hero.location, world.hero.direction);
                        if(world.map[np.X, np.Y].objStanding != null)
                        {
                            world.map[np.X, np.Y].objStanding.activate();
                        }
                        break;
                    }
                case Keys.I:
                    {
                        if (world.inventoryGUI.visible) world.inventoryGUI.hide();
                        else world.inventoryGUI.show();
                        moveMade = false;
                        break;
                    }
                case Keys.T:
                    {
                        world.hero.inventory.AddRange(world.map[world.hero.location.X, world.hero.location.Y].itemsLying);
                        world.map[world.hero.location.X, world.hero.location.Y].itemsLying.Clear();
                        break;
                    }
                default:
                    {
                        moveMade = false;
                        break;
                    }
            }
            world.hero.direction = ndir;
            if (dir == ndir && npx >= 0 && npy >= 0 && npx < world.map.width && npy < world.map.height && world.map[npx, npy].passable)
            {
                world.hero.setLocation(new Point(npx, npy));
                world.map[npx, npy].unitStanding = world.hero;
                world.map[npx, npy].passable = false;
            }
            else
            {
                world.hero.setLocation(new Point(px, py));
                 world.map[px, py].unitStanding = world.hero;
                 world.map[px, py].passable = false;
                if (dir == ndir) moveMade = false;
            }

           if (moveMade) world.heroMoveTick = world.tick;

            return moveMade;
        }

    }
}
