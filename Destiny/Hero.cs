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
        public int hp;
        public int maxhp;
        public AttackInfo heroBaseAttack;
        public DefendInfo heroBaseDef;
        public List<Item> inventory;
        public List<Item> slot;
        public String name;
        // 0 - Оружие
        // 1 - Броня
        public Hero(Point location, String name)
        {
            inventory = new List<Item>();
            slot = new List<Item>();
            slot.Add(null);
            slot.Add(null);
            this.direction = 0;
            this.location = location;
            this.oldLocation = location;
            this.hp = 100;
            this.maxhp = hp;
            this.sprite = "player";
            this.name = name;
            heroBaseAttack = new AttackInfo();
            heroBaseAttack.dmg = 2;
            heroBaseDef = new DefendInfo();
            heroBaseDef.armor = 1;
        }
    }
}
