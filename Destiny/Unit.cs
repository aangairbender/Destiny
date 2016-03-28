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
       protected Point location;
       protected int direction;
       protected String sprite;
       public Point getLocation()
       {
           return location;
       }
       public void setLocation(Point newLocation)
       {
           this.location = newLocation;
       }
       
       public void setSprite(String newSprite)
       {
           this.sprite = newSprite;
       }
       public String getSprite()
       {
           return sprite;
       }
    }
}
