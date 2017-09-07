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
    internal class PokemonGeneratorWorker : IPokemonGeneratorWorker
    {
        private readonly IPokemonDA _pokemonDA;
        private readonly Random r;
        private PokemonGeneratorConfig _config;

        private IList<PokemonChoice> possiblePokemon { get; set; }
        private int PreviousLevel { get; set; }
        public PokemonGeneratorConfig Config { get => _config; set { _config = value; } }

        public PokemonGeneratorWorker(IPokemonDA pokemonDA)
        {
            _pokemonDA = pokemonDA;
            r = new Random();
            _config = new PokemonGeneratorConfig();
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
                poke.MaxHp = (ushort)HP((double)poke.MaxHp, 15d, (double)poke.HitPointsEV, (double)level);
                poke.CurrentHp = poke.MaxHp;
                poke.Attack = (ushort)STAT((double)poke.Attack, (double)poke.AttackIV, (double)poke.AttackEV, (double)level);
                poke.Defense = (ushort)STAT((double)poke.Defense, (double)poke.DefenseIV, (double)poke.DefenseEV, (double)level);
                poke.SpAttack = (ushort)STAT((double)poke.SpAttack, (double)poke.SpecialIV, (double)poke.SpecialEV, (double)level);
                poke.SpDefense = (ushort)STAT((double)poke.SpDefense, (double)poke.SpecialIV, (double)poke.SpecialEV, (double)level);
                poke.Speed = (ushort)STAT((double)poke.Speed, (double)poke.SpeedIV, (double)poke.SpeedEV, (double)level);
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
            var ret = new PokeList(_config.TEAM_SIZE);

            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (PreviousLevel != level || possiblePokemon == null || possiblePokemon.Count < 20)
            {
                PreviousLevel = level;
                possiblePokemon = _pokemonDA.GetPossiblePokemon(level, entropy).Select(id => new PokemonChoice { PokemonId = id }).ToList();
            }

            // add initial probabilities
            foreach (var choice in possiblePokemon)
            {
                if (_config.IGNOREPOKEMON.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.POKEMON_LIKLIHOOD[PokemonClass.Ignored];
                }
                else if (_config.LEGENDARIES.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.POKEMON_LIKLIHOOD[PokemonClass.Legendary];
                }
                else if (_config.SPECIALPOKEMON.Contains(choice.PokemonId))
                {
                    choice.Probability = _config.POKEMON_LIKLIHOOD[PokemonClass.Special];
                }
                else
                {
                    choice.Probability = _config.POKEMON_LIKLIHOOD[PokemonClass.Standard];
                }
            }

            // choose team
            for (int i = 0; i < _config.TEAM_SIZE; i++)
            {
                var ichooseYou = new Pokemon();
                var chosen_one = ChooseWithProbability(possiblePokemon.Cast<IChoice>().ToList());
                if (chosen_one == null)
                {
                    throw new ArgumentException("Not enough Pokemon to choose from.");
                }

                ichooseYou.SpeciesId = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                ret.Species[i] = (byte)possiblePokemon[(int)chosen_one].PokemonId;
                possiblePokemon.RemoveAt((int)chosen_one);
                ichooseYou.Unused = 0x0;
                ichooseYou.OTName = "ROBOT";
                ichooseYou.HeldItem = 0x0;
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
                list.Pokemon[idx].Attack = (byte)s.Attack;
                list.Pokemon[idx].Defense = (byte)s.Defense;
                list.Pokemon[idx].Speed = (byte)s.Speed;
                list.Pokemon[idx].MaxHp = (ushort)s.Hp;
                list.Pokemon[idx].SpDefense = (byte)s.SpDefense;
                list.Pokemon[idx].SpAttack = (byte)s.SpAttack;
                list.Pokemon[idx].Level = (byte)level;
                list.Pokemon[idx].Experience = (uint)EXP(s.GrowthRate, level);

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
                foreach (var key in _config.MoveEffectFilters.Keys)
               {
                   if (m.Effect.Contains(key))
                   {
                       moveChoice.Probability *= _config.MoveEffectFilters[key];
                   }
               }

                // Apply weight on type
                if ((info.PokeTypes.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)) ||
                   (info.AttackTypesToFavor.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase)))
               {
                   moveChoice.Probability *= _config.SameTypeModifier;
               }

                // Apply weight on damage
                if (!info.DoSomeDamageFlag)
               {
                   moveChoice.Probability *= Likeliness.Full + (m.Power ?? 0) / _config.DamageModifier;
               }

                // Apply special weight for paired moves
                var paired = new List<int>();
               if (info.AlreadyPicked.Any(id => _config.PAIRED_MOVES.ContainsKey(id) && _config.PAIRED_MOVES[id].Contains(m.MoveId)))
               {
                   moveChoice.Probability = Likeliness.Full * _config.PairedModifier;
               }

                // Filter out dependant moves which do not already have their dependancy picked
                if (_config.DEPENDANT_MOVES.ContainsKey(m.MoveId) && (_config.DEPENDANT_MOVES[m.MoveId].Intersect(info.AlreadyPicked)?.Count() ?? 0) == 0)
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
                DamageType = poke.SpAttack > poke.Attack ? DamageType.Special : DamageType.Physical
            };
            info.DamageType = Math.Abs(poke.SpAttack - poke.Attack) < _config.DamageTypeDelta ? DamageType.Both : info.DamageType;
            var enemiesWeakAgainst = _pokemonDA.GetWeaknesses(string.Join(",", info.PokeTypes));
            info.AttackTypesToFavor = _pokemonDA.GetWeaknesses(string.Join(",", enemiesWeakAgainst)).ToList();

            // Prune moves removing and replacing as needed
            for (int i = 0; i < allPossibleMoves.Count; i++)
            {
                var move = allPossibleMoves[i];

                // Generate random move for sketch
                if (move.MoveId == 166)
                {
                    var randos = _pokemonDA.GetRandomMoves(_config.RandomMoveMinPower, _config.RandomMoveMaxPower).ToList();
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
                if ((move.LearnType ?? "").Equals("machine") && !_config.HM_BANK.Contains(move.MoveId) && !TMBank.Contains(move.MoveId))
                {
                    allPossibleMoves.Remove(move);
                }
            }

            // Choose moves
            if (_config.SPECIALPOKEMON.Contains(poke.SpeciesId))
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
                var moves = _pokemonDA.GetMovesForPokemon(poke.SpeciesId, level).ToList();
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
                poke.AttackEV = (ushort)GaussianRandom(0, 65535);
                poke.DefenseEV = (ushort)GaussianRandom(0, 65535);
                poke.HitPointsEV = (ushort)GaussianRandom(0, 65535);
                poke.SpecialEV = (ushort)GaussianRandom(0, 65535);
                poke.SpeedEV = (ushort)GaussianRandom(0, 65535);

                // IVs between 0-15
                poke.AttackIV = (byte)GaussianRandom(0, 15);
                poke.DefenseIV = (byte)GaussianRandom(0, 15);
                poke.SpecialIV = (byte)GaussianRandom(0, 15);
                poke.SpeedIV = (byte)GaussianRandom(0, 15);
            }
        }


        /// <summary>
        /// Use Box-Muller transform to simulate a gaussian distribution with a mean of <see cref="PokemonGeneratorConfig.StandardDeviation"/> and a standard deviation of <see cref="PokemonGeneratorConfig.StandardDeviation"/>.
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
                         _config.Mean + _config.StandardDeviation * randStdNormal; //random normal(mean,stdDev^2)

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