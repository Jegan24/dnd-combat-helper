using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public interface ICombatant : IComparable<ICombatant>, IDamagable
    {
        
      
        int Initiative { get; }
        public int InitiativeModifier { get; }
        int Dexterity { get; }
        int ArmorClass { get; }
        bool IsAlive { get; }
        string Name { get; }
        
        void ReceiveDamage(int incomingDamage, bool isCrit);
        void ReceiveDamage(int incomingDamage, DamageType damageType, bool isCrit);                
        void RollForInitiative(int roll);
        void RollForInitiative();
        new int CompareTo(ICombatant other);
    }
}
