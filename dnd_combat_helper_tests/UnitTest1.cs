using NUnit.Framework;
using dnd_combat_helper.Classes;
using dnd_combat_helper.Classes.Entities;
using dnd_combat_helper.Classes.Engine;
using System.Collections.Generic;
using System.Linq;

namespace dnd_combat_helper_tests
{
    public class Tests
    {
        PlayerCharacter pc;
        [SetUp]
        public void Setup()
        {
            PlayerCharacterDamageAndDeath();
            PlayerCharacterInitiative();
        }
        public void ResetPC()
        {
            pc = new PlayerCharacter("test", 20, 13, 11);
            pc.RollForInitiative(10);
        }
        [Test]
        public void PlayerCharacterDamageAndDeath()
        {
            ResetPC();            
            pc.ReceiveDamage(19, false);
            Assert.AreEqual(1, pc.CurrentHitPoints);
            Assert.AreEqual(false, pc.IsUnconscious);
            Assert.AreEqual(true, pc.IsAlive);

            ResetPC();
            pc.ReceiveDamage(20, false);
            Assert.AreEqual(0, pc.CurrentHitPoints);
            Assert.AreEqual(true, pc.IsUnconscious);
            Assert.AreEqual(true, pc.IsAlive);

            pc.ReceiveDamage(10, true);
            Assert.AreEqual(2, pc.FailedDeathSaves);

            pc.ReceiveDamage(1, false);
            Assert.AreEqual(false, pc.IsAlive);

            ResetPC();
            pc.ReceiveDamage(20, false);
            pc.DeathSave(10);
            pc.DeathSave(10);
            pc.DeathSave(10);
            Assert.AreEqual(false, pc.IsUnconscious);


            ResetPC();
            pc.ReceiveDamage(40, true);
            Assert.AreEqual(false, pc.IsAlive);           
        }

        public void PlayerCharacterInitiative()
        {
            ResetPC();
            PlayerCharacter slowPC = new PlayerCharacter("slow", 15, 8, 10);
            PlayerCharacter fastPC = new PlayerCharacter("fast", 10, 18, 10);
            List<ICombatant> combatants = new List<ICombatant>();

            slowPC.RollForInitiative(10);
            fastPC.RollForInitiative(10);

            combatants.Add(slowPC);
            combatants.Add(fastPC);
            combatants.Add(pc);

            combatants.Sort();
            Assert.AreEqual(fastPC, combatants.Min());
            Assert.AreEqual(slowPC, combatants.Max());  
            Assert.AreEqual(1, pc.InitiativeModifier);
            Assert.AreEqual(-1, slowPC.InitiativeModifier);
            Assert.AreEqual(4, fastPC.InitiativeModifier);

            slowPC.RollForInitiative(19);
            fastPC.RollForInitiative(14);
            Assert.AreEqual(slowPC.Initiative, fastPC.Initiative);
        }
    }
}