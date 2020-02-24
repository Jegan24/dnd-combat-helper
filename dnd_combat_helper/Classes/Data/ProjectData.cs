using dnd_combat_helper.Classes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace dnd_combat_helper.Classes.Data
{
    public class ProjectData
    {
        private static List<ICombatant> _combatants;
        public static IEnumerable<ICombatant> Combatants
        {
            get
            {
                if(_combatants == null)
                {
                    _combatants = new List<ICombatant>();
                }
                return _combatants;
            }
        }

    }
}
