/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>

namespace PokemonGenerator.DAL
{
    using Modals;
    using System.Collections.Generic;
    using System.Linq;
    using Serialization;

    /// <summary>
    /// Accesses the database to return information about pokemon. 
    /// <para/> 
    /// Data base is a modified version of of the project found here: https://github.com/veekun/pokedex
    /// </summary>
    class SQLManager
    {
        #region Stored Procedure Constants
        const string uspGetPossiblePokemon = @"SELECT DISTINCT D.* FROM [tbl_vwEvolutions] D WHERE D.Id NOT IN ( SELECT DISTINCT B.[Id] FROM [tbl_vwEvolutions] A INNER JOIN [tbl_vwEvolutions] B ON A.evolvedFromPrevID = B.Id WHERE COALESCE(A.minimum_level, 0) <= @p0 AND B.Id IS NOT NULL UNION SELECT DISTINCT C.[Id] FROM [tbl_vwEvolutions] A INNER JOIN [tbl_vwEvolutions] B ON A.evolvedFromPrevID = B.Id INNER JOIN [tbl_vwEvolutions] C ON B.evolvedFromPrevID = C.Id WHERE COALESCE(A.minimum_level, 0) <= @p0 AND C.Id IS NOT NULL ) AND COALESCE(D.minimum_level, 0) < @p0";

        const string uspGetPokemonMoveSet = @"SELECT level, moveId, moveName, identifier AS Type, power, pp, damageType, effect, method AS learnType FROM tbl_vwPokemonMoves INNER JOIN tbl_vwGenIIMoves moves ON moves.[moveId] = move_id WHERE pokemon_id = @p0 AND (level <= @p1 OR level IS NULL) ORDER BY level, moveId";

        const string uspGetWeaknesses = @"SELECT dt.identifier FROM [type_efficacy] te LEFT JOIN [types] as dt ON dt.[id] =te.damage_type_id LEFT JOIN [types] as dtb ON dtb.[id] = te.target_type_id WHERE @p0 LIKE '%' + dtb.identifier + '%' and damage_factor > 100";
        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a list of all pokemon at the given level, eliminating pokemon that would have already evolved at this level, as well as pokemon that haven't evelolved at this level.
        /// </summary>
        public List<int> GetPossiblePokemon(int level, Entropy entopy)
        {
            var list = new List<int>();
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = ctx.tbl_vwEvolutions.SqlQuery(uspGetPossiblePokemon, level).ToList();
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
                var results = ctx.Database.SqlQuery<uspGetPokemonMoveSetResult>(uspGetPokemonMoveSet, id, level).ToList();


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
        public List<uspGetPokemonMoveSetResult> getRandomMoves(int minPower, int maxPower)
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
                var results = ctx.Database.SqlQuery<string>(uspGetWeaknesses, v).ToList();
                return results.ToList();
            }
        }

        /// <summary>
        /// Gets all TMs.
        /// </summary>
        internal List<int> getTMs()
        {
            using (var ctx = new ThePokeBase())
            {
                //Get student name of string type
                var results = from tm in ctx.tbl_vwTMs
                              select tm.move_id;
                return results.ToList();
            }
        }
        #endregion
    }
}
