using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonGenerator.Models;
using System;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IPersistentConfigManager
    {
        void Save();
    }

    public class PersistentConfigManager : IPersistentConfigManager
    {
        public const string ConfigFileName = "configuration.json";

        private readonly IOptions<PersistentConfig> _options;
        private readonly string _optionsFilePath;
        private readonly JsonSerializerSettings _settings;

        public PersistentConfigManager(IOptions<PersistentConfig> options)
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