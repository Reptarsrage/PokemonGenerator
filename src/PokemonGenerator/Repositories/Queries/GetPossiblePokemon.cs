﻿namespace PokemonGenerator.Repositories.Queries
{
    internal static partial class Queries
    {
        /// <summary>
        /// All possible pokemon available at the given level (including all evolutions)
        /// </summary>
        /// <param name="level">The max pokemon level</param>
        public static readonly string GetPossiblePokemon = @"
        SELECT DISTINCT D.* 
        FROM [tbl_vwevolutions] D 
        WHERE D.id NOT IN (
            SELECT DISTINCT C.[id] 
            FROM [tbl_vwevolutions] A 
            INNER JOIN [tbl_vwevolutions] B 
               ON A.evolvedfromprevid = B.id 
            INNER JOIN [tbl_vwevolutions] C 
                ON B.evolvedfromprevid = C.id 
            WHERE COALESCE(A.minimum_level, 0) <= @level 
                AND C.id IS NOT NULL)
                AND COALESCE(D.minimum_level, 0) < @level";
    }
}