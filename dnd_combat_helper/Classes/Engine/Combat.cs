using dnd_combat_helper.Classes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dnd_combat_helper.Classes.Engine
{
    public class Combat
    {
        private List<ICombatant> combatants = new List<ICombatant>();

        public void AddCombatant(ICombatant combatant)
        {
            if (!combatants.Contains(combatant))
            {
                combatants.Add(combatant);
            }
        }

        public void RollForInitiative()
        {
            foreach(ICombatant combatant in combatants)
            {
                combatant.RollForInitiative(Functions.RollD20(0));
            }
            combatants.OrderByDescending(c => c);
        }
    }
}
