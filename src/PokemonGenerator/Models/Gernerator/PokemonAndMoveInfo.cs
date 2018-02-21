using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Enumerations;
using PokemonGenerator.Models.Serialization;
using System.Collections.Generic;

namespace PokemonGenerator.Models.Gernerator
{
    /// <summary>
    /// Internal class used to pick moves for one pokemon
    /// </summary>
    class PokemonAndMoveInfo
    {
        public PokemonAndMoveInfo()
        {
            AllPossibleMovesOrig = new List<PokemonMoveSetResult>();
            PokeTypes = new List<string>();
            AttackTypesToFavor = new List<string>();
            AlreadyPicked = new List<int>();
            AlreadyPickedEffects = new List<string>();
        }

        /// <summary>
        /// The pokemon we're choosing moves for
        /// </summary>
        public Pokemon Pokemon { get; set; }

        /// <summary>
        /// Unfiltered set of all possible moves
        /// </summary>
        public IList<PokemonMoveSetResult> AllPossibleMovesOrig { get; set; }

        /// <summary>
        /// The Type(s) of the pokemon we're choosing moves for (Fire Water ect.)
        /// </summary>
        public IList<string> PokeTypes { get; set; }

        /// <summary>
        /// Damage type to favor for this pokemon (Special/Physical)
        /// </summary>
        public DamageType PreferredDamageType { get; set; }

        /// <summary>
        /// Favored attack types (Fire/Water etc.)
        /// </summary>
        public IList<string> AttackTypesToFavor { get; set; }

        /// <summary>
        /// Moves previously picked
        /// </summary>
        public IList<int> AlreadyPicked { get; set; }

        /// <summary>
        /// A list of previously picked move effects.
        /// </summary>
        public IList<string> AlreadyPickedEffects { get; set; }

        /// <summary>
        /// Indicates whether we want to choose a move that does damage or a move that alters status. 
        /// Based on this flag we can prune the total selection of moves available for this pokemon at this level.
        /// </summary>
        public bool DoSomeDamageFlag { get; set; }
    }
}