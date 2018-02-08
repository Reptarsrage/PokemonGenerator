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

namespace PokemonGenerator.Providers
{
    internal interface IPokemonMoveProvider
    {
        void AssignMovesToTeam(PokeList list, int level);
    }

    internal class PokemonMoveProvider : IPokemonMoveProvider
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IProbabilityUtility _probabilityUtility;
        private readonly PokemonGeneratorConfig _pokemonGeneratorConfig;
        private readonly Random _random;

        public PokemonMoveProvider(
            IPokemonRepository pokemonRepository,
            IProbabilityUtility probabilityUtility,
            PokemonGeneratorConfig pokemonGeneratorConfig,
            Random random)
        {
            _pokemonRepository = pokemonRepository;
            _probabilityUtility = probabilityUtility;
            _pokemonGeneratorConfig = pokemonGeneratorConfig;
            _random = random;
        }

        /// <summary>
        /// Retrieves a list of all possible moves for each pokemon at the given level (whether by TM, HM, breeding or leveling up).
        /// Uses this list of moves to give each pokemon four moves. Uses a TMBank of all possible TM's available in the game to make sure that
        /// no team is unrealistically stacked with many TM's. Once a pokemon uses a TM to learn a move, that TM is consumed and can not be used by any other pokemon
        /// on the team.
        /// </summary>
        /// <param name="list">List of pokemon on the team.</param>
        /// <param name="level">The level of each pokemon.</param>
        public void AssignMovesToTeam(PokeList list, int level)
        {
            var TMBank = _pokemonRepository.GetTMs().ToList();
            for (int i = 0; i < list.Pokemon.Length; i++)
            {
                var poke = list.Pokemon[i];
                var moves = _pokemonRepository.GetMovesForPokemon(poke.SpeciesId, level).ToList();
                list.Pokemon[i] = AssignMovestoPokemon(poke, moves, TMBank);
            }
        }

