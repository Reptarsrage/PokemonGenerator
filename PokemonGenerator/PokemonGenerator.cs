using PokemonGenerator.DAL;
using PokemonGenerator.DAL.Serialization;
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
    internal class PokemonGenerator : IPokemonGenerator
    {
        readonly int[] LEGENDARIES = { 144, 145, 146, 151, 150, 243, 244, 245, 249, 250, 251 };
        readonly int[] IGNOREPOKEMON = { 10, 11, 13, 14, 129, 201 }; /*caterpie, metapod, weedle, kakuna, magikarp, unown */
        readonly int[] SPECIALPOKEMON = { 10, 11, 13, 14, 129, 132, 201, 202 }; /* Specially treated for move selection bc they can learn < 4 moves total
                                                                                 * caterpie, metapod, weedle, kakuna, magikarp, ditto, unown, wobbuffet */
        private readonly Dictionary<int, int[]> pairedMoves = new Dictionary<int, int[]>()
        {
            { 156, new int[] { 214, 173 } }, /* Rest = sleep-talk, snore */
            { 47, new int[] { 138, 171 } }, /* sing = dream-eater, nightmare */
            { 79, new int[] { 138, 171 } }, /* sleep-powder = dream-eater, nightmare */
            { 95, new int[] { 138, 171 } }, /* hypnosis = dream-eater, nightmare */
            { 142, new int[] { 138, 171 } }, /* lovely-kiss = dream-eater, nightmare */
            { 147, new int[] { 138, 171 } }, /* spore = dream-eater, nightmare */
        };

        private readonly Dictionary<int, int[]> dependantMoves = new Dictionary<int, int[]>()
        {
            { 214, new int[] { 156 } }, /* Rest = sleep-talk, snore */
            { 173, new int[] { 156 } }, /* hypnosis = dream-eater, nightmare */
            { 138, new int[] { 47, 79, 95, 142, 147 } }, /* sing = dream-eater, nightmare */
            { 171, new int[] { 47, 79, 95, 142, 147 } }, /* sleep-powder = dream-eater, nightmare */ 
        };

        private readonly List<int> HMBank = new List<int>()
        {
            15, /* cut */
            19, /* fly */
            70, /* strength */
            127, /* waterfall */
            148, /* flash */
            250, /* whirlpool */
            57 /* surf */
        };

        private readonly Dictionary<string, double> moveEffectsFilters = new Dictionary<string, double>()
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

        const double MEAN = 0.5d;
        const double STDEVIATION = 0.5d;

        private IPokemonDA dal;
        private int level;
        private List<int> possiblePokemon;
        private Random r;

        public PokemonGenerator()
        {
            dal = new PokemonDA();
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
            // Make sure both players end up with different pokemons, re-use the same list if possible
            if (this.level != level || this.possiblePokemon == null || this.possiblePokemon.Count < 20)
            {
                this.level = level;
                this.possiblePokemon = dal.GetPossiblePokemon(level, entropy);
            }

            var ret = new PokeList(6);

            // add initial probabilities
            var elements = new List<double>();
            this.possiblePokemon.ForEach(p =>
            {
                var prob = Likeliness.Full;

                if (IGNOREPOKEMON.Contains(p))
                {
                    prob = Likeliness.None;
                }
                else if (LEGENDARIES.Contains(p))
                {
                    prob = Likeliness.Very_Low;


                }
                else if (SPECIALPOKEMON.Contains(p))
                {
                    prob = Likeliness.Medium_Low;
                }
                elements.Add(prob);

            });

            // choose team
            for (int i = 0; i < 6; i++)
            {
                Pokemon ichooseYou = new Pokemon();
                var chosen_one = chooseWithProbability(elements);
                if (chosen_one == null)
                {
                    throw new ArgumentException("Not enough Pokemon to choose from.");
                }

                ichooseYou.species = (byte)this.possiblePokemon[(int)chosen_one];
                ret.Species[i] = (byte)this.possiblePokemon[(int)chosen_one];
                this.possiblePokemon.RemoveAt((int)chosen_one);
                elements.RemoveAt((int)chosen_one);
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
            var stats = dal.GetTeamBaseStats(list);
            foreach (var s in stats)
            {
                var idx = list.Species.ToList().IndexOf((byte)s.id);

                // Set Name
                list.Names[idx] = s.identifier.ToUpper();

                // Set Stats
                list.Pokemon[idx].attack = (byte)s.attack;
                list.Pokemon[idx].defense = (byte)s.defense;
                list.Pokemon[idx].speed = (byte)s.speed;
                list.Pokemon[idx].maxHp = (ushort)s.hp;
                list.Pokemon[idx].spDefense = (byte)s.spDefense;
                list.Pokemon[idx].spAttack = (byte)s.spAttack;
                list.Pokemon[idx].level = (byte)level;
                list.Pokemon[idx].experience = (uint)EXP(s.growthRate, level);

                // Set Others
                list.Pokemon[idx].Name = s.identifier.ToUpper();
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
        /// If the move has a certain effect, we can weigh it based on this effect. (e.g. moves that cause the user to faint are wighed very low). See: <see cref="moveEffectsFilters"/> <para/>
        /// If we are concerned about damage, we prefer moves that do the most damage. <para/>
        /// If a move relies on another move being chosen in order to be effective, we must look at the previously chosen moves and decide a weight based on prerequisites being filled. See: <see cref="pairedMoves"/>.<para/> 
        /// If a move has already been chosen, or a very simliar move has been chosen (same effect, same type), then the move will be very unlikely to be picked.  <para/>
        /// If the move type compliments the pokemon's type (either matches the type or is strong against pokemon that the current pokemon is usually weak against.), then the move is preferred. <parar/>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private int? PickOneMove(PokemonAndMoveInfo info)
        {
            var allPossibleMoves = new List<uspGetPokemonMoveSetResult>(info.AllPossibleMovesOrig);

            // filter out all non-damaging/damaging attacks
            if (info.DoSomeDamageFlag)
            {
                allPossibleMoves.RemoveAll(m => (m.power ?? 0) > 0);
            }
            else
            {
                allPossibleMoves.RemoveAll(m => (m.power ?? 0) <= 0);
            }

            if (allPossibleMoves.Count == 0)
            {
                return null;
            }

            Dictionary<uspGetPokemonMoveSetResult, double> moveProbabilities = new Dictionary<uspGetPokemonMoveSetResult, double>();
            allPossibleMoves.ForEach(m =>
            {
                var prob = Likeliness.Full;

                // Apply weight on damage type
                // if damage type does not mesh with the pokemon, make the likelyhood VERY unlikely
                if (info.DamageType == DamageType.Special && (m.damageType ?? "special").Equals("physical"))
                {
                    prob *= Likeliness.Extremely_Low;
                }
                else if (info.DamageType == DamageType.Physical && (m.damageType ?? "special").Equals("special"))
                {
                    prob *= Likeliness.Extremely_Low;
                }

                // Apply weight on effect
                foreach (var key in this.moveEffectsFilters.Keys)
                {
                    if (m.effect.Contains(key))
                    {
                        prob *= this.moveEffectsFilters[key];
                    }
                }

                // Apply weight on type
                if (info.PokeTypes.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase))
                {
                    prob *= 1.5;
                }
                if (info.AttackTypesToFavor.Contains(m.Type, StringComparer.CurrentCultureIgnoreCase))
                {
                    prob *= 1.5;
                }

                if (!info.DoSomeDamageFlag)
                {
                    // Apply weight on powers
                    prob *= (1.00 + ((int)m.power / 200.00));
                }

                // Apply special weight for paired moves
                var paired = new List<int>();
                info.AlreadyPicked.ForEach(id =>
                {
                    if (this.pairedMoves.ContainsKey(id) && this.pairedMoves[id].Contains(m.moveId))
                    {
                        prob = Likeliness.Full * 2.0;
                    }
                });

                if (dependantMoves.ContainsKey(m.moveId) && (dependantMoves[m.moveId].Intersect(info.AlreadyPicked)?.Count() ?? 0) == 0)
                {
                    prob = Likeliness.None; // do not meet pre-reqs for this move!
                }

                // Finally, apply weight on similar moves to already picked
                if (info.AlreadyPickedEffects.Contains(m.effect, StringComparer.CurrentCultureIgnoreCase))
                {
                    prob *= Likeliness.Extremely_Low;
                }
                if (info.AlreadyPicked.Contains(m.moveId))
                {
                    prob *= Likeliness.Extremely_Low;
                }

                moveProbabilities.Add(m, prob);
            });

            var chosen = chooseWithProbability(moveProbabilities.Values.ToList());

            if (chosen != null)
            {
                return moveProbabilities.Keys.ToList()[(int)chosen].moveId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Chooses 4 moves for a pokemon.
        /// </summary>
        /// <param name="info">The parameters to use when choosing a move.</param>
        /// <returns>An Id for the move that was chosen.</returns>
        /// <exception cref="ArgumentException">If there are not enough moves to choose from.</exception>
        private int ChooseMove(PokemonAndMoveInfo info)
        {
            int? chosenMoveId = null;

            if ((chosenMoveId = PickOneMove(info)) == null)
            {
                info.DoSomeDamageFlag = !info.DoSomeDamageFlag;
                if ((chosenMoveId = PickOneMove(info)) == null)
                {
                    throw new ArgumentException("Not enough moves to choose from!");
                }
            }

            info.AlreadyPicked.Add((int)chosenMoveId);
            var move1Obj = info.AllPossibleMovesOrig.First(m => m.moveId == (int)chosenMoveId);
            info.AlreadyPickedEffects.Add(move1Obj.effect);
            return (int)chosenMoveId;
        }

        /// <summary>
        /// Fills in all the params needed to calculate 4 moves for a pokmeon. Treats special pokemon uniquely <see cref="SPECIALPOKEMON"/>.
        /// </summary>
        /// <param name="poke">Pokemon to choose moves for</param>
        /// <param name="allPossibleMoves">A list of all possible moves available for the pokemon to learn</param>
        /// <param name="TMBank">The curreent bank of TM's available</param>
        /// <returns>A pokemon fulley equipped with moves.</returns>
        private Pokemon AssignMovestoPokemon(Pokemon poke, List<uspGetPokemonMoveSetResult> allPossibleMoves, ref List<int> TMBank)
        {
            var chosenMoves = new Stack<int>();
            var info = new PokemonAndMoveInfo();

            info.Pokemon = poke;
            info.PokeTypes = poke.Types.ToList();
            info.DamageType = poke.spAttack > poke.attack ? DamageType.Special : DamageType.Physical;
            info.DamageType = Math.Abs(poke.spAttack - poke.attack) < 15 ? DamageType.Both : info.DamageType;
            var enemiesWeakAgainst = dal.GetWeaknesses(string.Join(",", info.PokeTypes));
            info.AttackTypesToFavor = dal.GetWeaknesses(string.Join(",", enemiesWeakAgainst));

            // Prune moves removing and replacing as needed
            for (int i = 0; i < allPossibleMoves.Count; i++)
            {
                var move = allPossibleMoves[i];
                // Generate random move for sketch
                if (move.moveId == 166)
                {
                    var randos = dal.GetRandomMoves(40, 100);
                    allPossibleMoves[i] = randos[r.Next(0, randos.Count)];
                }

                // remove duplicates
                List<uspGetPokemonMoveSetResult> removeMoves = null;
                if ((removeMoves = allPossibleMoves.FindAll(m => (m.learnType ?? "#").Equals(move.learnType ?? "@") && m.moveId == move.moveId && allPossibleMoves.IndexOf(m) != i)) != null && removeMoves.Count > 0)
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
                if ((move.learnType ?? "").Equals("machine") && !HMBank.Contains(move.moveId) && !TMBank.Contains(move.moveId))
                {
                    allPossibleMoves.Remove(move);
                }
            }

            // Choose moves
            if (SPECIALPOKEMON.Contains(poke.species))
            {
                allPossibleMoves.ForEach(m => chosenMoves.Push(m.moveId));
            }
            else
            {
                info.AllPossibleMovesOrig = new List<uspGetPokemonMoveSetResult>(allPossibleMoves);
                info.DoSomeDamageFlag = true;

                for (int i = 0; i < 4; i++)
                {
                    var move = this.ChooseMove(info);
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
            poke.MoveName1 = allPossibleMoves.Find(m => m.moveId == poke.moveIndex1)?.moveName ?? "";
            poke.MoveName2 = allPossibleMoves.Find(m => m.moveId == poke.moveIndex2)?.moveName ?? "";
            poke.MoveName3 = allPossibleMoves.Find(m => m.moveId == poke.moveIndex3)?.moveName ?? "";
            poke.MoveName4 = allPossibleMoves.Find(m => m.moveId == poke.moveIndex4)?.moveName ?? "";

            // Set pp
            poke.currentPP1 = (byte)(allPossibleMoves.Find(m => m.moveId == poke.moveIndex1)?.pp ?? 0);
            poke.currentPP2 = (byte)(allPossibleMoves.Find(m => m.moveId == poke.moveIndex2)?.pp ?? 0);
            poke.currentPP3 = (byte)(allPossibleMoves.Find(m => m.moveId == poke.moveIndex3)?.pp ?? 0);
            poke.currentPP4 = (byte)(allPossibleMoves.Find(m => m.moveId == poke.moveIndex4)?.pp ?? 0);

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
            var TMBank = dal.GetTMs();
            for (int i = 0; i < list.Pokemon.Length; i++)
            {
                var poke = list.Pokemon[i];
                var moves = dal.GetMovesForPokemon(poke.species, level);
                list.Pokemon[i] = this.AssignMovestoPokemon(poke, moves, ref TMBank);
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
        /// <param name="elements">A list of relative probabilities.</param>
        /// <returns>The index of the chosen element, null on failure.</returns>
        private int? chooseWithProbability(List<double> elements)
        {
            // normalize probabilities
            var norm = 1.0d / elements.Sum();
            elements = elements.Select(k => k * norm).ToList();
            double runningSum = 0.0;
            for (int i = 0; i < elements.Count; i++)
            {
                runningSum += elements[i];
                elements[i] = runningSum;
            }

            // choose from list with probabilities
            double diceRoll = this.r.NextDouble();

            for (int i = 0; i < elements.Count; i++)
            {
                if (diceRoll < elements[i])
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