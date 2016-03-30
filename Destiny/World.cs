using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny
{
    class World
    {
        public Map map;
        public List<Unit> units;
        public Hero hero;
        public List<Item> items;
        public BitmapCollection bc;
        private Random rand;
        private IdManager idManager;
        
        public World(int width, int height)
        {
            idManager = new IdManager();
            rand = new Random(DateTime.Now.Millisecond);
            bc = new BitmapCollection();
            map = new Map(width, height);
            units = new List<Unit>();
            items = new List<Item>();

            bool placed = false;
            for(int i=0;i<map.getWidth() && !placed;++i)
                for(int j=0;j<map.getHeight() && !placed;++j) 
                    if(map[i,j].isPassable() && map[i,j].getTile() == "wood")
                    {
                        hero = new Hero(new Point(i, j));
                        placed = true;
                    }

            units.Add(hero);
            map[hero.getLocation().X, hero.getLocation().Y].unitStanding = hero;
            map[hero.getLocation().X, hero.getLocation().Y].passable = false;

            ///////////////




        }
        private void placeUnit(Unit unit)
        {
            int ax, ay;
            do
            {
                ax = rand.Next(0, map.getWidth());
                ay = rand.Next(0, map.getHeight());
            } while (!map[ax, ay].isPassable());
            unit.setLocation(new Point(ax, ay));
            units.Add(unit);
            map[ax, ay].unitStanding = unit;
            map[ax, ay].passable = false;
        }


        public void Draw(Graphics g)
        {
            g.Clear(Color.Black);
            for (int i = 0; i < map.getWidth(); ++i)
                for (int j = 0; j < map.getHeight(); ++j)
                {
                    g.DrawImage(bc["tiles"][map[i, j].getTile()], i * 32, j * 32);
                    if (map[i, j].hasDecoration()) g.DrawImage(bc["decorations"][map[i, j].getDecoration()], i * 32, j * 32);
                    if (map[i, j].itemsLying.Count > 1) g.DrawImage(bc["items"]["sack"], i * 32, j * 32);
                    else if (map[i, j].itemsLying.Count == 1) g.DrawImage(bc["items"][map[i,j].itemsLying[0].sprite], i * 32, j * 32);
                    if (map[i, j].unitStanding != null)
                    {
                        g.DrawImage(bc["actors"][map[i, j].unitStanding.getSprite()+map[i,j].unitStanding.direction.ToString()], i * 32, j * 32);
                        g.DrawRectangle(new Pen(Color.Black), i * 32+2, j * 32, 28, 2);
                        //g.FillRectangle(new SolidBrush(Color.Red), i * 32+2, j * 32, 28 * map[i,j].unitStanding / (map[i,j].unitStanding.astr*10), 2);
                    }
                }

            //g.DrawString(hero.hp.ToString() + "/" + (hero.astr * 10).ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), new PointF(0, 0));

        }

        public void makeMoves()
        {
            foreach(var a in units)
            {
                a.makeMove(this);
            }
        }

        public void attack(Unit unit, AttackInfo attackInfo)
        {
        }

    }
}
