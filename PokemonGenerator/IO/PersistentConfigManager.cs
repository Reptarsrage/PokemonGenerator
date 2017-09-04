using Newtonsoft.Json;
using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.IO
{
    class PersistentConfigManager : IPersistentConfigManager
    {
        private string _configFileName;
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

        public PersistentConfig Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_configFileName));
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
                File.WriteAllText(_configFileName, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            catch { /* TODO: Error reporting */  }
        }
    }
}