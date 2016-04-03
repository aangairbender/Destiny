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
        public const int moveTime = 4;
        public const int cellSize = 32;
        public Map map;
        public List<Unit> units;
        public List<Obj> objects;
        public Hero hero;
        public List<Item> items;
        public BitmapCollection bc;
        private Random rand;
        private IdManager idManager;
        public HeroController heroController;
        public InventoryGUI inventoryGUI;
        public InputController inputController;
        public int tick;
        public int heroMoveTick;
        public bool moveAvailable;
        
        public World(int width, int height)
        {
            moveAvailable = true;
            tick = 0;
            inputController = new InputController(this);
            idManager = new IdManager();
            rand = new Random(DateTime.Now.Millisecond);
            bc = new BitmapCollection();
            map = new Map(width, height);
            units = new List<Unit>();
            items = new List<Item>();
            objects = new List<Obj>();
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j) 
                    if(map[i, j].decoration.Contains("door"))
                    {
                        Door d = new Door(new Point(i, j), map[i, j].decoration);
                        objects.Add(d);
                        map[i, j].decoration = "";
                        map[i, j].objStanding = d;
                        map[i, j].passable = d.passable;
                        
                    }

            heroController = new HeroController(this);

            bool placed = false;
            for(int i=0;i<map.getWidth() && !placed;++i)
                for(int j=0;j<map.getHeight() && !placed;++j) 
                    if(map[i,j].isPassable() && map[i,j].getTile() == "wood")
                    {
                        hero = new Hero(new Point(i, j), "TestGuy");
                        placed = true;
                    }


            inventoryGUI = new InventoryGUI(this);
            units.Add(hero);
            map[hero.getLocation().X, hero.getLocation().Y].unitStanding = hero;
            map[hero.getLocation().X, hero.getLocation().Y].passable = false;

            ///////////////



            updateCells();
        }
        public void placeUnit(Unit unit)
        {
            int ax, ay;
            do
            {
                ax = rand.Next(0, map.getWidth());
                ay = rand.Next(0, map.getHeight());
            } while (!(map[ax, ay].isPassable() && map[ax,ay].unitStanding == null));
            unit.setLocation(new Point(ax, ay));
            unit.oldLocation = unit.location;
            units.Add(unit);
            map[ax, ay].unitStanding = unit;
            map[ax, ay].passable = false;
        }


        public void draw(Graphics g, int width, int height, int tick)
        {
            this.tick = tick;

            int heroMoveDelta = tick - heroMoveTick;
            float curToOld = Math.Min(1.0f, 1.0f * heroMoveDelta / moveTime);
            if (heroMoveDelta >= moveTime) moveAvailable = true;
            else moveAvailable = false;

            g.Clear(Color.Black);
            for (int i = 0; i < map.getWidth(); ++i)
                for (int j = 0; j < map.getHeight(); ++j)
                {
                    g.DrawImage(bc["tiles"][map[i, j].getTile()], i * cellSize, j * cellSize);
                    if (map[i, j].hasDecoration()) g.DrawImage(bc["decorations"][map[i, j].getDecoration()], i * cellSize, j * cellSize);
                    
                    if (map[i, j].itemsLying.Count > 1) g.DrawImage(bc["items"]["sack"], i * cellSize, j * cellSize);
                    else if (map[i, j].itemsLying.Count == 1) g.DrawImage(bc["items"][map[i,j].itemsLying[0].sprite], i * cellSize, j * cellSize);
                    
                    if (map[i, j].objStanding != null)
                    {
                        g.DrawImage(bc["objects"][map[i, j].objStanding.sprite], i * cellSize, j * cellSize);
                        //g.DrawRectangle(new Pen(Color.Black), i * cellSize+2, j * cellSize, 28, 2);
                        //g.FillRectangle(new SolidBrush(Color.Red), i * cellSize+2, j * cellSize, 28 * map[i,j].unitStanding / (map[i,j].unitStanding.astr*10), 2);
                    }
                    
                }
            foreach(Unit u in units)
            {
                int X = u.location.X * cellSize;
                int Y = u.location.Y * cellSize;
                int oldX = u.oldLocation.X * cellSize;
                int oldY = u.oldLocation.Y * cellSize;
                float drawX = X * curToOld + oldX * (1.0f - curToOld);
                float drawY = Y * curToOld + oldY * (1.0f - curToOld);
                g.DrawImage(bc["actors"][u.getSprite() + u.direction.ToString()], drawX, drawY);
                if (u != hero)
                {
                    g.DrawRectangle(new Pen(Color.Black), drawX + 2, drawY, cellSize - 4, 2);
                    g.FillRectangle(new SolidBrush(Color.Red), drawX + 2, drawY, (cellSize - 4) * ((Monster)u).def.hp / ((Monster)u).def.maxhp, 2);
                }
                else
                {
                    g.DrawRectangle(new Pen(Color.Black), drawX + 2, drawY, cellSize - 4, 2);
                    g.FillRectangle(new SolidBrush(Color.Red), drawX + 2, drawY, (cellSize - 4) * ((Hero)u).def.hp / ((Hero)u).def.maxhp, 2);
                }
                
            }

            if (inventoryGUI.visible) inventoryGUI.draw(g, width, height);
            g.DrawString(moveAvailable.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);
            g.DrawString(tick.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 20);
            g.DrawString(heroMoveTick.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 40);
           
            //g.DrawString(hero.hp.ToString() + "/" + (hero.astr * 10).ToString(), new Font("Arial", 16), new SolidBrush(Color.Red), new PointF(0, 0));
            //g.DrawString(heroMoveDelta.ToString(), new Font("Arial", 16), new SolidBrush(Color.Black), 0, 0);
        }

        public void makeMoves()
        {
            for (int i = 0; i < units.Count; ++i)
            {
                if(units[i].def.hp<=0)
                {
                    map[units[i].location.X, units[i].location.Y].unitStanding = null;
                    map[units[i].location.X, units[i].location.Y].passable = true;
                    units.Remove(units[i]);
                    i--;
                }
            }
            if(!units.Contains(hero))
            {
                MessageBox.Show("gg");
                Application.Exit();
            }
            foreach (var a in units)
            {
                if (a == hero) continue;
                a.makeMove(this);
            }
            updateCells();
        }

        public int calcDmg(AttackInfo atk,DefendInfo def)
        {
            return atk.dmg-def.armor;
        }

        public void updateCells()
        {
            for (int i = 0; i < map.width; ++i)
                for (int j = 0; j < map.height; ++j)
                    map[i, j].unitStanding = null;
            foreach (Unit u in units)
                map[u.location.X, u.location.Y].unitStanding = u;
            for (int i = 0; i < map.width; ++i)
                for (int j = 0; j < map.height; ++j)
                {
                    if (map[i, j].unitStanding != null) map[i, j].passable = false;
                    if (map[i, j].objStanding != null) map[i, j].passable = map[i, j].objStanding.passable;
                }
        }

    }
}
