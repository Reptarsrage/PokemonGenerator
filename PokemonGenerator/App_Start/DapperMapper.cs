using Dapper.ColumnMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PokemonGenerator
{
    /// <summary>
    /// Initializes all custom mappings for Dapper enabled models.
    /// 
    /// If you want your model to have custom dapper mappings. 
    /// Add the ColumnMapping attribute to any property with a mapping.
    /// </summary>
    internal static class DapperMapper
    {
        public static void Init()
        {
            var assemblies = new[]
            {
                    "PokemonGenerator",
                    /* Add additional Dapper-able assemblies here */
                };

            var types = new List<Type>();

            foreach (var assembly in assemblies)
            {
                types.AddRange(Assembly.Load(assembly).GetTypes()
                    .Where(t => t.IsClass));
            }

            ColumnTypeMapper.RegisterForTypes(types.ToArray());
        }
    }
}
