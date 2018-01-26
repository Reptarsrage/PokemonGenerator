using Newtonsoft.Json;
using PokemonGenerator.Enumerations;
using System.Collections.Generic;

namespace PokemonGenerator.Models
{
    public class PokemonGeneratorConfig
    {
        public PokemonGeneratorConfig()
        {
            LegendaryPokemon = new List<int> { 144, 145, 146, 151, 150, 243, 244, 245, 249, 250, 251 };
            IgnoredPokemon = new List<int> { 10, 11, 13, 14, 129, 201 }; /*caterpie, metapod, weedle, kakuna, magikarp, unown */
            SpecialPokemon = new List<int> { 10, 11, 13, 14, 129, 132, 201, 202 }; /* caterpie, metapod, weedle, kakuna, magikarp, ditto, unown, wobbuffet */
            PairedMoves = new Dictionary<int, int[]>()
            {
                    { 156, new int[] { 214, 173 } }, /* Rest = sleep-talk, snore */
                    { 47, new int[] { 138, 171 } }, /* sing = dream-eater, nightmare */
                    { 79, new int[] { 138, 171 } }, /* sleep-powder = dream-eater, nightmare */
                    { 95, new int[] { 138, 171 } }, /* hypnosis = dream-eater, nightmare */
                    { 142, new int[] { 138, 171 } }, /* lovely-kiss = dream-eater, nightmare */
                    { 147, new int[] { 138, 171 } }, /* spore = dream-eater, nightmare */
            };
            DependantMoves = new Dictionary<int, int[]>()
            {
                    { 214, new int[] { 156 } }, /* Rest = sleep-talk, snore */
                    { 173, new int[] { 156 } }, /* hypnosis = dream-eater, nightmare */
                    { 138, new int[] { 47, 79, 95, 142, 147 } }, /* sing = dream-eater, nightmare */
                    { 171, new int[] { 47, 79, 95, 142, 147 } }, /* sleep-powder = dream-eater, nightmare */ 
            };
            HMBank = new List<int>()
            {
                    15, /* cut */
                    19, /* fly */
                    70, /* strength */
                    127, /* waterfall */
                    148, /* flash */
                    250, /* whirlpool */
                    57 /* surf */
            };
            TeamSize = 6;
            MoveEffectFilters = new Dictionary<string, double>()
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
            PokemonLiklihood = new PokemonLiklihood()
            {
                 Ignored = Likeliness.None,
                 Standard = Likeliness.Full,
                 Legendary = Likeliness.Very_Low,
                 Special = Likeliness.Medium_Low
            };
            Mean = 0.5D;
            Skew = 0.3D;
            StandardDeviation = 0.1D;
            SameTypeModifier = 1.5D;
            DamageModifier = 200D;
            PairedModifier = 2D;
            DamageTypeDelta = 15;
            RandomMoveMinPower = 40;
            RandomMoveMaxPower = 100;
        }

        /// <summary>
        /// All Legendary pokemon Ids
        /// </summary>
        [JsonIgnore]
        public List<int> LegendaryPokemon { get; set; }

        /// <summary>
        /// All Special pokemon Ids
        /// Specially treated for move selection bc they can learn &lt; 4 moves total
        /// </summary>
        [JsonIgnore]
        public List<int> SpecialPokemon { get; set; }

        /// <summary>
        /// Moves that go well together
        /// </summary>
        [JsonIgnore]
        public Dictionary<int, int[]> PairedMoves { get; set; }

        /// <summary>
        /// Moves that depend on another to be any use at all
        /// </summary>
        [JsonIgnore]
        public Dictionary<int, int[]> DependantMoves { get; set; }

        /// <summary>
        /// A list of all HM moves
        /// </summary>
        [JsonIgnore]
        public List<int> HMBank { get; set; }

        /// <summary>
        /// Pokemon Team size
        /// </summary>
        [JsonIgnore]
        public int TeamSize { get; set; }

        /// <summary>
        /// Liklihood of the move based on how strong the type generally is
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, double> MoveEffectFilters { get; set; }

        /// <summary>
        /// How likly a pokemon is to be put on a team, based on the class of the pokemon
        /// </summary>
        public PokemonLiklihood PokemonLiklihood { get; set; }

        /// <summary>
        /// All Useless pokemon Ids
        /// </summary>
        public List<int> IgnoredPokemon { get; set; }

        /// <summary>
        /// Average
        /// </summary>
        public double Mean { get; set; }

        /// <summary>
        /// Average skew
        /// </summary>
        public double Skew { get; set; }

        /// <summary>
        /// Standard deviation
        /// </summary>
        public double StandardDeviation { get; set; }

        /// <summary>
        /// The higher this is, the more likely pokemon with a certain type will only know moves of that type.
        /// </summary>
        public double SameTypeModifier { get; set; }

        /// <summary>
        /// The higher this is, the less damage will affect move probability. 
        /// At low values, damage will play a key role in choosing moves
        /// </summary>
        public double DamageModifier { get; set; }

        /// <summary>
        /// The higher this is, pokemon are more likely to have paired moves (<see cref="PairedMoves"/>)
        /// </summary>
        public double PairedModifier { get; set; }

        /// <summary>
        /// if the difference between a pokemon's attack and special attack
        /// exceeds this delta, the pokemon will favor a certain damage type.
        /// Else, it will favor all damage types
        /// </summary>
        public int DamageTypeDelta { get; set; }

        /// <summary>
        /// When a move must be chosen at random (e.g. for sketch), then this is the minimum damage that the move must to
        /// </summary>
        public int RandomMoveMinPower { get; set; }

        /// <summary>
        /// When a move must be chosen at random (e.g. for sketch), then this is the maxinimum damage that the move can to
        /// </summary>
        public int RandomMoveMaxPower { get; set; }
    }
}