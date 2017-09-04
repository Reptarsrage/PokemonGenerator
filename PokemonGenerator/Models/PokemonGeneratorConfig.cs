using Newtonsoft.Json;
using PokemonGenerator.Enumerations;
using System.Collections.Generic;

namespace PokemonGenerator.Models
{
    public class PokemonGeneratorConfig
    {
        /// <summary>
        /// All Legendary pokemon Ids
        /// </summary>
        [JsonIgnore]
        public readonly int[] LEGENDARIES = { 144, 145, 146, 151, 150, 243, 244, 245, 249, 250, 251 };

        /// <summary>
        /// All Useless pokemon Ids
        /// </summary>
        [JsonIgnore]
        public readonly int[] IGNOREPOKEMON = { 10, 11, 13, 14, 129, 201 }; /*caterpie, metapod, weedle, kakuna, magikarp, unown */

        /// <summary>
        /// All Special pokemon Ids
        /// Specially treated for move selection bc they can learn &lt; 4 moves total
        /// </summary>
        [JsonIgnore]
        public readonly int[] SPECIALPOKEMON = { 10, 11, 13, 14, 129, 132, 201, 202 }; /* caterpie, metapod, weedle, kakuna, magikarp, ditto, unown, wobbuffet */

        /// <summary>
        /// Moves that go well together
        /// </summary>
        [JsonIgnore]
        public readonly IDictionary<int, int[]> PAIRED_MOVES = new Dictionary<int, int[]>()
        {
            { 156, new int[] { 214, 173 } }, /* Rest = sleep-talk, snore */
            { 47, new int[] { 138, 171 } }, /* sing = dream-eater, nightmare */
            { 79, new int[] { 138, 171 } }, /* sleep-powder = dream-eater, nightmare */
            { 95, new int[] { 138, 171 } }, /* hypnosis = dream-eater, nightmare */
            { 142, new int[] { 138, 171 } }, /* lovely-kiss = dream-eater, nightmare */
            { 147, new int[] { 138, 171 } }, /* spore = dream-eater, nightmare */
        };

        /// <summary>
        /// Moves that depend on another to be any use at all
        /// </summary>
        [JsonIgnore]
        public readonly IDictionary<int, int[]> DEPENDANT_MOVES = new Dictionary<int, int[]>()
        {
            { 214, new int[] { 156 } }, /* Rest = sleep-talk, snore */
            { 173, new int[] { 156 } }, /* hypnosis = dream-eater, nightmare */
            { 138, new int[] { 47, 79, 95, 142, 147 } }, /* sing = dream-eater, nightmare */
            { 171, new int[] { 47, 79, 95, 142, 147 } }, /* sleep-powder = dream-eater, nightmare */ 
        };

        /// <summary>
        /// A list of all HM moves
        /// </summary>
        [JsonIgnore]
        public readonly IList<int> HM_BANK = new List<int>()
        {
            15, /* cut */
            19, /* fly */
            70, /* strength */
            127, /* waterfall */
            148, /* flash */
            250, /* whirlpool */
            57 /* surf */
        };

        /// <summary>
        /// Pokemon Team size
        /// </summary>
        [JsonIgnore]
        public readonly int TEAM_SIZE = 6;

        /// <summary>
        /// Liklihood of the move based on how strong the type generally is
        /// </summary>
        public IDictionary<string, double> MoveEffectFilters = new Dictionary<string, double>()
        {
             {"faint", Likeliness.Medium_Low }
            ,{"sleep", Likeliness.Medium }
            ,{"charge", Likeliness.Low }
            ,{"no additional effect", Likeliness.High }
            ,{"recoil", Likeliness.Low }
            ,{"confuses the user", Likeliness.Very_Low }
            ,{"heal", Likeliness.Very_High }
            ,{"nothing", Likeliness.None }
            ,{"Cannot lower", Likeliness.None }
            ,{"ends wild battles", Likeliness.None }
        };

        /// <summary>
        /// How likly a pokemon is to be put on a team, based on the class of the pokemon
        /// </summary>
        public IDictionary<PokemonClass, double> POKEMON_LIKLIHOOD = new Dictionary<PokemonClass, double>()
        {
             {PokemonClass.Ignored, Likeliness.None },
             {PokemonClass.Standard, Likeliness.Full },
             {PokemonClass.Legendary, Likeliness.Very_Low },
             {PokemonClass.Special, Likeliness.Medium_Low }
        };

        /// <summary>
        /// Average
        /// </summary>
        public double Mean = 0.5D;

        /// <summary>
        /// Standard deviation
        /// </summary>
        public double StandardDeviation = 0.5D;

        /// <summary>
        /// The higher this is, the more likely pokemon with a certain type will only know moves of that type.
        /// </summary>
        public double SameTypeModifier = 1.5D;

        /// <summary>
        /// The higher this is, the less damage will affect move probability. 
        /// At low values, damage will play a key role in choosing moves
        /// </summary>
        public double DamageModifier = 200D;

        /// <summary>
        /// The higher this is, pokemon are more likely to have paired moves (<see cref="PAIRED_MOVES"/>)
        /// </summary>
        public double PairedModifier = 2D;

        /// <summary>
        /// if the difference between a pokemon's attack and special attack
        /// exceeds this delta, the pokemon will favor a certain damage type.
        /// Else, it will favor all damage types
        /// </summary>
        public int DamageTypeDelta = 15;

        /// <summary>
        /// When a move must be chosen at random (e.g. for sketch), then this is the minimum damage that the move must to
        /// </summary>
        public int RandomMoveMinPower = 40;

        /// <summary>
        /// When a move must be chosen at random (e.g. for sketch), then this is the maxinimum damage that the move can to
        /// </summary>
        public int RandomMoveMaxPower = 100;
    }
}