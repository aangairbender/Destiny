﻿using System;
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
        public int attack;
        AttackInfo heroBaseAttack;
        DefendInfo heroBaseDef;
        public Hero(Point location)
        {
            this.direction = 0;
            this.location = location;
            this.hp = 100;
            this.attack = 5;
            this.sprite = "player";
            AttackInfo heroBaseAttack = new AttackInfo();
            heroBaseAttack.dmg = 5;
            DefendInfo heroBaseDefend = new DefendInfo();
            heroBaseDefend.armor = 1;
        }
    }
}
