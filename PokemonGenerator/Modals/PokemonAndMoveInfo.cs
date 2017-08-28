using PokemonGenerator.DAL.Serialization;
using PokemonGenerator.Modals;
using System.Collections.Generic;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// Class used internally inside of PokemonGernerator to store relavent info for deciding moves.
    /// </summary>
    internal class PokemonAndMoveInfo
    {
        public PokemonAndMoveInfo()
        {
            AllPossibleMovesOrig = new List<uspGetPokemonMoveSetResult>();
            PokeTypes = new List<string>();
            AttackTypesToFavor = new List<string>();
            AlreadyPicked = new List<int>();
            AlreadyPickedEffects = new List<string>();
        }

        public Pokemon Pokemon { get; set; }
        public List<uspGetPokemonMoveSetResult> AllPossibleMovesOrig { get; set; }
        public List<string> PokeTypes { get; set; }
        public DamageType DamageType { get; set; }
        public List<string> AttackTypesToFavor { get; set; }
        public List<int> AlreadyPicked { get; set; }
        public List<string> AlreadyPickedEffects { get; set; }
        public bool DoSomeDamageFlag { get; set; }
    }
}