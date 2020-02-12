using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public class Monster : ICombatant
    {
        public int CurrentHitPoints { get; private set; }

        public int MaxHitPoints { get; private set; }

        public int Initiative { get; private set; }

        public int Dexterity { get; private set; }

        public int ArmorClass { get; private set; }

        public bool IsAlive { get; private set; }

        public string Name { get; private set; }

        public List<DamageType> Resistances { get; private set; }

        public List<DamageType> Immunities { get; private set; }

        public int CompareTo(ICombatant other)
        {
            int result = 0;
            if (this.Initiative.CompareTo(other.Initiative) == 0)
            {
                if (this.Dexterity.CompareTo(other.Dexterity) == 0)
                {
                    if (Functions.CoinFlip())
                    {
                        result = 1;
                    }
                    else
                    {
                        result = -1;
                    }
                }
                else
                {
                    result = this.Dexterity.CompareTo(other.Dexterity);
                }
            }
            else
            {
                result = this.Initiative.CompareTo(other.Initiative);
            }
            return result;
        }

        public void ReceiveDamage(int incomingDamage, bool isCrit)
        {
            CurrentHitPoints -= incomingDamage;
            if(CurrentHitPoints <= 0)
            {
                IsAlive = false;
            }
        }

        public void ReceiveDamage(int incomingDamage, DamageType damageType)
        {
            throw new NotImplementedException();
        }

        public void ReceiveDamage(Damage incomingDamage)
        {
            throw new NotImplementedException();
        }

        public void ReceiveDamage(List<Damage> incomingDamages)
        {
            throw new NotImplementedException();
        }

        public void RollForInitiative(int roll)
        {
            throw new NotImplementedException();
        }
    }
}
