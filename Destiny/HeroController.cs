using System;
using System.Collections.Generic;
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
            bool moveMade = true;
            switch (keyCode)
            {
                case Keys.Up:
                    {

                        break;
                    }
                case Keys.Right:
                    {

                        break;
                    }
                case Keys.Down:
                    {

                        break;
                    }
                case Keys.Left:
                    {

                        break;
                    }
                case Keys.A:
                    {

                        break;
                    }
                default:
                    {
                        moveMade = false;
                        break;
                    }
            }
            return moveMade;
        }
    }
}
