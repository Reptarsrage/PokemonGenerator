using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Enumerations;
using PokemonGenerator.Models.Gernerator;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace PokemonGenerator.Providers
{
    public interface IPokemonMoveProvider
    {
        Pokemon AssignMoves(Pokemon poke, int level);
    }

    public class PokemonMoveProvider : IPokemonMoveProvider
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IProbabilityUtility _probabilityUtility;
        private readonly IOptions<PersistentConfig> _config;
        private readonly Random _random;

        public PokemonMoveProvider(
            IPokemonRepository pokemonRepository,
            IProbabilityUtility probabilityUtility,
            IOptions<PersistentConfig> config,
            Random random)
        {
            _pokemonRepository = pokemonRepository;
            _probabilityUtility = probabilityUtility;
            _random = random;
            _config = config;
        }

        /// <summary>
        /// Retrieves a list of all possible moves for each pokemon at the given level (whether by TM, HM, breeding or leveling up).
        /// Uses this list of moves to give each pokemon four moves. Uses a TMBank of all possible TM's available in the game to make sure that
        /// no team is unrealistically stacked with many TM's. Once a pokemon uses a TM to learn a move, that TM is consumed and can not be used by any other pokemon
        /// on the team.
        /// </summary>
        /// <param name="poke">Pokemon to choose for.</param>
        /// <param name="level">The level of each pokemon.</param>
        public Pokemon AssignMoves(Pokemon poke, int level)
        {
            var tmBank = _pokemonRepository.GetTMs().ToList();
            var moves = _pokemonRepository.GetMovesForPokemon(poke.SpeciesId, level).ToList();
            return AssignMovestoPokemon(poke, moves, tmBank);
        }

        /// <summary>
        /// Fills in all the params needed to calculate 4 moves for a pokmeon. Treats special pokemon uniquely <see cref="SPECIALPOKEMON"/>.
        /// </summary>
        /// <param name="poke">Pokemon to choose moves for</param>
        /// <param name="allPossibleMoves">A list of all possible moves available for the pokemon to learn</param>
        /// <param name="TMBank">The curreent bank of TM's available</param>
        /// <returns>A pokemon fulley equipped with moves.</returns>
        private Pokemon AssignMovestoPokemon(Pokemon poke, IList<PokemonMoveSetResult> allPossibleMoves, IList<int> TMBank)
        {
            var chosenMoves = new Stack<int>();
            var info = new PokemonAndMoveInfo
            {
                Pokemon = poke,
                PokeTypes = poke.Types,
                PreferredDamageType = poke.SpAttack > poke.Attack ? DamageType.Special : DamageType.Physical
            };
            info.PreferredDamageType = Math.Abs(poke.SpAttack - poke.Attack) < _config.Value.Configuration.DamageTypeDelta ? DamageType.Both : info.PreferredDamageType;
            var enemiesWeakAgainst = _pokemonRepository.GetWeaknesses(string.Join(",", info.PokeTypes ?? new List<string>(0)));
            info.AttackTypesToFavor = _pokemonRepository.GetWeaknesses(string.Join(",", enemiesWeakAgainst ?? new List<string>(0))).ToList();

            // Prune moves removing and replacing as needed
            var screenedMoves = new List<PokemonMoveSetResult>();
            foreach (var move in allPossibleMoves)
            {
                // Generate random move for sketch
                if (move.MoveId == 166)
                {
                    var randos = _pokemonRepository.GetRandomMoves(_config.Value.Configuration.RandomMoveMinPower, _config.Value.Configuration.RandomMoveMaxPower).ToList();
                    screenedMoves.Add(randos[_random.Next(0, randos.Count)]);
                    continue;
                }

                // remove duplicates
                if (screenedMoves.Any(m => m.Equals(move)))
                {
                    continue;
                }

                // Remove if unavailable in TM bank and HM bank
                if (string.Equals(move.LearnType, "machine", StringComparison.OrdinalIgnoreCase) && 
                    !_config.Value.Configuration.HMBank.Contains(move.MoveId) && !TMBank.Contains(move.MoveId))
                {
                    continue;
                }

                screenedMoves.Add(move);
            }

            allPossibleMoves = screenedMoves;

            // Choose moves
            if (_config.Value.Configuration.SpecialPokemon.Contains(poke.SpeciesId))
            {
                foreach (var m in allPossibleMoves)
                {
                    chosenMoves.Push(m.MoveId);
                }
            }
            else
            {
                info.AllPossibleMovesOrig = new List<PokemonMoveSetResult>(allPossibleMoves);
                info.DoSomeDamageFlag = false;

                for (var i = 0; i < 4; i++)
                {
                    var move = ChooseMove(info);
                    chosenMoves.Push(move);
                    info.DoSomeDamageFlag = !info.DoSomeDamageFlag;
                    if (TMBank.Contains(move))
                    {
                        TMBank.Remove(move);
                    }
                }
            }

            // We aren't using these
            poke.Move1PowerPointsUps = 0;
            poke.Move2PowerPointsUps = 0;
            poke.Move3PowerPointsUps = 0;
            poke.Move4PowerPointsUps = 0;

            // Set moves
            poke.MoveIndex1 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.MoveIndex2 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.MoveIndex3 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.MoveIndex4 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);

            // Set move names
            poke.Move1Name = allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex1)?.MoveName ?? "";
            poke.Move2Name = allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex2)?.MoveName ?? "";
            poke.Move3Name = allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex3)?.MoveName ?? "";
            poke.Move4Name = allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex4)?.MoveName ?? "";

            // Set pp
            poke.Move1PowerPointsCurrent = (byte)(allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex1)?.Pp ?? 0);
            poke.Move2PowerPointsCurrent = (byte)(allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex2)?.Pp ?? 0);
            poke.Move3PowerPointsCurrent = (byte)(allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex3)?.Pp ?? 0);
            poke.Move4PowerPointsCurrent = (byte)(allPossibleMoves.ToList().FirstOrDefault(m => m.MoveId == poke.MoveIndex4)?.Pp ?? 0);

            return poke;
        }

        /// <summary>
        /// Chooses 4 moves for a pokemon.
        /// </summary>
        /// <param name="info">The parameters to use when choosing a move.</param>
        /// <returns>An Id for the move that was chosen.</returns>
        /// <exception cref="ArgumentException">If there are not enough moves to choose from.</exception>
        private int ChooseMove(PokemonAndMoveInfo info)
        {
            // Try with current damage setting
            if (!PickOneMove(info, out var chosenMoveId))
            {
                // Give up
                throw new ArgumentException("Not enough moves to choose from!");
            }
            info.AlreadyPicked.Add(chosenMoveId);
            var move1Obj = info.AllPossibleMovesOrig.First(m => m.MoveId == chosenMoveId);
            info.AlreadyPickedEffects.Add(move1Obj.Effect);
            return chosenMoveId;
        }

        /// <summary>
        /// By far the most complex method here. 
        /// Uses the parameters to choose one move for a pokemon by assigning a probability value to each possible move. 
        /// The higher this value is compared to all of the other moves, the more likely this move is to be chosen.
        /// (See: <see cref="GeneratorConfig"/>, <see cref="PokemonAndMoveInfo"/>)
        /// </summary>
        private bool PickOneMove(PokemonAndMoveInfo info, out int chosenMoveId)
        {
            chosenMoveId = 0;
            var allPossibleMoves = new List<PokemonMoveSetResult>(info.AllPossibleMovesOrig);

            // Calculate probabilities
            var moveProbabilities = allPossibleMoves.Select(m =>
            {
                var moveChoice = new MoveChoice
                {
                    Probability = Likeliness.Full,
                    Move = m
                };

                // filter out all non-damaging/damaging attacks depending on the flag
                if (info.DoSomeDamageFlag && (m.Power ?? 0) <= 0)
                {
                    moveChoice.Probability *= Likeliness.Extremely_Low;
                }
                
                if (!info.DoSomeDamageFlag && (m.Power ?? 0) > 0)
                {
                    moveChoice.Probability *= Likeliness.Extremely_Low;
                }

                // Apply weight on damage type
                // if damage type does not mesh with the pokemon, make the likelyhood VERY unlikely
                if ((info.PreferredDamageType == DamageType.Special && (m.DamageType ?? "special").Equals("physical")) ||
                    (info.PreferredDamageType == DamageType.Physical && (m.DamageType ?? "special").Equals("special")))
                {
                    moveChoice.Probability *= _config.Value.Configuration.DamageTypeModifier;
                }

                // Apply weight on effect
                foreach (var key in _config.Value.Configuration.MoveEffectFilters.Keys)
                {
                    if (m.Effect.Contains(key))
                    {
                        moveChoice.Probability *= _config.Value.Configuration.MoveEffectFilters[key];
                    }
                }

                // Apply weight on type
                if ((info.PokeTypes?.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase) ?? false)  ||
                    (info.AttackTypesToFavor.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)))
                {
                    moveChoice.Probability *= _config.Value.Configuration.SameTypeModifier;
                }

                // Apply weight on damage
                if (info.DoSomeDamageFlag)
                {
                    moveChoice.Probability *= Likeliness.Full + (m.Power ?? 0) / _config.Value.Configuration.DamageModifier;
                }

                // Apply special weight for paired moves
                if (info.AlreadyPicked.Any(id => _config.Value.Configuration.PairedMoves.ContainsKey(id) && _config.Value.Configuration.PairedMoves[id].Contains(m.MoveId)))
                {
                    moveChoice.Probability = Likeliness.Full * _config.Value.Configuration.PairedModifier;
                }

                // Filter out dependant moves which do not already have their dependency picked
                if (_config.Value.Configuration.DependantMoves.ContainsKey(m.MoveId) && !_config.Value.Configuration.DependantMoves[m.MoveId].Intersect(info.AlreadyPicked).Any())
                {
                    moveChoice.Probability = Likeliness.None;
                }

                // Apply weight on similar moves to already picked
                if (!string.IsNullOrWhiteSpace(m.Effect) &&  info.AlreadyPickedEffects.Contains(m.Effect, StringComparer.CurrentCultureIgnoreCase))
                {
                    moveChoice.Probability *= _config.Value.Configuration.AlreadyPickedMoveEffectsModifier;
                }

                // Finally, apply weight on moves already picked
                if (info.AlreadyPicked.Contains(m.MoveId))
                {
                    moveChoice.Probability *= _config.Value.Configuration.AlreadyPickedMoveModifier;
                }

                return moveChoice;
            }).ToList();

            // Return false if we filtered out all of our choices
            if (!moveProbabilities.Any() || moveProbabilities.All(choice => choice.Probability == 0))
            {
                return false;
            }

            // Choose with probabilities
            var chosen = _probabilityUtility.ChooseWithProbability(moveProbabilities.Cast<IChoice>().ToList());
            if (chosen != null)
            {
                chosenMoveId = moveProbabilities[(int)chosen].Move.MoveId;
                return true;
            }
            return false;
        }
    }
}