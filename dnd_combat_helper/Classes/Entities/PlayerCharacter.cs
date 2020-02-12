using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public class PlayerCharacter : ICombatant
    {
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
        public bool IsInDeathSavingThrows
        {
            get
            {
                return _isInDeathSavingThrows;
            }
            private set
            {
                _failedDeathSaves = 0;
                _successfulDeathSaves = 0;
                _isInDeathSavingThrows = value;
            }
        }
        private bool _isInDeathSavingThrows = false;
        public int SuccessfulDeathSaves
        {
            get
            {
                return _successfulDeathSaves;
            }
            set
            {
                _successfulDeathSaves = value;
                if (_successfulDeathSaves >= 3)
                {
                    IsInDeathSavingThrows = false;
                }
            }
        }
        public int FailedDeathSaves
        {
            get
            {
                return _failedDeathSaves;
            }
            set
            {
                _failedDeathSaves = value;
                if (_failedDeathSaves >= 3)
                {
                    IsAlive = false;
                    CurrentHitPoints = 1;
                }
            }
        }
        private int _failedDeathSaves = 0;
        private int _successfulDeathSaves = 0;
        public void DeathSave()
        {
            DeathSave(Functions.RollD20(0));
        }

        public void DeathSave(int roll)
        {
            if (IsInDeathSavingThrows)
            {
                if (roll >= 10)
                {
                    SuccessfulDeathSaves++;
                }
                else
                {
                    FailedDeathSaves++;
                }
            }
        }
        public string Name { get; private set; }

        public List<DamageType> Resistances { get; private set; }

        public List<DamageType> Immunities { get; private set; }

        public PlayerCharacter(string name, int maxHP, int dexterity, int armorClass)
        {
            Name = name;
            MaxHitPoints = maxHP;
            Dexterity = dexterity;
            ArmorClass = armorClass;
            IsAlive = true;
            IsInDeathSavingThrows = false;
            CurrentHitPoints = MaxHitPoints;            
        }

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
            if (CurrentHitPoints <= MaxHitPoints * -1)
            {
                IsAlive = false;
            }
            else if (CurrentHitPoints <= 0)
            {
                if (IsInDeathSavingThrows)
                {
                    if (isCrit)
                    {
                        FailedDeathSaves += 2;
                    }
                    else
                    {
                        FailedDeathSaves += 1;
                    }
                }
                else
                {
                    IsInDeathSavingThrows = true;
                }
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
