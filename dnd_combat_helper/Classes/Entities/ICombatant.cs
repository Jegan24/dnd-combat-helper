﻿using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public interface ICombatant : IComparable<ICombatant>
    {
        private static int nextId = 1;
        int localId { get; }
        int CurrentHitPoints { get; }
        int MaxHitPoints { get; }
        int Initiative { get; }
        public int InitiativeModifier { get; }
        int Dexterity { get; }
        int ArmorClass { get; }
        bool IsAlive { get; }
        string Name { get; }
        List<DamageType> Resistances { get; }
        List<DamageType> Immunities { get; }
        void ReceiveDamage(int incomingDamage, bool isCrit);
        void ReceiveDamage(int incomingDamage, DamageType damageType, bool isCrit);
        void ReceiveDamage(Damage incomingDamage);
        void ReceiveDamage(List<Damage> incomingDamages);
        void RollForInitiative(int roll);
        void RollForInitiative();
        protected static int GetNextId()
        {
            int returnId = nextId;
            nextId++;
            return returnId;
        }    
    }
}
