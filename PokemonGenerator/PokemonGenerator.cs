using PokemonGenerator.DAL;
using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator
{
    /// <summary>
    /// The real  MVP of this application. Contains all the logic for generating a team of pokemon.
    /// </summary>
    internal partial class PokemonGenerator : IPokemonGenerator
    {
        private readonly IPokemonDA _pokemonDA;
        private readonly Random r;

        private IList<PokemonChoice> possiblePokemon { get; set; }
        private int PreviousLevel { get; set; }

        public PokemonGenerator(IPokemonDA pokemonDA)
        {
            _pokemonDA = pokemonDA;
            r = new Random();
        }

        /// <summary>
        /// Smartly generates a team of six random (ish) pokemon.
        /// 
        /// </summary>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        /// <param name="entropy">The level of randomness to use when generating pokemon. (Not yet implemented)</param>
        /// <returns><see cref="PokeList"/> containing the generated team.</returns>
        public PokeList GenerateRandomPokemon(int level, Entropy entropy)
        {
            if (entropy != Entropy.Low)
            {
                throw new NotImplementedException("Entropy value must be set to low.");
            }

            if (level < 5 || level > 100)
            {
                throw new ArgumentOutOfRangeException($"level ({level}) must be between 5 and 100 inclusive.");
            }

            // Choose Pokemon Team
            var ret = ChooseTeam(level, entropy);

            // Choose base stats
            ChooseTeamStats(ref ret, level);

            // Choose IV's and EV's
            AssignIVsAndEVsToTeam(ref ret);

            // Calculate final stats using formulae
            foreach (var poke in ret.Pokemon)
            {
                poke.maxHp = (ushort)HP((double)poke.maxHp, 15d, (double)poke.hpEV, (double)level);
                poke.currentHp = poke.maxHp;
                poke.attack = (ushort)STAT((double)poke.attack, (double)poke.attackIV, (double)poke.attackEV, (double)level);
                poke.defense = (ushort)STAT((double)poke.defense, (double)poke.defenseIV, (double)poke.defenseEV, (double)level);
                poke.spAttack = (ushort)STAT((double)poke.spAttack, (double)poke.specialIV, (double)poke.specialEV, (double)level);
                poke.spDefense = (ushort)STAT((double)poke.spDefense, (double)poke.specialIV, (double)poke.specialEV, (double)level);
                poke.speed = (ushort)STAT((double)poke.speed, (double)poke.speedIV, (double)poke.speedEV, (double)level);
            }

            // Assign moves
            AssignMovesToTeam(ref ret, level);

            // Done!
            return ret;
        }

        /// <summary>
        /// Chooses six rendom (ish) empty <see cref="Pokemon"/> with only their species (ID)..
        /// Note:
        /// <para />
        /// Will only choose pokemon at a certain level, eliminating pokemon that would have eveolved before the level, or will be eveolved into after the level. <para />
        /// Will never choose pokemon contained in <see cref="IGNOREPOKEMON"/>.<para />
        /// Will very rarely choose pokemon contained in <see cref="LEGENDARIES"/>.<para />
        /// Has a low chance of choose pokemon contained in <see cref="SPECIALPOKEMON"/>.<para />
        /// </summary>
        /// <param name="level">The level of generated pokemon. Must be between 5 and 100 inclusive.</param>
        /// <param name="entropy">The level of randomness to use when generating pokemon. (Not yet implemented)</param>
        /// <returns><see cref="PokeList"/> containing the generated team.</returns>
        private PokeList ChooseTeam(int level, Entropy entropy)
        {
            var ret = new PokeList(TEAM_SIZE);

            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (PreviousLevel != level || possiblePokemon == null || possiblePokemon.Count < 20)
            {
                PreviousLevel = level;
                possiblePokemon = _pokemonDA.GetPossiblePokemon(level, entropy).Select(id => new PokemonChoice { PokemonId = id }).ToList();
            }

            // add initial probabilities
            foreach (var choice in possiblePokemon)
            {
                if (IGNOREPOKEMON.Contains(choice.PokemonId))
                {
                    choice.Probability = POKEMON_LIKLIHOOD[PokemonClass.Ignored];
                }
                else if (LEGENDARIES.Contains(choice.PokemonId))
                {
                    choice.Probability = POKEMON_LIKLIHOOD[PokemonClass.Legendary];
                }
                else if (SPECIALPOKEMON.Contains(choice.PokemonId))
                {
                    choice.Probability = POKEMON_LIKLIHOOD[PokemonClass.Special];
                } else
                {
                    choice.Probability = POKEMON_LIKLIHOOD[PokemonClass.Standard];
                }
            }

            // choose team
            for (int i = 0; i < TEAM_SIZE; i++)
            {
                var ichooseYou = new Pokemon();
                var chosen_one = ChooseWithProbability(possiblePokemon.Cast<IChoice>().ToList());
                if (chosen_one == null)
                {
                    throw new ArgumentException("Not enough Pokemon to choose from.");
                }

                ichooseYou.species = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                ret.Species[i] = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                possiblePokemon.RemoveAt((int)chosen_one);
                ichooseYou.unused = 0x0;
                ichooseYou.OTName = "ROBOT";
                ichooseYou.heldItem = 0x0;
                ret.Pokemon[i] = ichooseYou;
            }

            return ret;
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
        /// types  The Pokemon's type(s) (used for moce selection).       <para /> 
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="level"></param>
        private void ChooseTeamStats(ref PokeList list, int level)
        {
            var stats = _pokemonDA.GetTeamBaseStats(list);
            foreach (var s in stats)
            {
                var idx = list.Species.ToList().IndexOf((byte)s.Id);

                // Set Name
                list.Names[idx] = s.Identifier.ToUpper();

                // Set Stats
                list.Pokemon[idx].attack = (byte)s.Attack;
                list.Pokemon[idx].defense = (byte)s.Defense;
                list.Pokemon[idx].speed = (byte)s.Speed;
                list.Pokemon[idx].maxHp = (ushort)s.Hp;
                list.Pokemon[idx].spDefense = (byte)s.SpDefense;
                list.Pokemon[idx].spAttack = (byte)s.SpAttack;
                list.Pokemon[idx].level = (byte)level;
                list.Pokemon[idx].experience = (uint)EXP(s.GrowthRate, level);

                // Set Others
                list.Pokemon[idx].Name = s.Identifier.ToUpper();
                list.Pokemon[idx].Types = s.Types.Split(new char[] { ',' }).ToList();
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
            var moveProbabilities =  allPossibleMoves.Select(m =>
            {
                var moveChoice = new MoveChoice {
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
                foreach (var key in MOVE_EFFECTS_FILTERS.Keys)
                {
                    if (m.Effect.Contains(key))
                    {
                        moveChoice.Probability *= MOVE_EFFECTS_FILTERS[key];
                    }
                }

                // Apply weight on type
                if ((info.PokeTypes.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)) ||
                    (info.AttackTypesToFavor.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)))
                {
                    moveChoice.Probability *= SAME_TYPE_MODIFIER;
                }

                // Apply weight on damage
                if (!info.DoSomeDamageFlag)
                {
                    moveChoice.Probability *= Likeliness.Full + (m.Power ?? 0) / DAMAGE_MODIFIER;
                }

                // Apply special weight for paired moves
                var paired = new List<int>();
                if (info.AlreadyPicked.Any(id => PAIRED_MOVES.ContainsKey(id) && PAIRED_MOVES[id].Contains(m.MoveId)))
                {
                    moveChoice.Probability = Likeliness.Full * PAIRED_MODIFIER;
                }

                // Filter out dependant moves which do not already have their dependancy picked
                if (DEPENDANT_MOVES.ContainsKey(m.MoveId) && (DEPENDANT_MOVES[m.MoveId].Intersect(info.AlreadyPicked)?.Count() ?? 0) == 0)
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
            var chosen = ChooseWithProbability(moveProbabilities.Cast<IChoice>().ToList());
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
                DamageType = poke.spAttack > poke.attack ? DamageType.Special : DamageType.Physical
            };
            info.DamageType = Math.Abs(poke.spAttack - poke.attack) < DAMAGE_TYPE_DELTA ? DamageType.Both : info.DamageType;
            var enemiesWeakAgainst = _pokemonDA.GetWeaknesses(string.Join(",", info.PokeTypes));
            info.AttackTypesToFavor = _pokemonDA.GetWeaknesses(string.Join(",", enemiesWeakAgainst)).ToList();

            // Prune moves removing and replacing as needed
            for (int i = 0; i < allPossibleMoves.Count; i++)
            {
                var move = allPossibleMoves[i];

                // Generate random move for sketch
                if (move.MoveId == 166)
                {
                    var randos = _pokemonDA.GetRandomMoves(RANDOM_MOVE_MIN_POWER, RANDOM_MOVE_MAX_POWER).ToList();
                    allPossibleMoves[i] = randos[r.Next(0, randos.Count)];
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
                if ((move.LearnType ?? "").Equals("machine") && !HM_BANK.Contains(move.MoveId) && !TMBank.Contains(move.MoveId))
                {
                    allPossibleMoves.Remove(move);
                }
            }

            // Choose moves
            if (SPECIALPOKEMON.Contains(poke.species))
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
            poke.ppUps1 = 0;
            poke.ppUps2 = 0;
            poke.ppUps3 = 0;
            poke.ppUps4 = 0;

            // Set moves
            poke.moveIndex1 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.moveIndex2 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.moveIndex3 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);
            poke.moveIndex4 = (byte)(chosenMoves.Count > 0 ? chosenMoves.Pop() : 0);

            // Set move names
            poke.MoveName1 = allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex1)?.MoveName ?? "";
            poke.MoveName2 = allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex2)?.MoveName ?? "";
            poke.MoveName3 = allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex3)?.MoveName ?? "";
            poke.MoveName4 = allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex4)?.MoveName ?? "";

            // Set pp
            poke.currentPP1 = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex1)?.Pp ?? 0);
            poke.currentPP2 = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex2)?.Pp ?? 0);
            poke.currentPP3 = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex3)?.Pp ?? 0);
            poke.currentPP4 = (byte)(allPossibleMoves.ToList().Find(m => m.MoveId == poke.moveIndex4)?.Pp ?? 0);

            return poke;
        }

        /// <summary>
        /// Retrieves a list of all possible moves for each pokemon at the given level (whether by TM, HM, breeding or leveling up).
        /// Uses this list of moves to give each pokemon four moves. Uses a TMBank of all possible TM's available in the game to make sure that
        /// no team is unrealistically stacked with many TM's. Once a pokemon uses a TM to learn a move, that TM is consumed and can not be used by any other pokemon
        /// on the team.
        /// </summary>
        /// <param name="list">List of pokemon on the team.</param>
        /// <param name="level">The level of each pokemon.</param>
        private void AssignMovesToTeam(ref PokeList list, int level)
        {
            var TMBank = _pokemonDA.GetTMs().ToList();
            for (int i = 0; i < list.Pokemon.Length; i++)
            {
                var poke = list.Pokemon[i];
                var moves = _pokemonDA.GetMovesForPokemon(poke.species, level).ToList();
                list.Pokemon[i] = AssignMovestoPokemon(poke, moves, TMBank);
            }
        }

        /// <summary>
        /// Chooses both IV and EV values for each pokemon. 
        /// Uses a gaussian distribution with a mean in the middle and a std deviation of around 30%.
        /// </summary>
        /// <param name="list">List of pokemon on the team.</param>
        private void AssignIVsAndEVsToTeam(ref PokeList list)
        {
            foreach (var poke in list.Pokemon)
            {
                // EVs between 0-65535
                poke.attackEV = (ushort)GaussianRandom(0, 65535);
                poke.defenseEV = (ushort)GaussianRandom(0, 65535);
                poke.hpEV = (ushort)GaussianRandom(0, 65535);
                poke.specialEV = (ushort)GaussianRandom(0, 65535);
                poke.speedEV = (ushort)GaussianRandom(0, 65535);

                // IVs between 0-15
                poke.attackIV = (byte)GaussianRandom(0, 15);
                poke.defenseIV = (byte)GaussianRandom(0, 15);
                poke.specialIV = (byte)GaussianRandom(0, 15);
                poke.speedIV = (byte)GaussianRandom(0, 15);
            }
        }


        /// <summary>
        /// Use Box-Muller transform to simulate a gaussian distribution with a mean of <see cref="STDEVIATION"/> and a standard deviation of <see cref="STDEVIATION"/>.
        /// </summary>
        /// <param name="low">The low bound</param>
        /// <param name="high">The High bound.</param>
        /// <returns></returns>
        private int GaussianRandom(int low, int high)
        {
            double u1 = r.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = r.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         MEAN + STDEVIATION * randStdNormal; //random normal(mean,stdDev^2)

            int range = high - low;
            return low + (int)Math.Round(range * randNormal);
        }

        /// <summary>
        /// Chooses from a list of elements where each element is the relative probability of itself being chosen (Relative to all other elements). This may fail if there are elements with negative probabilties or if the list is empty.
        /// </summary>
        /// <param name="choices">A list of relative probabilities.</param>
        /// <returns>The index of the chosen element, null on failure.</returns>
        private int? ChooseWithProbability(IList<IChoice> choices)
        {
            // Normalize probabilities
            var norm = 1D / choices.Select(pc => pc.Probability).Sum();
            foreach (var choice in choices)
            {
                choice.Probability *= norm;
            }

            // Makes sure they all add up to 100 after being normalized
            var runningSum = 0D;
            for (int i = 0; i < choices.Count; i++)
            {
                runningSum += choices[i].Probability;
                choices[i].Probability = runningSum;
            }

            // Choose from list with probabilities
            var diceRoll = r.NextDouble();
            for (int i = 0; i < choices.Count; i++)
            {
                if (diceRoll < choices[i].Probability)
                {
                    return i;
                }
            }
            return null;
        }

        /// <summary>
        /// Calculates the max hp for a pokemon based on it's base stat value, IV and EV values using a standard formula for Generations I and II.
        /// <para />
        /// http://bulbapedia.bulbagarden.net/wiki/Statistic
        /// </summary>
        /// <param name="base">Base HP Stat</param>
        /// <param name="iv">IV (0-15)</param>
        /// <param name="ev">EV (0-65535)</param>
        /// <param name="lvl">level of pokemon (5-100)</param>
        /// <returns>The max HP for the pokemon</returns>
        private double HP(double @base, double iv, double ev, double lvl)
        {
            return Math.Floor(((@base + iv) * 2.0 + Math.Floor(Math.Ceiling(Math.Sqrt(ev)) / 4.0)) * lvl / 100.0) + lvl + 10;
        }

        /// <summary>
        /// Calculates the stat for a pokemon based on it's base stat value, IV and EV values using a standard formula for Generations I and II.
        /// <para />
        /// http://bulbapedia.bulbagarden.net/wiki/Statistic
        /// </summary>
        /// <param name="base">Base HP Stat</param>
        /// <param name="iv">IV (0-15)</param>
        /// <param name="ev">EV (0-65535)</param>
        /// <param name="lvl">level of pokemon (5-100)</param>
        /// <returns>The calculated stat for the pokemon</returns>
        private double STAT(double @base, double iv, double ev, double lvl)
        {
            return Math.Floor(((@base + iv) * 2.0 + Math.Floor(Math.Ceiling(Math.Sqrt(ev)) / 4.0)) * lvl / 100.0) + 5;
        }

        /// <summary>
        /// Calcualtes the experience points a pokemon would need to attain the given level.
        /// <para/> 
        /// http://bulbapedia.bulbagarden.net/wiki/Experience
        /// </summary>
        /// <param name="group">The experience group  of the pokemon (See: http://bulbapedia.bulbagarden.net/wiki/Experience )</param>
        /// <param name="n">The level of the pokemon</param>
        /// <returns></returns>
        private double EXP(string group, int n)
        {
            switch (group)
            {
                case "medium-slow":
                    return (6d / 5d) * Math.Pow(n, 3) - 15d * Math.Pow(n, 2) + 100d * n - 140d;
                case "medium":
                    return Math.Pow(n, 3);
                case "fast":
                    return 4d * Math.Pow(n, 3) / 5d;
                case "slow":
                    return 5d * Math.Pow(n, 3) / 4d;
                case "medium-fast":
                    return Math.Pow(n, 3);
                default:
                    return Math.Pow(n, 3);
            }
        }
    }
}