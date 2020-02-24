using dnd_combat_helper.Classes.Engine;
using dnd_combat_helper.Classes.Entities;
using System;

namespace dnd_combat_helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Combat combat = new Combat();
            ICombatant player1 = new PlayerCharacter("player 1", 10, 10, 10, null, null);
            ICombatant player2 = new PlayerCharacter("player 2", 15, 15, 15, null, null);
            ICombatant player3 = new PlayerCharacter("player 3", 5, 5, 5, null, null);
            ICombatant player4 = new PlayerCharacter("player 4", 20, 20, 20, null, null);
            ICombatant enemy1 = new NonPlayerCharacter("enemy 1", 15, 10, 10, null, null);
            ICombatant enemy2 = new NonPlayerCharacter("enemy 2", 10, 15, 10, null, null);
            ICombatant enemy3 = new NonPlayerCharacter("enemy 3", 10, 10, 15, null, null);
            ICombatant enemy4 = new NonPlayerCharacter("enemy 4", 12, 12, 12, null, null);

            combat.AddCombatant(player1);
            combat.AddCombatant(player2);
            combat.AddCombatant(player3);
            combat.AddCombatant(player4);
            combat.AddCombatant(enemy1);
            combat.AddCombatant(enemy2);
            combat.AddCombatant(enemy3);
            combat.AddCombatant(enemy4);

            foreach(ICombatant combatant in combat.Combatants)
            {
                combatant.RollForInitiative();
            }

            combat.SetCombatOrder();

            foreach (ICombatant combatant in combat.Combatants)
            {                
                Console.WriteLine($"{GenericCharacter.GetIdOf(combatant).ToString()}. {combatant.Name}, Initiative: {combatant.Initiative}, Current HP: {combatant.CurrentHitPoints}, AC: {combatant.ArmorClass}");
            }
        }
    }
}
