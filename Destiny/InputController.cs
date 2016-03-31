using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Destiny
{
    class InputController
    {
        private World world;
        public bool mouseDowned;
        public Point mouseLocation;
        public InputController(World world)
        {
            this.world = world;
        }
        public void keyDown(KeyEventArgs e)
        {
            if (world.heroController.makeMove(e.KeyCode)) world.makeMoves();
        }
        public void keyUp(KeyEventArgs e)
        {

        }
        public void mouseDown(MouseEventArgs e)
        {
            mouseDowned = true;
            mouseLocation = e.Location;
        }
        public void mouseUp(MouseEventArgs e)
        {
            mouseDowned = false;
            mouseLocation = e.Location;
        }
        public void mouseMove(MouseEventArgs e)
        {
            mouseLocation = e.Location;
        }
    }
}
