using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Map
    {
        public int width;
        public int height;
        public Cell[,] cells;

        private void generate()
        {
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    cells[i, j] = new Cell("grass", "", true);
            ////////////////////////////////////
            for (int i = 0; i < width; ++i)
            {
                cells[i, 0].setDecoration("obsidian");
                cells[i, 0].setPassable(false);
                cells[i, height - 1].setDecoration("obsidian");
                cells[i, height - 1].setPassable(false);
            }

            for (int i = 1; i < height - 1; ++i)
            {
                cells[0, i].setDecoration("obsidian");
                cells[0, i].setPassable(false);
                cells[width - 1, i].setDecoration("obsidian");
                cells[width - 1, i].setPassable(false);
            }

            //////////////////////////////

            Random rand = new Random();

            int wx = rand.Next(width / 2 - 2, width / 2 + 3);
            int wy = rand.Next(height / 2 - 2, height / 2 + 3);

            cells[wx, wy].setDecoration("well");
            cells[wx, wy].setPassable(false);
            /*
            for (int i = wx - 1; i <= wx + 1; ++i)
                for (int j = wy - 1; j <= wy + 1; ++j)
                    cells[i, j].setTile("tile_stone");*/


            int housesCnt = rand.Next(3, 6);

            for(int h = 0; h < housesCnt;)
            {
                int hwidth = rand.Next(4, 6);
                int hheight = rand.Next(4, 6);
                int x = rand.Next(3, width - 5 - hwidth);
                int y = rand.Next(3, height - 5 - hheight);
                bool canBuild = true;
                for(int i = x - 1; i < x + hwidth + 1 && canBuild; ++i)
                    for(int j = y - 1; j < y + hheight + 1; ++j)
                        if (!cells[i, j].isPassable())
                        {
                            canBuild = false;
                            break;
                        }
                if(canBuild)
                {
                    h++;

                    for (int i = x; i < x + hwidth; ++i)
                        for (int j = y; j < y + hheight; ++j)
                            cells[i, j].setTile("wood");


                    cells[x, y].setDecoration("wall_wood_corner_top_left");
                    cells[x, y].setPassable(false);
                    cells[x + hwidth - 1, y].setDecoration("wall_wood_corner_top_right");
                    cells[x + hwidth - 1, y].setPassable(false);
                    cells[x + hwidth - 1, y + hheight - 1].setDecoration("wall_wood_corner_bottom_right");
                    cells[x + hwidth - 1, y + hheight - 1].setPassable(false);
                    cells[x, y + hheight - 1].setDecoration("wall_wood_corner_bottom_left");
                    cells[x, y + hheight - 1].setPassable(false);

                    for(int i = x + 1; i < x + hwidth - 1; ++i)
                    {
                        cells[i, y].setDecoration("wall_wood_horizontal");
                        cells[i, y].setPassable(false);
                        cells[i, y + hheight - 1].setDecoration("wall_wood_horizontal");
                        cells[i, y + hheight - 1].setPassable(false);
                    }

                    for (int j = y + 1; j < y + hheight - 1; ++j)
                    {
                        cells[x, j].setDecoration("wall_wood_vertical_left");
                        cells[x, j].setPassable(false);
                        cells[x + hwidth - 1, j].setDecoration("wall_wood_vertical_right");
                        cells[x + hwidth - 1, j].setPassable(false);
                    }

                    int doorSide = rand.Next(0, 4);
                    if(doorSide % 2 == 0)
                    {
                        int doorX = rand.Next(x + 1, x + hwidth - 1);
                        if (doorSide == 0) cells[doorX, y].setDecoration("wall_wood_door_horizontal_closed");
                        else cells[doorX, y + hheight - 1].setDecoration("wall_wood_door_horizontal_closed");
                    }
                    else
                    {
                        int doorY = rand.Next(y + 1, y + hheight - 1);
                        if (doorSide == 1) cells[x, doorY].setDecoration("wall_wood_door_vertical_left_closed");
                        else cells[x + hwidth - 1, doorY].setDecoration("wall_wood_door_vertical_right_closed");
                    }

                }
            }

            //////////////////////////
            int treesCnt = rand.Next(30, 61);
            for(int tr = 0; tr < treesCnt; )
            {
                int tx = rand.Next(0, width);
                int ty = rand.Next(0, height);
                if (cells[tx, ty].getTile() != "grass" || !cells[tx, ty].isPassable()) continue;
                bool canPlant = true;
                for (int i = tx - 1; i <= tx + 1 && canPlant; ++i)
                    for (int j = ty - 1; j <= ty + 1; ++j) 
                        if(cells[i, j].getTile() == "wood")
                        {
                            canPlant = false;
                            break;
                        }
                if (!canPlant) continue;
                tr++;
                cells[tx, ty].setDecoration("tree");
                cells[tx, ty].setPassable(false);  
            }
            //////////////////////
            int bushesCnt = rand.Next(15, 31);
            for (int tr = 0; tr < bushesCnt; )
            {
                int tx = rand.Next(0, width);
                int ty = rand.Next(0, height);
                if (cells[tx, ty].getTile() != "grass" || !cells[tx, ty].isPassable()) continue;
                bool canPlant = true;
                for (int i = tx - 1; i <= tx + 1 && canPlant; ++i)
                    for (int j = ty - 1; j <= ty + 1; ++j)
                        if (cells[i, j].getTile() == "wood")
                        {
                            canPlant = false;
                            break;
                        }
                if (!canPlant) continue;
                tr++;
                cells[tx, ty].setDecoration("bush");
                cells[tx, ty].setPassable(false);
            }
            //////////////////////
            int flowersCnt = rand.Next(15, 31);
            for (int tr = 0; tr < flowersCnt; )
            {
                int tx = rand.Next(0, width);
                int ty = rand.Next(0, height);
                if (cells[tx, ty].getTile() != "grass" || !cells[tx, ty].isPassable()) continue;
                bool canPlant = true;
                for (int i = tx - 1; i <= tx + 1 && canPlant; ++i)
                    for (int j = ty - 1; j <= ty + 1; ++j)
                        if (cells[i, j].getTile() == "wood")
                        {
                            canPlant = false;
                            break;
                        }
                if (!canPlant) continue;
                tr++;
                cells[tx, ty].setDecoration("flower");
                cells[tx, ty].setPassable(true);
            }
        }

        public Map(int width, int height)
        {
            this.width = width;
            this.height = height;
            cells = new Cell[width, height];
            generate();
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }

        public Cell this[int x, int y]
        {
            get
            {
                return cells[x, y];
            }
        }
    }
}
