using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Hero:Unit
    {
        int hp;
        int attack;
        public Hero(int id,Point location,String sprite,int hp, int attack)
        {
            this.hp = hp;
            this.attack = attack;
            this.location = location;
            this.direction = 0;
            this.sprite[0] = sprite;
            //asdasdвылдвалвыываыва
            
        }
    }
}
