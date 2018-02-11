namespace PokemonGenerator.Repositories.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetAllPokemon = @"
            SELECT 
                 STATS.id
                ,STATS.identifier
                ,COALESCE(EVOLVE.minimum_level, 0) AS [minimumLevel]
            FROM  tbl_vwBaseStats STATS
            LEFT JOIN [tbl_vwevolutions] EVOLVE
                ON EVOLVE.id = STATS.id";
    }
}