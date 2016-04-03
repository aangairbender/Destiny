using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class HealthPotion:Item
    {
        public HealthPotion()
        {
            sprite = "health_potion";
            equipable = false;
            usable = true;
            name = "Зелье лечения";
            description = "+10 очков здоровья";
        }
        public override void use(Hero hero)
        {
            hero.def.maxhp += 10;
            hero.inventory.Remove(this);
        }
    }
}
