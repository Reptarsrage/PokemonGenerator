using Newtonsoft.Json;
using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.IO
{
    class PersistentConfigManager : IPersistentConfigManager
    {
        private string _configFileName;
        private readonly JsonSerializerSettings _settings;

        public string ConfigFilePath
        {
            get
            {
                return _configFileName;
            }
            set
            {
                _configFileName = value;
            }
        }

        public PersistentConfigManager()
        {
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public PersistentConfig Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_configFileName), _settings);
            }
            catch { /* TODO: Error reporting */  }

            return new PersistentConfig
            {
                Configuration = new PokemonGeneratorConfig(),
                Options = new PokeGeneratorOptions()
            };
        }

        public void Save(PersistentConfig config)
        {
            try
            {
                File.WriteAllText(_configFileName, JsonConvert.SerializeObject(config, _settings));
            }
            catch { /* TODO: Error reporting */  }
        }
    }
}