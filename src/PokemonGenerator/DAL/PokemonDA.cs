using Dapper;
using Microsoft.Extensions.Configuration;
using PokemonGenerator.Models.Dto;
using PokemonGenerator.Models.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

namespace PokemonGenerator.DAL
{
    public interface IPokemonDA
    {
        IEnumerable<PokemonMoveSetResult> GetMovesForPokemon(int id, int level);
        IEnumerable<int> GetPossiblePokemon(int level);
        IEnumerable<PokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower);
        IEnumerable<BaseStats> GetTeamBaseStats(PokeList list);
        IEnumerable<PokemonEntry> GetAllPokemon();
        IEnumerable<string> GetWeaknesses(string v);
        IEnumerable<int> GetTMs();
    }

    /// <summary>
    /// Accesses the database to return information about pokemon. 
    /// <para/> 
    /// Data base is a modified version of of the project found here: https://github.com/veekun/pokedex
    /// </summary>
    public class PokemonDA : IPokemonDA, IDisposable
    {
        private readonly IDbConnection _dbConnection;

        public PokemonDA(IConfiguration configuration)
        {
            _dbConnection = new SqlCeConnection(configuration.GetConnectionString("ThePokeBase"));
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        /// <summary>
        /// Gets a list of all pokemon at the given level, eliminating pokemon that would have already evolved at this level, as well as pokemon that haven't evelolved at this level.
        /// </summary>
        public IEnumerable<int> GetPossiblePokemon(int level)
        {
            return _dbConnection.Query<int>(Queries.Queries.GetPossiblePokemon, new { level = level }, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets a list of all moves available to the pokemon at the given level, eliminating moves that would only be avaialable at later levels.
        /// </summary>
        public IEnumerable<PokemonMoveSetResult> GetMovesForPokemon(int id, int level)
        {
            return _dbConnection.Query<PokemonMoveSetResult>(Queries.Queries.GetPokemonMoveSet, new { level = level, id = id }, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets the base stats for the team of pokemon.
        /// </summary>
        public IEnumerable<BaseStats> GetTeamBaseStats(PokeList list)
        {
            return _dbConnection.Query<BaseStats>(Queries.Queries.GetTeamBaseStats, new { ids = list.Species.Select(s => (int)s).ToList() }, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets a random move. Completely random, but with a min and max power.
        /// </summary>
        public IEnumerable<PokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower)
        {
            return _dbConnection.Query<PokemonMoveSetResult>(Queries.Queries.GetRandomMoves
                , new { minPower = minPower, maxPower = maxPower }, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets the types that are strong against the given type.
        /// </summary>
        public IEnumerable<string> GetWeaknesses(string type)
        {
            return _dbConnection.Query<string>(Queries.Queries.GetWeaknesses, new { type = type }, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets all TMs.
        /// </summary>
        public IEnumerable<int> GetTMs()
        {
            return _dbConnection.Query<int>(Queries.Queries.GetTMs, commandType: CommandType.Text);
        }

        /// <summary>
        /// Gets all Pokemon names and ids
        /// </summary>
        public IEnumerable<PokemonEntry> GetAllPokemon()
        {
            return _dbConnection.Query<PokemonEntry>(Queries.Queries.GetAllPokemon, commandType: CommandType.Text);
        }
    }
}