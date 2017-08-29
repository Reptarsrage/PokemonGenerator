namespace PokemonGenerator.DAL.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetTMs = @"
            SELECT 
                move_id 
            FROM tbl_vwTMs";
    }
}