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
        public Hero player;
        public List<Item> items;
        public BitmapCollection bc;
        private Random rand;
        private IdManager idManager;
        
        public World(int width, int height)
        {
            rand = new Random(DateTime.Now.Millisecond);
            bc = new BitmapCollection();
            map = new Map(width, height);
            units = new List<Unit>();
            items = new List<Item>();
            player = new Hero(idManager.next(), "player", 100, 10);

            bool placed = false;
            for(int i=0;i<map.getWidth() && !placed;++i)
                for(int j=0;j<map.getHeight() && !placed;++j) 
                    if(map[i,j].isPassable() && map[i,j].getTile() == "wood")
                    {
                        player.setLocation(new Point(i, j));
                        placed = true;
                    }
            units.Add(player);
            map[player.getLocation().X, player.getLocation().Y].actorStanding = player;
            map[player.getLocation().X, player.getLocation().Y].passable = false;

            ///////////////



            placeItem("Wooden axe", "axe_wooden");
            placeItem("Wooden pick", "pick_wooden");
            placeItem("Wooden axe", "axe_stone");
            placeItem("Wooden pick", "pick_stone");
            placeItem("Wooden axe", "axe_iron");
            placeItem("Wooden pick", "pick_iron");


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
            map[ax, ay].actorStanding = unit;
            map[ax, ay].passable = false;
        }


        public void Draw(Graphics g)
        {
            g.Clear(Color.Black);
            for (int i = 0; i < map.getWidth(); ++i)
                for (int j = 0; j < map.getHeight(); ++j)
                {
                    g.DrawImage(bc["tiles"][map[i, j].getTile()], i * 32, j * 32);
                    if (map[i, j].hasObstacle()) g.DrawImage(bc["obstacles"][map[i, j].getObstacle()], i * 32, j * 32);
                    if (map[i, j].hasDecoration()) g.DrawImage(bc["decorations"][map[i, j].getDecoration()], i * 32, j * 32);
                    if (map[i, j].itemsLying.Count > 1) g.DrawImage(bc["items"]["sack"], i * 32, j * 32);
                    else if (map[i, j].itemsLying.Count == 1) g.DrawImage(bc["items"][map[i,j].itemsLying[0].sprite], i * 32, j * 32);
                    if (map[i, j].actorStanding != null)
                    {
                        g.DrawImage(bc["actors"][map[i, j].actorStanding.sprite], i * 32, j * 32);
                        g.DrawRectangle(new Pen(Color.Black), i * 32+2, j * 32, 28, 2);
                        g.FillRectangle(new SolidBrush(Color.Red), i * 32+2, j * 32, 28 * map[i,j].actorStanding.hp / (map[i,j].actorStanding.astr*10), 2);
                    }
                }

            for (int i = 0; i < map.getWidth(); ++i)
                for (int j = 0; j < map.getHeight(); ++j)
                    if (player.u[i, j] == 0) g.DrawImage(bc["tiles"]["fog"], i * 32, j * 32);


            g.DrawString(player.hp.ToString() + "/" + (player.astr * 10).ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), new PointF(0, 0));

        }

        public void makeMoves()
        {
            foreach(var a in actors)
            {
                if (a == player) continue;
                if (a.makeMove(this)) break;
            }
        }

        public void attack(Actor a, Actor b)
        {
            b.hp -= a.aagi;
            if(b.hp <= 0)
            {
                map[b.x, b.y].actorStanding = null;
                map[b.x, b.y].passable = true;
                map[b.x, b.y].itemsLying.AddRange(b.items);
                actors.Remove(b);
            }
        }

    }
}
