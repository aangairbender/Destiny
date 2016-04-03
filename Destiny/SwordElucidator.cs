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
        public SwordElucidator()
        {
            sprite = "sword_elucidator";
            type = 0;
            equipable = true;
            usable = false;
            slot = 0;
            description = "+5 урона";
            name = "Вразумитель";
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
            if (hero.inventory.Contains(this)) hero.inventory.Remove(this);
        }
        public override void unequip(Hero hero)
        {
            hero.slot[0] = null;
            hero.atk.dmg -= dmgBonus;
            hero.inventory.Add(this);
          
        }
        public override void use(Hero hero)
        {
            return;
        }
    }
}
