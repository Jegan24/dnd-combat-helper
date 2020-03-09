using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public abstract class GenericCharacter : ICombatant
    {
        private static int nextId = 1;

        public int localId { get; }

        public int CurrentHitPoints { get; protected set; }

        public int MaxHitPoints { get; protected set; }

        public int Initiative { get; protected set; }

        public int InitiativeModifier
        {
            get
            {
                int modifier = Dexterity - 10;
                bool addRemainder = modifier < 0 && modifier % 2 != 0;
                return (Dexterity - 10) / 2 - (addRemainder ? 1 : 0);
            }
        }

        public int Dexterity { get; protected set; }

        public int ArmorClass { get; protected set; }

        public bool IsAlive { get; protected set; }

        public string Name { get; protected set; }

        protected GenericCharacter(string name, int maxHp, int dexterity, int armorClass, List<DamageType> resistances, List<DamageType> immunites)
        {
            localId = GetNextId();
            Name = name;
            MaxHitPoints = maxHp;
            CurrentHitPoints = MaxHitPoints;
            Dexterity = dexterity;
            ArmorClass = armorClass;

            // Prevent force damage from being added to resistances/immunities, as it is unresistable damage
            if (resistances != null)
            {
                _resistances = new List<DamageType>(resistances);
                if (_resistances.Contains(DamageType.Force))
                {
                    _resistances.Remove(DamageType.Force);
                }
            }
            if (immunites != null)
            {
                _immunities = new List<DamageType>(immunites);
                if (_immunities.Contains(DamageType.Force))
                {
                    _immunities.Remove(DamageType.Force);
                }
            }

            IsAlive = true;
        }

        public event EventHandler<DamageEventArgs> OnDamaged;
        
        protected List<DamageType> _resistances;

        public IEnumerable<DamageType> Resistances
        {
            get
            {
                if (_resistances == null)
                {
                    _resistances = new List<DamageType>();
                }
                return _resistances;
            }

        }
        protected List<DamageType> _immunities;

        public IEnumerable<DamageType> Immunities
        {
            get
            {
                if (_immunities == null)
                {
                    _immunities = new List<DamageType>();
                }
                return _immunities;
            }
        }

        public int CompareTo(ICombatant other)
        {
            int result = 0;
            if (this.Initiative.CompareTo(other.Initiative) == 0)
            {
                if (this.Dexterity.CompareTo(other.Dexterity) == 0)
                {
                    // Both combatants have the same initiative score and dexterity, so the outcome is randomly decided
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
            if (_resistances.Contains(damageType))
            {
                incomingDamage /= 2;
            }
            else if (_immunities.Contains(damageType))
            {
                incomingDamage = 0;
            }

            ReceiveDamage(incomingDamage, isCrit);            
        }

        public void ReceiveDamage(Damage incomingDamage)
        {
            ReceiveDamage(incomingDamage.AmountOfDamage, incomingDamage.DamageType, incomingDamage.IsCrit);
        }

        public void ReceiveDamage(IEnumerable<Damage> incomingDamages)
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

        private static int GetNextId()
        {
            int returnId = nextId;
            nextId++;
            return returnId;
        }

        public static int GetIdOf(ICombatant combatant)
        {
            int id = 0;
            GenericCharacter generic;
            if (combatant is GenericCharacter)
            {
                generic = (GenericCharacter)combatant;
                id = generic.localId;
            }
            return id;
        }
    }
}
