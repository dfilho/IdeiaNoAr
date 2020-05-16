using RPGCombatKata.En;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKata.Entities
{
    public class Character
    {
        public Character(int id, int power, ClassesEnumerator characterClass)
        {
            Id = id;
            Health = 1000;
            Level = 1;
            Alive = true;
            Power = power;
            MaxRange = characterClass.GetHashCode();
            Factions = new List<Faction>();
        }
        public int Id { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public bool Alive { get; set; }
        public int Power { get; set; }
        public int MaxRange { get; private set; }
        public List<Faction> Factions { get; set; }
    }
}
