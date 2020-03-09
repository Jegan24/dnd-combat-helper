using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public class NonPlayerCharacter : GenericCharacter
    {
        public NonPlayerCharacter(string name,int maxHp, int dexterity, int armorClass, List<DamageType> resistances, List<DamageType> immunites) :
            base(name, maxHp, dexterity, armorClass, resistances, immunites)
        {
            
        }

        public NonPlayerCharacter(string name, int maxHp, int dexterity, int armorClass) :
            base(name, maxHp, dexterity, armorClass, null, null)
        {

        }

        public override void ReceiveDamage(int incomingDamage, bool isCrit)
        {
            CurrentHitPoints -= incomingDamage;
            if (CurrentHitPoints <= 0)
            {
                IsAlive = false;
            }
        }

    }
}
