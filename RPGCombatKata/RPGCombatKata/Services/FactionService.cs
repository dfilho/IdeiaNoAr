using RPGCombatKata.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGCombatKata.Services
{
    public class FactionService
    {
        public bool JoinFaction(Faction faction, Character character)
        {
            if (character.Factions.Where(x => x.Id == faction.Id).FirstOrDefault() == null)
            {
                character.Factions.Add(faction);
                return true;
            }
            else
                return false;
        }

        public bool LeaveFaction(Faction faction, Character character)
        {
            if (character.Factions.Where(x => x.Id == faction.Id).FirstOrDefault() != null)
            {
                character.Factions.Remove(faction);
                return true;
            }
            else
                return false;
        }
    }
}
