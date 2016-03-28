using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Unit 
    {
       public Point location;
       public int direction;
       public String sprite;
       public Point getLocation()
       {
           return location;
       }
       public void setLocation(Point newLocation)
       {
           this.location = newLocation;
       }
       public void makeMove(World world)
       {

       }
    }
}
