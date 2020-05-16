using NUnit.Framework;
using RPGCombatKata.Entities;
using RPGCombatKata.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest
{
    public class FactionTest
    {
        readonly FactionService factionService;

        public FactionTest()
        {
            factionService = new FactionService();
        }

        [Test]
        public void JoinFactionError()
        {
            var character = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var faction = new Faction(1, "UnitFaction");
            character.Factions.Add(faction);

            Assert.IsFalse(factionService.JoinFaction(faction, character));
        }

        [Test]
        public void JoinFaction()
        {
            var character = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var faction = new Faction(1, "UnitFaction");

            Assert.IsTrue(factionService.JoinFaction(faction, character));
        }

        [Test]
        public void LeaveFactionError()
        {
            var character = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var faction = new Faction(1, "UnitFaction");

            Assert.IsFalse(factionService.LeaveFaction(faction, character));
        }

        [Test]
        public void LeaveFaction()
        {
            var character = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var faction = new Faction(1, "UnitFaction");
            character.Factions.Add(faction);

            Assert.IsTrue(factionService.LeaveFaction(faction, character));
        }
    }
}
