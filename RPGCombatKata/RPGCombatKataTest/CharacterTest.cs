using NUnit.Framework;
using RPGCombatKata.Entities;
using RPGCombatKata.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKataTest
{
    public class CharacterTest
    {
        readonly CharacterService characterService;
        public CharacterTest()
        {
            characterService = new CharacterService();
        }

        [Test]
        public void DealDamageCharacterValidateId()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var response = characterService.DealDamage(attacker, attacker, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageCharacterSameFactions()
        {
            var faction = new Faction(1, "UnitFaction");
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var target = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);
            attacker.Factions.Add(faction);
            target.Factions.Add(faction);

            var response = characterService.DealDamage(attacker, target, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageCharacterTargetDead()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var target = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);
            target.Alive = false;

            var response = characterService.DealDamage(attacker, target, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageCharacterCharacterDead()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            attacker.Alive = false;
            var target = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);

            var response = characterService.DealDamage(attacker, target, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageCharacterWithoutRange()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var target = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);

            var response = characterService.DealDamage(attacker, target, 50);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageCharacterDefeatTarget()
        {
            var attacker = new Character(1, 500, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var target = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);
            attacker.Level = 10;

            var response = characterService.DealDamage(attacker, target, 1);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void DealDamageCharacterDontDefeatTarget()
        {
            var attacker = new Character(2, 50, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var target = new Character(1, 50, RPGCombatKata.En.ClassesEnumerator.RANGED);

            var response = characterService.DealDamage(attacker, target, 1);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void DealDamageThingAlreadyDestroyed()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var thing = new Thing(1, "Tree", 2000);
            thing.Destroyed = true;
            var response = characterService.DealDamage(attacker, thing, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageThingCharacterDead()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            attacker.Alive = false;
            var thing = new Thing(1, "Tree", 2000);

            var response = characterService.DealDamage(attacker, thing, 2);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageThingWithoutRange()
        {
            var attacker = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var thing = new Thing(1, "Tree", 2000);

            var response = characterService.DealDamage(attacker, thing, 200);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void DealDamageThingDestroyed()
        {
            var attacker = new Character(1, 2000, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var thing = new Thing(1, "Tree", 2000);

            var response = characterService.DealDamage(attacker, thing, 1);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void DealDamageThingDontDestroyThing()
        {
            var attacker = new Character(1, 500, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var thing = new Thing(1, "Tree", 2000);

            var response = characterService.DealDamage(attacker, thing, 1);
            Assert.IsTrue(response.Success);
        }

        [Test]
        public void HealCharacterNotFriends()
        {
            var unitFaction = new Faction(1, "UnitFaction");
            var testFaction = new Faction(2, "TestFaction");
            var healer = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var friend = new Character(2, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            healer.Factions.Add(unitFaction);
            friend.Factions.Add(testFaction);
            var response = characterService.HealCharacter(healer, friend);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void HealCharacterFriendDead()
        {
            var unitFaction = new Faction(1, "UnitFaction");
            var healer = new Character(1, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var friend = new Character(2, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            friend.Alive = false;
            healer.Factions.Add(unitFaction);
            friend.Factions.Add(unitFaction);
            var response = characterService.HealCharacter(healer, friend);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void HealCharacterALotOfPower()
        {
            var unitFaction = new Faction(1, "UnitFaction");
            var healer = new Character(1, 5000, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var friend = new Character(2, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            healer.Factions.Add(unitFaction);
            friend.Factions.Add(unitFaction);
            var response = characterService.HealCharacter(healer, friend);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void HealCharacter()
        {
            var unitFaction = new Faction(1, "UnitFaction");
            var healer = new Character(1, 50, RPGCombatKata.En.ClassesEnumerator.MELEE);
            var friend = new Character(2, 100, RPGCombatKata.En.ClassesEnumerator.MELEE);
            healer.Factions.Add(unitFaction);
            friend.Factions.Add(unitFaction);
            var response = characterService.HealCharacter(healer, friend);
            Assert.IsTrue(response.Success);
        }
    }
}
