﻿namespace PokemonGenerator.DAL.Queries
{
    internal static partial class Queries
    {
        public static readonly string GetAllPokemon = @"
            SELECT 
                 id
                ,identifier
            FROM  tbl_vwBaseStats";
    }
}