using System;
using System.IO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonGenerator.Models.Configuration;

namespace PokemonGenerator.Repositories
{
    public interface IConfigRepository
    {
        void Save();
    }

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
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            _options = options;
        }

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