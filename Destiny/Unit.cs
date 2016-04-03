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
       public Point oldLocation;
       public int direction;
       public DefendInfo def;
       public AttackInfo atk;
        // 0 - верх
        // 1 - право
        // 2 - низ
        // 3 - лево
       public String sprite;
       public void attack(Unit target)
       {
           target.def.hp -= this.atk.dmg;
           
       }
       public Point getLocation()
       {
           return location;
       }
       public void setLocation(Point newLocation)
       {
           oldLocation = location;

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
       public virtual void makeMove(World world)
       {

       }
    }
}
