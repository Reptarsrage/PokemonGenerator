using System;

namespace PokemonGenerator.Utilities
{
    class PokemonStatUtility : IPokemonStatUtility
    {
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
        public uint CalculateHitPoints(double baseHitPoints, double iv, double ev, double level)
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
        public uint CalculateStat(double baseStat, double iv, double ev, double level)
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
        public uint CalculateExperiencePoints(string experienceGroup, int level)
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