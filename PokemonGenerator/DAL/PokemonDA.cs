using PokemonGenerator.DAL.Serialization;
using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator.DAL
{
    /// <summary>
    /// Accesses the database to return information about pokemon. 
    /// <para/> 
    /// Data base is a modified version of of the project found here: https://github.com/veekun/pokedex
    /// </summary>
    internal class PokemonDA : IPokemonDA
    {
        /// <summary>
        /// Gets a list of all pokemon at the given level, eliminating pokemon that would have already evolved at this level, as well as pokemon that haven't evelolved at this level.
        /// </summary>
        public List<int> GetPossiblePokemon(int level, Entropy entopy)
        {
            var list = new List<int>();
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = ctx.tbl_vwEvolutions.SqlQuery(Queries.Queries.GetPossiblePokemon, level).ToList();
                results.ForEach(s => list.Add(s.id));
            }
            return list;
        }

        /// <summary>
        /// Gets a list of all moves available to the pokemon at the given level, eliminating moves that would only be avaialable at later levels.
        /// </summary>
        public List<uspGetPokemonMoveSetResult> GetMovesForPokemon(int id, int level)
        {
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = ctx.Database.SqlQuery<uspGetPokemonMoveSetResult>(Queries.Queries.GetPokemonMoveSet, id, level).ToList();


                var ret = results.ToList();
                return ret;
            }
        }

        /// <summary>
        /// Gets the base stats for the team of pokemon.
        /// </summary>
        public List<tbl_vwBaseStats> GetTeamBaseStats(PokeList list)
        {
            using (var ctx = new ThePokeBase())
            {
                var iList = list.Species.ToList();
                var query = from mon in ctx.tbl_vwBaseStats
                            where iList.Contains((byte)mon.id)
                            select mon;
                return query.ToList();
            }
        }

        /// <summary>
        /// Gets a random move. Completely random, but with a min and max power.
        /// </summary>
        public List<uspGetPokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower)
        {
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = ctx.Database.SqlQuery<uspGetPokemonMoveSetResult>("SELECT level, moveId, moveName, identifier AS Type, power, pp, damageType, effect FROM tbl_vwPokemonMoves INNER JOIN tbl_vwGenIIMoves moves ON moves.[moveId] = move_id WHERE [power] >= @p0 AND [power] <= @p1 ORDER BY level, moveId", minPower, maxPower);

                return results.ToList();
            }
        }

        /// <summary>
        /// Gets the types that are strong against the given type.
        /// </summary>
        public List<string> GetWeaknesses(string v)
        {
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = ctx.Database.SqlQuery<string>(Queries.Queries.GetWeaknesses, v).ToList();
                return results.ToList();
            }
        }

        /// <summary>
        /// Gets all TMs.
        /// </summary>
        public List<int> GetTMs()
        {
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = from tm in ctx.tbl_vwTMs
                              select tm.move_id;
                return results.ToList();
            }
        }
    }
}