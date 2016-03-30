using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Destiny
{
    class Item 
    {
        public string sprite;
        public int type;
        public bool equipable;
        public bool usable;
        public int slot;
        public string description;
        public string name;
        public virtual void use(Hero hero)
        {

        }
        public virtual bool canEquip(Hero hero)
        {
            return true;
        }
        public virtual bool canUse(Hero hero)
        {
            return true;
        }
        public virtual void equip(Hero hero)
        {

        }
        public virtual void unequip(Hero hero)
        {

        }
    }
}
