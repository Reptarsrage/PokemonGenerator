using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonGenerator.Models.Configuration;
using System;
using System.IO;

namespace PokemonGenerator.Repositories
{
    /// <summary>
    /// Saves persistent program config and options
    /// </summary>
    public interface IConfigRepository
    {
        /// <summary>
        /// Writes persistant config to file
        /// </summary>
        void Save();
    }

    /// <inheritdoc />
    public class ConfigRepository : IConfigRepository
    {
        public const string ConfigFileName = "configuration.json";

        private readonly IOptions<PersistentConfig> _options;
        private readonly string _optionsFilePath;
        private readonly JsonSerializerSettings _settings;

        public ConfigRepository(IOptions<PersistentConfig> options)
        {
            var dir = (string)AppDomain.CurrentDomain.GetData("DataDirectory");
            _optionsFilePath = Path.Combine(dir, ConfigFileName);

            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            _options = options;
        }

        /// <inheritdoc />
        public void Save()
        {
            try
            {
                File.WriteAllText(_optionsFilePath, JsonConvert.SerializeObject(_options.Value, _settings));
            }
            catch { /* TODO: Error reporting */  }
        }
    }
}