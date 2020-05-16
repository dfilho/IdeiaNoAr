using System;
using System.Collections.Generic;
using System.Text;

namespace RPGCombatKata.Entities
{
    public class Faction
    {
        public Faction(int id, string nameFaction)
        {
            Id = id;
            NameFaction = nameFaction;
        }
        public int Id { get; set; }
        public string NameFaction { get; set; }
    }
}
