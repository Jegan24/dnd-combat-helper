using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public class PlayerCharacter : GenericCharacter
    {
        #region properties

        public bool IsUnconscious
        {
            get
            {
                return _IsUnconscious;
            }
            private set
            {
                _failedDeathSaves = 0;
                _successfulDeathSaves = 0;
                _IsUnconscious = value;
            }
        }
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
                    IsUnconscious = false;
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


        #endregion
        private bool _IsUnconscious = false;
        private int _failedDeathSaves = 0;
        private int _successfulDeathSaves = 0;
        public void DeathSave()
        {
            DeathSave(Functions.RollD20(0));
        }

        public void DeathSave(int roll)
        {
            if (IsUnconscious)
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

        public PlayerCharacter(string name, int maxHp, int dexterity, int armorClass, List<DamageType> resistances, List<DamageType> immunites) :
            base(name, maxHp, dexterity, armorClass, resistances, immunites)
        {
            IsUnconscious = false;
        }

        public PlayerCharacter(string name, int maxHp, int dexterity, int armorClass) :
            base(name, maxHp, dexterity, armorClass, null, null)
        {
            IsUnconscious = false;
        }



        public string GetMenuOptionString()
        {
            return $"{localId}: {Name}";
        }

        public override void ReceiveDamage(int incomingDamage, bool isCrit)
        {
            CurrentHitPoints -= incomingDamage;
            if (CurrentHitPoints <= MaxHitPoints * -1)
            {
                IsAlive = false;
            }
            else if (CurrentHitPoints <= 0)
            {
                if (IsUnconscious)
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
                    IsUnconscious = true;
                }
            }
        }
    }
}
