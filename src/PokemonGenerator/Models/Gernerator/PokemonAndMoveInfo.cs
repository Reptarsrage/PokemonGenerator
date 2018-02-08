using PokemonGenerator.Models.Serialization;
using System.Collections.Generic;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Enumerations;

namespace PokemonGenerator.Models.Gernerator
{
    /// <summary>
    /// Class used internally inside of PokemonGernerator to store relavent info for deciding moves.
    /// </summary>
    internal class PokemonAndMoveInfo
    {
        public PokemonAndMoveInfo()
        {
            AllPossibleMovesOrig = new List<PokemonMoveSetResult>();
            PokeTypes = new List<string>();
            AttackTypesToFavor = new List<string>();
            AlreadyPicked = new List<int>();
            AlreadyPickedEffects = new List<string>();
        }

        public Pokemon Pokemon { get; set; }
        public IList<PokemonMoveSetResult> AllPossibleMovesOrig { get; set; }
        public IList<string> PokeTypes { get; set; }
        public DamageType DamageType { get; set; }
        public IList<string> AttackTypesToFavor { get; set; }
        public IList<int> AlreadyPicked { get; set; }
        public IList<string> AlreadyPickedEffects { get; set; }
        public bool DoSomeDamageFlag { get; set; }
    }
}