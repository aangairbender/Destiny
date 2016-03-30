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
        public int attack;
        public AttackInfo heroBaseAttack;
        public DefendInfo heroBaseDef;
        public List<Item> inventory;
        public List<Item> slot;
        // 0 - Оружие
        // 1 - Броня
        public Hero(Point location)
        {
            inventory = new List<Item>();
            slot = new List<Item>();
            slot.Add(null);
            slot.Add(null);
            this.direction = 0;
            this.location = location;
            this.hp = 100;
            this.maxhp = hp;
            this.attack = 2;
            this.sprite = "player";
            heroBaseAttack = new AttackInfo();
            heroBaseAttack.dmg = 2;
            heroBaseDef = new DefendInfo();
            heroBaseDef.armor = 1;
        }
    }
}
