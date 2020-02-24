using dnd_combat_helper.Classes.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Entities
{
    public interface IDamagable
    {
        int CurrentHitPoints { get; }
        int MaxHitPoints { get; }
        IEnumerable<DamageType> Resistances { get; }
        IEnumerable<DamageType> Immunities { get; }
        void ReceiveDamage(Damage incomingDamage);
        void ReceiveDamage(IEnumerable<Damage> incomingDamages);
    }
}
