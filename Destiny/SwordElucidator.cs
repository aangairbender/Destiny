using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class SwordElucidator : Item
    {
        private int dmgBonus = 5;
        private int critBonus = 50;
        public SwordElucidator()
        {
            sprite = "sword_elucidator";
            type = 0;
            equipable = true;
            usable = false;
            slot = 0;
            description = "+5 урона\n 50% шанс нанести критический удар";
            name = "Вразумитель 2";
        }

        public override bool canEquip(Hero hero)
        {
            return true;
        }
        public override bool canUse(Hero hero)
        {
            return false;
        }
        public override void equip(Hero hero)
        {
            hero.slot[0] = this;
            hero.atk.dmg += dmgBonus;
            hero.atk.dmg += critBonus;
            if (hero.inventory.Contains(this)) hero.inventory.Remove(this);
        }
        public override void unequip(Hero hero)
        {
            hero.slot[0] = null;
            hero.atk.dmg -= dmgBonus;
            hero.atk.crit -= critBonus;
            hero.inventory.Add(this);
          
        }
        public override void use(Hero hero)
        {
            return;
        }
    }
}
