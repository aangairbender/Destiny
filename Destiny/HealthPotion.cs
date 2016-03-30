using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class HealthPotion:Item
    {
        public override void use(Hero hero)
        {
            hero.maxhp += 10;
        }
    }
}
