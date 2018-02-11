using Dapper;
using Microsoft.Extensions.Configuration;
using PokemonGenerator.Models.DTO;
using PokemonGenerator.Models.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;

namespace PokemonGenerator.Repositories
{
    /// <summary>
    /// Accesses the database to return information about pokemon. 
    /// 
    /// Data base is a modified version of of the project found here: https://github.com/veekun/pokedex
    /// </summary>
    public interface IPokemonRepository
    {
        /// <summary>
        /// Gets a list of all pokemon at the given level, eliminating pokemon that would have already evolved at this level, as well as pokemon that haven't evelolved at this level.
        /// </summary>
        IEnumerable<int> GetPossiblePokemon(int level);

        /// <summary>
        /// Gets a list of all pokemon at the given level, eliminating only pokemon that haven't evelolved at this level.
        /// </summary>
        IEnumerable<int> GetRandomBagPokemon(int level);

        /// <summary>
        /// Gets a list of all moves available to the pokemon at the given level, eliminating moves that would only be avaialable at later levels.
        /// </summary>
        IEnumerable<PokemonMoveSetResult> GetMovesForPokemon(int id, int level);

        /// <summary>
        /// Gets the base stats for the team of pokemon.
        /// </summary>
        IEnumerable<BaseStats> GetTeamBaseStats(PokeList list);

        /// <summary>
        /// Gets a random move. Completely random, but with a min and max power.
        /// </summary>
        IEnumerable<PokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower);

        /// <summary>
        /// Gets the types that are strong against the given type.
        /// </summary>
        IEnumerable<string> GetWeaknesses(string v);

        /// <summary>
        /// Gets all TMs.
        /// </summary>
        IEnumerable<int> GetTMs();

        /// <summary>
        /// Gets all Pokemon names and ids
        /// </summary>
        IEnumerable<PokemonEntry> GetAllPokemon();
    }

    /// <inheritdoc cref="IPokemonRepository" />
    public class PokemonRepository : IPokemonRepository, IDisposable
    {
        private readonly IDbConnection _dbConnection;

        public PokemonRepository(IConfiguration configuration)
        {
            _dbConnection = new SqlCeConnection(configuration.GetConnectionString("ThePokeBase"));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        /// <inheritdoc />
        public IEnumerable<int> GetPossiblePokemon(int level)
        {
            return _dbConnection.Query<int>(Queries.Queries.GetPossiblePokemon, new { level }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<int> GetRandomBagPokemon(int level)
        {
            return _dbConnection.Query<int>(Queries.Queries.GetRandomBagPokemon, new { level }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<PokemonMoveSetResult> GetMovesForPokemon(int id, int level)
        {
            return _dbConnection.Query<PokemonMoveSetResult>(Queries.Queries.GetPokemonMoveSet, new { level, id }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<BaseStats> GetTeamBaseStats(PokeList list)
        {
            return _dbConnection.Query<BaseStats>(Queries.Queries.GetTeamBaseStats, new { ids = list.Species.Select(s => (int)s).ToList() }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<PokemonMoveSetResult> GetRandomMoves(int minPower, int maxPower)
        {
            return _dbConnection.Query<PokemonMoveSetResult>(Queries.Queries.GetRandomMoves
                , new { minPower = minPower, maxPower = maxPower }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<string> GetWeaknesses(string type)
        {
            return _dbConnection.Query<string>(Queries.Queries.GetWeaknesses, new { type }, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<int> GetTMs()
        {
            return _dbConnection.Query<int>(Queries.Queries.GetTMs, commandType: CommandType.Text);
        }

        /// <inheritdoc />
        public IEnumerable<PokemonEntry> GetAllPokemon()
        {
            return _dbConnection.Query<PokemonEntry>(Queries.Queries.GetAllPokemon, commandType: CommandType.Text);
        }
    }
}