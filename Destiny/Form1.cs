using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image bmp;

        World w;

        public Form1()
        {
            InitializeComponent();
            w = new World(42, 22);
        }


        void q()
        {
            w.Draw(g);

            pictureBox1.Image = bmp;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            q();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = this.ClientSize;
            if (pictureBox1.Width == 0 || pictureBox1.Height == 0) return;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int px = w.hero.getLocation().X;
            int py = w.hero.getLocation().Y;
            int dx = 0;
            int dy = 0;
            bool moveMade = true;
            switch (e.KeyCode)
            {
                case Keys.NumPad8:
                    dy--;
                    break;
                case Keys.NumPad2:
                    dy++;
                    break;
                case Keys.NumPad6:
                    dx++;
                    break;
                case Keys.NumPad4:
                    dx--;
                    break;
                case Keys.NumPad7:
                    dx--;
                    dy--;
                    break;
                case Keys.NumPad9:
                    dx++;
                    dy--;
                    break;
                case Keys.NumPad1:
                    dx--;
                    dy++;
                    break;
                case Keys.NumPad3:
                    dx++;
                    dy++;
                    break;
                case Keys.O:
                    {
                        for (int i = -1; i <= 1; ++i)
                            for (int j = -1; j <= 1; ++j)
                                if (Math.Abs(i) + Math.Abs(j) != 1) continue;
                                else
                                {
                                    String s = w.map[px + i, py + j].getDecoration();
                                    if (s.Contains("door") && s.Contains("closed"))
                                    {
                                        s = s.Replace("closed", "opened");
                                        w.map[px + i, py + j].setPassable(true);
                                    }
                                    w.map[px + i, py + j].setDecoration(s);
                                }
                        break;
                    }
                case Keys.C:
                    {
                        for (int i = -1; i <= 1; ++i)
                            for (int j = -1; j <= 1; ++j)
                                if (Math.Abs(i) + Math.Abs(j) != 1) continue;
                                else
                                {
                                    String s = w.map[px + i, py + j].getDecoration();
                                    if (s.Contains("door") && s.Contains("opened"))
                                    {
                                        s = s.Replace("opened", "closed");
                                        w.map[px + i, py + j].setPassable(false);
                                    }
                                    w.map[px + i, py + j].setDecoration(s);
                                }
                        break;
                    }
                /*case Keys.F:
                    {
                        if (w.map[px, py].itemsLying.Count > 0)
                        {
                            w.hero.items.AddRange(w.map[px, py].itemsLying);
                            w.map[px, py].itemsLying.Clear();
                        }
                        break;
                    }*/
                /*case Keys.A:
                    {
                        for (int i = -1; i <= 1; ++i)
                            for (int j = -1; j <= 1; ++j)
                                if (Math.Abs(i) + Math.Abs(j) == 0) continue;
                                else
                                {
                                    if (w.map[px + i, py + j].unitStanding == null) continue;
                                    w.attack(w.hero, w.map[px + i, py + j].unitStanding);
                                }
                        break;
                    }*/

                default:
                    moveMade = false;
                    break;
            }
            w.map[px, py].unitStanding = null;
            w.map[px, py].passable = true;
            if (w.map[px + dx, py + dy].isPassable())
            {
                px += dx;
                py += dy;
            }
            w.map[px, py].unitStanding = w.hero;
            w.map[px, py].passable = false;


            w.hero.setLocation(new Point(px, py));

            if (moveMade)
            {
                w.makeMoves();
            }

            q();
        }

    }
}
