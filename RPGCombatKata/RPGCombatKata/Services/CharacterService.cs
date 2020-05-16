using RPGCombatKata.Entities;
using RPGCombatKata.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGCombatKata.Services
{
    public class CharacterService
    {
        public CharacterResponse DealDamage(Character attacker, Character target, int attackDistance)
        {
            if (attacker.Id == target.Id)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "You cannot deal damage to itself"
                };

            var friends = attacker.Factions.Any(x => target.Factions.Any(y => y.Id == x.Id));
            if (friends)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "Friendly fire is disable"
                };

            if (!target.Alive)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "Stop stop stop, his already dead!"
                };

            if (!attacker.Alive)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "Hey, you are dead. Back to the graveyard!"
                };

            if (attackDistance > attacker.MaxRange)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "Your foe isn't nearby"
                };


            var levelDifference = attacker.Level - target.Level;

            if (levelDifference <= -5)
                target.Health -= (Convert.ToInt32(attacker.Power * 0.5));
            else if (levelDifference >= 5)
                target.Health -= (Convert.ToInt32(attacker.Power / 0.5));
            else
                target.Health -= attacker.Power;

            if (target.Health <= 0)
            {
                target.Alive = false;
                return new CharacterResponse
                {
                    Success = true,
                    Message = "You defeated your opponent"
                };
            }

            return new CharacterResponse
            {
                Success = true,
                Message = $"Your opponent is still alive, his health is {target.Health}"
            };
        }

        public CharacterResponse DealDamage(Character attacker, Thing target, int attackDistance)
        {
            if (target.Destroyed)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "It's destroyed"
                };

            if (!attacker.Alive)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "Hey, you are dead. Back to the graveyard!"
                };

            if (attackDistance > attacker.MaxRange)
                return new CharacterResponse 
                {
                    Success = false,
                    Message = "This thing isn't nearby"
                };

            target.Health -= attacker.Power;

            if (target.Health <= 0)
            {
                target.Destroyed = true;
                return new CharacterResponse
                {
                    Success = true,
                    Message = $"You destroyed {target.Name}"
                };
            }

            return new CharacterResponse
            {
                Success = true,
                Message = $"The {target.Name} still resists, his health is {target.Health}"
            };
        }

        public CharacterResponse HealCharacter(Character healer, Character friend)
        {
            var friends = healer.Factions.Any(x => friend.Factions.Any(y => y.Id == x.Id));
            if (!friends)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "You are not friend of him. Kill him!"
                };
            if (!friend.Alive)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "I'm sorry, he is dead!"
                };
            if (healer.Power > 1000)
                return new CharacterResponse
                {
                    Success = false,
                    Message = "You cannot recover this size of your friends health"
                };

            friend.Health += healer.Power;

            return new CharacterResponse
            {
                Success = true,
                Message = "Your friend is now recovered!"
            };
        }
    }
}
