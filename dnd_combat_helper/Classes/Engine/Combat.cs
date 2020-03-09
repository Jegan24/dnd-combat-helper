using dnd_combat_helper.Classes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dnd_combat_helper.Classes.Engine
{
    public class Combat
    {
        private List<ICombatant> _combatants = new List<ICombatant>();
        public IEnumerable<ICombatant> Combatants { get
            {
                if(_combatants == null)
                {
                    _combatants = new List<ICombatant>();
                }
                return _combatants;
            }
        }
        public Queue<ICombatant> currentRound { get; private set; }
        public Queue<ICombatant> nextRound { get; private set; }

        public void AddCombatant(ICombatant combatant)
        {
            if (!_combatants.Contains(combatant))
            {
                _combatants.Add(combatant);
            }
        }

        public void RollForInitiative()
        {
            foreach (ICombatant combatant in Combatants)
            {
                combatant.RollForInitiative(Functions.RollD20(0));
            }
            //combatants.OrderByDescending(c => c);
        }

        public void SetCombatOrder()
        {
            _combatants.Sort();
            currentRound = new Queue<ICombatant>(Combatants);
        }
        
        public IEnumerable<PlayerCharacter> GetPlayerCharacters()
        {
            List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();

            foreach(ICombatant combatant in Combatants)
            {
                PlayerCharacter temp;
                if(combatant is PlayerCharacter)
                {
                    temp = (PlayerCharacter)combatant;
                    playerCharacters.Add(temp);
                }
            }
            return playerCharacters;
        }

        public IEnumerable<NonPlayerCharacter> GetNonPlayerCharacters()
        {
            List<NonPlayerCharacter> nonPlayerCharacters = new List<NonPlayerCharacter>();

            foreach (ICombatant combatant in Combatants)
            {
                NonPlayerCharacter temp;
                if (combatant is NonPlayerCharacter)
                {
                    temp = (NonPlayerCharacter)combatant;
                    nonPlayerCharacters.Add(temp);
                }
            }
            return nonPlayerCharacters;
        }
    }
}
