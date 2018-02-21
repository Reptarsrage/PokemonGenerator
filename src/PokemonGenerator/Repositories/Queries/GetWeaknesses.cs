namespace PokemonGenerator.Repositories.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetWeaknesses = @"
            SELECT 
                dt.identifier 
            FROM [type_efficacy] te 
            LEFT JOIN [types] AS dt 
                ON dt.[id] = te.damage_type_id 
            LEFT JOIN [types] AS dtb 
                ON dtb.[id] = te.target_type_id 
            WHERE @type LIKE '%' + dtb.identifier + '%' 
                AND damage_factor > 100 ";
    }
}
