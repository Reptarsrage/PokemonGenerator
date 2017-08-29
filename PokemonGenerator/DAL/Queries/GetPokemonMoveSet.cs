﻿namespace PokemonGenerator.DAL.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetPokemonMoveSet = @"
            SELECT 
                level, 
                moveid, 
                movename, 
                identifier AS Type, 
                power, 
                pp, 
                damagetype, 
                effect, 
                method AS learnType 
            FROM tbl_vwpokemonmoves 
            INNER JOIN tbl_vwgeniimoves moves 
                ON moves.[moveid] = move_id 
            WHERE pokemon_id = @p0 
                AND ( level <= @p1 OR level IS NULL ) 
            ORDER BY level, moveid";
    }
}