        /// <summary>
        /// <para> 
        /// By far the most complex method here. 
        /// Uses the parameters to choose one move for a pokemon.
        /// 
        /// info.doSomeDamageFlag indicates whether we want to choose a move that does damage or a move that alters status. 
        /// Based on this flag we can prune the total selection of moves available for this pokemon at this level. 
        /// 
        /// Then assigns a probability value to each move. The higher this value is compared to all of the other moves, the more likely this move is to be chosen.
        /// </para> 
        /// 
        /// Considers the following when assigning probabilities: <para/>
        /// If the damage type of the move matches the pokemon's damage type (physical or special based on the pokemon's base attack and base spAttack values). <para/>
        /// If the move has a certain effect, we can weigh it based on this effect. (e.g. moves that cause the user to faint are wighed very low). See: <see cref="MOVE_EFFECTS_FILTERS"/> <para/>
        /// If we are concerned about damage, we prefer moves that do the most damage. <para/>
        /// If a move relies on another move being chosen in order to be effective, we must look at the previously chosen moves and decide a weight based on prerequisites being filled. See: <see cref="PAIRED_MOVES"/>.<para/> 
        /// If a move has already been chosen, or a very simliar move has been chosen (same effect, same type), then the move will be very unlikely to be picked.  <para/>
        /// If the move type compliments the pokemon's type (either matches the type or is strong against pokemon that the current pokemon is usually weak against.), then the move is preferred. <parar/>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool PickOneMove(PokemonAndMoveInfo info, out int chosenMoveId)
        {
            chosenMoveId = 0;
            var allPossibleMoves = new List<PokemonMoveSetResult>(info.AllPossibleMovesOrig);

            // filter out all non-damaging/damaging attacks
            if (info.DoSomeDamageFlag)
            {
                allPossibleMoves.RemoveAll(m => (m.Power ?? 0) <= 0);
            }
            else
            {
                allPossibleMoves.RemoveAll(m => (m.Power ?? 0) > 0);
            }

            if (allPossibleMoves.Count == 0)
            {
                return false;
            }

            // Calculate probabilities
            var moveProbabilities = allPossibleMoves.Select(m =>
            {
                var moveChoice = new MoveChoice
                {
                    Probability = Likeliness.Full,
                    Move = m
                };

                // Apply weight on damage type
                // if damage type does not mesh with the pokemon, make the likelyhood VERY unlikely
                if ((info.DamageType == DamageType.Special && (m.DamageType ?? "special").Equals("physical")) ||
                   (info.DamageType == DamageType.Physical && (m.DamageType ?? "special").Equals("special")))
                {
                    moveChoice.Probability *= Likeliness.Extremely_Low;
                }

                // Apply weight on effect
                foreach (var key in _pokemonGeneratorConfig.MoveEffectFilters.Keys)
                {
                    if (m.Effect.Contains(key))
                    {
                        moveChoice.Probability *= _pokemonGeneratorConfig.MoveEffectFilters[key];
                    }
                }

                // Apply weight on type
                if ((info.PokeTypes.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)) ||
                   (info.AttackTypesToFavor.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)))
                {
                    moveChoice.Probability *= _pokemonGeneratorConfig.SameTypeModifier;
                }

                // Apply weight on damage
                if (!info.DoSomeDamageFlag)
                {
                    moveChoice.Probability *= Likeliness.Full + (m.Power ?? 0) / _pokemonGeneratorConfig.DamageModifier;
                }

                // Apply special weight for paired moves
                var paired = new List<int>();
                if (info.AlreadyPicked.Any(id => _pokemonGeneratorConfig.PairedMoves.ContainsKey(id) && _pokemonGeneratorConfig.PairedMoves[id].Contains(m.MoveId)))
                {
                    moveChoice.Probability = Likeliness.Full * _pokemonGeneratorConfig.PairedModifier;
                }

                // Filter out dependant moves which do not already have their dependency picked
                if (_pokemonGeneratorConfig.DependantMoves.ContainsKey(m.MoveId) && (_pokemonGeneratorConfig.DependantMoves[m.MoveId].Intersect(info.AlreadyPicked)?.Count() ?? 0) == 0)
                {
                    moveChoice.Probability = Likeliness.None;
                }

                // Finally, apply weight on similar moves to already picked
                if (info.AlreadyPickedEffects.Contains(m.Effect, StringComparer.CurrentCultureIgnoreCase) || info.AlreadyPicked.Contains(m.MoveId))
                {
                    moveChoice.Probability *= Likeliness.Extremely_Low;
                }

                return moveChoice;
            }).ToList();

            // Choose with probabilities
            var chosen = _probabilityUtility.ChooseWithProbability(moveProbabilities.Cast<IChoice>().ToList());
            if (chosen != null)
            {
                chosenMoveId = moveProbabilities[(int)chosen].Move.MoveId;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Chooses 4 moves for a pokemon.
        /// </summary>
        /// <param name="info">The parameters to use when choosing a move.</param>
        /// <returns>An Id for the move that was chosen.</returns>
        /// <exception cref="ArgumentException">If there are not enough moves to choose from.</exception>
        private int ChooseMove(PokemonAndMoveInfo info)
        {
            var chosenMoveId = 0;

            // Try with current damage setting
            if (!PickOneMove(info, out chosenMoveId))
            {
                // Try again with other damage setting
                info.DoSomeDamageFlag = !info.DoSomeDamageFlag;
                if (!PickOneMove(info, out chosenMoveId))
                {
                    // Give up
                    throw new ArgumentException("Not enough moves to choose from!");
                }
            }
            info.AlreadyPicked.Add(chosenMoveId);
            var move1Obj = info.AllPossibleMovesOrig.First(m => m.MoveId == chosenMoveId);
            info.AlreadyPickedEffects.Add(move1Obj.Effect);
            return chosenMoveId;
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
                DamageType = poke.SpAttack > poke.Attack ? DamageType.Special : DamageType.Physical
            };
            info.DamageType = Math.Abs(poke.SpAttack - poke.Attack) < _pokemonGeneratorConfig.DamageTypeDelta ? DamageType.Both : info.DamageType;
            var enemiesWeakAgainst = _pokemonRepository.GetWeaknesses(string.Join(",", info.PokeTypes));
            info.AttackTypesToFavor = _pokemonRepository.GetWeaknesses(string.Join(",", enemiesWeakAgainst)).ToList();

            // Prune moves removing and replacing as needed
            for (int i = 0; i < allPossibleMoves.Count; i++)
            {
                var move = allPossibleMoves[i];

                // Generate random move for sketch
                if (move.MoveId == 166)
                {
                    var randos = _pokemonRepository.GetRandomMoves(_pokemonGeneratorConfig.RandomMoveMinPower, _pokemonGeneratorConfig.RandomMoveMaxPower).ToList();
                    allPossibleMoves[i] = randos[_random.Next(0, randos.Count)];
                }

                // remove duplicates
                var removeMoves = allPossibleMoves
                    .ToList()
                    .FindAll(m => (m.LearnType ?? "#").Equals(move.LearnType ?? "@") && m.MoveId == move.MoveId && allPossibleMoves.IndexOf(m) != i);
                if (removeMoves?.Any() ?? false)
                {
                    removeMoves.ForEach(m =>
                    {
                        if (allPossibleMoves.Count > 4)
                        {
                            allPossibleMoves.Remove(m);
                        }
                    });
                    i--;
                    continue;
                }

                // Remove if unavailable in TMBank
                if ((move.LearnType ?? "").Equals("machine") && !_pokemonGeneratorConfig.HMBank.Contains(move.MoveId) && !TMBank.Contains(move.MoveId))
                {
                    allPossibleMoves.Remove(move);
                }
            }

            // Choose moves
            if (_pokemonGeneratorConfig.SpecialPokemon.Contains(poke.SpeciesId))
            {
                foreach (var m in allPossibleMoves)
                {
                    chosenMoves.Push(m.MoveId);
                }
            }
            else
            {
                info.AllPossibleMovesOrig = new List<PokemonMoveSetResult>(allPossibleMoves);
                info.DoSomeDamageFlag = true;

                for (int i = 0; i < 4; i++)
                {
                    var move = ChooseMove(info);
                    chosenMoves.Push(move);
                    info.DoSomeDamageFlag = false;
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
            poke.Move1Name = allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex1)?.MoveName ?? "";
            poke.Move2Name = allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex2)?.MoveName ?? "";
            poke.Move3Name = allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex3)?.MoveName ?? "";
            poke.Move4Name = allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex4)?.MoveName ?? "";

            // Set pp
            poke.Move1PowerPointsCurrent = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex1)?.Pp ?? 0);
            poke.Move2PowerPointsCurrent = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex2)?.Pp ?? 0);
            poke.Move3PowerPointsCurrent = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex3)?.Pp ?? 0);
            poke.Move4PowerPointsCurrent = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.MoveIndex4)?.Pp ?? 0);

            return poke;
        }
    }
}
