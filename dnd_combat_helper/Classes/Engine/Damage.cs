using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Engine
{
    public class Damage
    {
        public int AmountOfDamage { get; }
        public DamageType DamageType { get; }
        public bool IsCrit { get; }

        public Damage(int amountOfDamage, DamageType damageType, bool isCrit)
        {
            AmountOfDamage = amountOfDamage;
            this.DamageType = damageType;
            IsCrit = isCrit;
        }

    }
}
