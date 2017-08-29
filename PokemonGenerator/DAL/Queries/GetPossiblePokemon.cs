namespace PokemonGenerator.DAL.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetPossiblePokemon = @"
        SELECT DISTINCT D.* 
        FROM [tbl_vwevolutions] D 
        WHERE D.id NOT IN (
            SELECT DISTINCT B.[id] 
            FROM [tbl_vwevolutions] A 
            INNER JOIN [tbl_vwevolutions] B 
                ON A.evolvedfromprevid = B.id 
            WHERE COALESCE(A.minimum_level, 0) <= @p0 
                AND B.id IS NOT NULL

            UNION 

            SELECT DISTINCT C.[id] 
            FROM [tbl_vwevolutions] A 
            INNER JOIN [tbl_vwevolutions] B 
               ON A.evolvedfromprevid = B.id 
            INNER JOIN [tbl_vwevolutions] C 
                ON B.evolvedfromprevid = C.id 
            WHERE COALESCE(A.minimum_level, 0) <= @p0 
                AND C.id IS NOT NULL)
                AND COALESCE(D.minimum_level, 0) < @p0";
    }
}