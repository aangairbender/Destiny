using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Cell
    {
        public bool passable;
        public String tile;
        public String decoration;
        public List<Item> itemsLying;
        public Unit unitStanding;

        public Cell(String tile, String decoration, bool passable)
        {
            this.tile = tile;
            this.passable = passable;
            this.decoration = decoration;
            itemsLying = new List<Item>();
        }

        public String getTile()
        {
            return tile;
        }

        public void setTile(String tile)
        {
            this.tile = tile;
        }

        public String getDecoration()
        {
            return decoration;
        }

        public void setDecoration(String decoration)
        {
            this.decoration = decoration;
        }

        public bool hasDecoration()
        {
            if (decoration.Length > 0) return true;
            else return false;
        }

        public bool isPassable()
        {
            return passable;
        }
        public void setPassable(bool passable)
        {
            this.passable = passable;
        }
    }
}
