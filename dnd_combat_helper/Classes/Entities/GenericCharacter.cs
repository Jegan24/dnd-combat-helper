using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public abstract class GenericCharacter : ICombatant
    {
        public int localId { get; }
        public int CurrentHitPoints { get; private set; }

        public int MaxHitPoints { get; private set; }

        public int Initiative { get; private set; }

        public int InitiativeModifier
        {
            get
            {
                int modifier = Dexterity - 10;
                bool addRemainder = modifier < 0 && modifier % 2 != 0;
                return (Dexterity - 10) / 2 - (addRemainder ? 1 : 0);
            }
        }

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
            return result * -1;
        }

        public abstract void ReceiveDamage(int incomingDamage, bool isCrit);

        public void ReceiveDamage(int incomingDamage, DamageType damageType, bool isCrit)
        {
            if (Resistances.Contains(damageType))
            {
                incomingDamage /= 2;
            }
            else if (Immunities.Contains(damageType))
            {
                incomingDamage = 0;
            }

            ReceiveDamage(incomingDamage, isCrit);
        }

        public void ReceiveDamage(Damage incomingDamage)
        {
            ReceiveDamage(incomingDamage.AmountOfDamage, incomingDamage.DamageType, incomingDamage.IsCrit);
        }

        public void ReceiveDamage(List<Damage> incomingDamages)
        {
            foreach (Damage damage in incomingDamages)
            {
                ReceiveDamage(damage);
            }
        }

        public void RollForInitiative()
        {
            Initiative = Functions.RollD20(InitiativeModifier);
        }

        public void RollForInitiative(int roll)
        {
            Initiative = roll + InitiativeModifier;
        }
    }
}
