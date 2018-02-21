namespace PokemonGenerator.Repositories.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetTeamBaseStats = @"
            SELECT 
                 id
                ,identifier
                ,Types
                ,hp
                ,attack
                ,defense
                ,spAttack
                ,spDefense
                ,speed
                ,growthRate
            FROM  tbl_vwBaseStats 
            WHERE id IN @ids";
    }
}
