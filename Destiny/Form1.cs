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
            w.draw(g, bmp.Width, bmp.Height);

            pictureBox1.Image = bmp;
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
            w.inputController.keyDown(e);
            q();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            w.inputController.keyUp(e);
            q();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            w.inputController.mouseDown(e);
            q();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            w.inputController.mouseMove(e);
            q();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            w.inputController.mouseUp(e);
            q();
        }

    }
}
