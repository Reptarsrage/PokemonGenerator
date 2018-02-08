using PokemonGenerator.DAL;
using PokemonGenerator.Models.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator.Utilities
{
    interface IPokemonStatUtility
    {
        void GetTeamBaseStats(PokeList list, int level);
        void AssignIVsAndEVsToTeam(PokeList list, int level);
        IEnumerable<int> GetPossiblePokemon(int level);
        void CalculateStatsForTeam(PokeList list, int level);
    }

    class PokemonStatUtility : IPokemonStatUtility
    {
        private readonly IPokemonDA _pokemonDA;
        private readonly IProbabilityUtility _probabilityUtility;

        public PokemonStatUtility(IPokemonDA pokemonDA, IProbabilityUtility probabilityUtility)
        {
            _pokemonDA = pokemonDA;
            _probabilityUtility = probabilityUtility;
        }

        /// <summary>
        /// Retrieves and sets all of the base stat values for the team.
        /// <para /> 
        /// Base Stats include:       <para /> 
        /// attack  The base attack power for the pokemon.       <para /> 
        /// defense  The base defense for the pokemon.       <para /> 
        /// speed  The base speed for the pokemon.       <para /> 
        /// maxHp  The base maxHp for the pokemon.       <para /> 
        /// spDefense  The base spDefense for the pokemon.       <para /> 
        /// spAttack  The base spAttack power for the pokemon.       <para /> 
        /// level  The level for the pokemon.       <para /> 
        /// experience  The XP that the pokemon would have at the given level.       <para /> 
        /// name  The pokemon's name and nickname.       <para /> 
        /// types  The Pokemon's type(s) (used for move selection).       <para /> 
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="level"></param>
        public void GetTeamBaseStats(PokeList list, int level)
        {
            var stats = _pokemonDA.GetTeamBaseStats(list);
            foreach (var s in stats)
            {
                var idx = list.Species.ToList().IndexOf((byte)s.Id);

                // Set Name
                list.Names[idx] = s.Identifier.ToUpper();

                // Set Stats
                list.Pokemon[idx].Attack = (byte)s.Attack;
                list.Pokemon[idx].Defense = (byte)s.Defense;
                list.Pokemon[idx].Speed = (byte)s.Speed;
                list.Pokemon[idx].MaxHp = (ushort)s.Hp;
                list.Pokemon[idx].SpDefense = (byte)s.SpDefense;
                list.Pokemon[idx].SpAttack = (byte)s.SpAttack;
                list.Pokemon[idx].Level = (byte)level;
                list.Pokemon[idx].Experience = (uint)CalculateExperiencePoints(s.GrowthRate, level);

                // Set Others
                list.Pokemon[idx].Name = s.Identifier.ToUpper();
                list.Pokemon[idx].Types = s.Types.Split(new char[] { ',' }).ToList();
            }
        }

        /// <summary>
        /// Chooses both IV and EV values for each pokemon. 
        /// Uses a gaussian distribution with a mean in the middle and a std deviation of around 30%.
        /// </summary>
        /// <param name="list">List of pokemon on the team.</param>
        public void AssignIVsAndEVsToTeam(PokeList list, int level)
        {
            foreach (var poke in list.Pokemon)
            {
                // EVs between 0-65535
                poke.AttackEV = (ushort)_probabilityUtility.GaussianRandomSkewed(0, 65535, level / 100D);
                poke.DefenseEV = (ushort)_probabilityUtility.GaussianRandomSkewed(0, 65535, level / 100D);
                poke.HitPointsEV = (ushort)_probabilityUtility.GaussianRandomSkewed(0, 65535, level / 100D);
                poke.SpecialEV = (ushort)_probabilityUtility.GaussianRandomSkewed(0, 65535, level / 100D);
                poke.SpeedEV = (ushort)_probabilityUtility.GaussianRandomSkewed(0, 65535, level / 100D);

                // IVs between 0-15
                poke.AttackIV = (byte)_probabilityUtility.GaussianRandom(0, 15);
                poke.DefenseIV = (byte)_probabilityUtility.GaussianRandom(0, 15);
                poke.SpecialIV = (byte)_probabilityUtility.GaussianRandom(0, 15);
                poke.SpeedIV = (byte)_probabilityUtility.GaussianRandom(0, 15);
            }
        }

        public IEnumerable<int> GetPossiblePokemon(int level)
        {
            return _pokemonDA.GetPossiblePokemon(level);
        }

        public void CalculateStatsForTeam(PokeList pokeList, int level)
        {
            foreach (var poke in pokeList.Pokemon)
            {
                poke.MaxHp = (ushort)CalculateHitPoints(poke.MaxHp, 15D, poke.HitPointsEV, level);
                poke.CurrentHp = poke.MaxHp;
                poke.Attack = (ushort)CalculateStat(poke.Attack, poke.AttackIV, poke.AttackEV, level);
                poke.Defense = (ushort)CalculateStat(poke.Defense, poke.DefenseIV, poke.DefenseEV, level);
                poke.SpAttack = (ushort)CalculateStat(poke.SpAttack, poke.SpecialIV, poke.SpecialEV, level);
                poke.SpDefense = (ushort)CalculateStat(poke.SpDefense, poke.SpecialIV, poke.SpecialEV, level);
                poke.Speed = (ushort)CalculateStat(poke.Speed, poke.SpeedIV, poke.SpeedEV, level);
            }
        }

        /// <summary>
        /// Calculates the max hp for a pokemon based on it's base stat value, IV and EV values using a standard formula for Generations I and II.
        /// <para />
        /// http://bulbapedia.bulbagarden.net/wiki/Statistic
        /// </summary>
        /// <param name="baseHitPoints">Base HP Stat</param>
        /// <param name="iv">IV (0-15)</param>
        /// <param name="ev">EV (0-65535)</param>
        /// <param name="level">level of pokemon (5-100)</param>
        /// <returns>The max HP for the pokemon</returns>
        internal uint CalculateHitPoints(double baseHitPoints, double iv, double ev, double level)
        {
            return (uint)(Math.Floor(((baseHitPoints + iv) * 2D + Math.Floor(Math.Ceiling(Math.Sqrt(ev)) / 4D)) * level / 100D) + level + 10D);
        }

        /// <summary>
        /// Calculates the stat for a pokemon based on it's base stat value, IV and EV values using a standard formula for Generations I and II.
        /// <para />
        /// http://bulbapedia.bulbagarden.net/wiki/Statistic
        /// </summary>
        /// <param name="baseStat">Base Stat Value</param>
        /// <param name="iv">IV (0-15)</param>
        /// <param name="ev">EV (0-65535)</param>
        /// <param name="level">level of pokemon (5-100)</param>
        /// <returns>The calculated stat for the pokemon</returns>
        internal uint CalculateStat(double baseStat, double iv, double ev, double level)
        {
            return (uint)(Math.Floor(((baseStat + iv) * 2D + Math.Floor(Math.Ceiling(Math.Sqrt(ev)) / 4D)) * level / 100D) + 5D);
        }

        /// <summary>
        /// Calcualtes the experience points a pokemon would need to attain the given level.
        /// <para/> 
        /// http://bulbapedia.bulbagarden.net/wiki/Experience
        /// </summary>
        /// <param name="experienceGroup">The experience group  of the pokemon (See: http://bulbapedia.bulbagarden.net/wiki/Experience )</param>
        /// <param name="level">The level of the pokemon</param>
        /// <returns></returns>
        internal uint CalculateExperiencePoints(string experienceGroup, int level)
        {
            var ret = 0D;
            switch (experienceGroup)
            {
                case "medium-slow":
                    ret = (6D / 5D) * Math.Pow(level, 3D) - 15D * Math.Pow(level, 2D) + 100D * level - 140D;
                    break;
                case "fast":
                    ret = 4D * Math.Pow(level, 3D) / 5D;
                    break;
                case "slow":
                    ret = 5D * Math.Pow(level, 3D) / 4D;
                    break;
                case "medium":
                case "medium-fast":
                default:
                    ret = Math.Pow(level, 3D);
                    break;
            }
            return (uint)ret;
        }
    }
}