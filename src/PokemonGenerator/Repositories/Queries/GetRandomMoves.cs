namespace PokemonGenerator.Repositories.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetRandomMoves = @"
            SELECT 
                 level
                ,moveId
                ,moveName
                ,identifier AS Type
                ,power
                ,pp
                ,damageType
                ,effect 
            FROM tbl_vwPokemonMoves 
            INNER JOIN tbl_vwGenIIMoves moves 
                ON moves.[moveId] = move_id 
            WHERE [power] >= @minPower 
                AND [power] <= @maxPower 
            ORDER BY level, moveId";
    }
}