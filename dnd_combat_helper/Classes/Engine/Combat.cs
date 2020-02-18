using dnd_combat_helper.Classes.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace dnd_combat_helper.Classes.Engine
{
    public class Combat
    {
        public List<ICombatant> combatants { get; private set; } = new List<ICombatant>();
        public Queue<ICombatant> currentRound { get; private set; }
        public Queue<ICombatant> nextRound { get; private set; }

        public void AddCombatant(ICombatant combatant)
        {
            if (!combatants.Contains(combatant))
            {
                combatants.Add(combatant);
            }
        }

        public void RollForInitiative()
        {
            foreach (ICombatant combatant in combatants)
            {
                combatant.RollForInitiative(Functions.RollD20(0));
            }
            //combatants.OrderByDescending(c => c);
        }

        public void SetCombatOrder()
        {
            combatants.Sort();
            currentRound = new Queue<ICombatant>(combatants);
        }

        

        public List<PlayerCharacter> GetPlayerCharacters()
        {
            List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();

            foreach(ICombatant combatant in combatants)
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

        public List<NonPlayerCharacter> GetNonPlayerCharacters()
        {
            List<NonPlayerCharacter> nonPlayerCharacters = new List<NonPlayerCharacter>();

            foreach (ICombatant combatant in combatants)
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
