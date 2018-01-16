using Newtonsoft.Json;
using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IPersistentConfigManager
    {
        string ConfigFilePath { get; set; }

        PersistentConfig Load();
        void Save(PersistentConfig config);
    }

    public class PersistentConfigManager : IPersistentConfigManager
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
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
        }

        public PersistentConfig Load()
        {
            var ret = new PersistentConfig(new PokemonGeneratorConfig(), new PokeGeneratorOptions());
            try
            {
                var parsed = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_configFileName), _settings);
                ret.Configuration = parsed.Configuration ?? ret.Configuration;
                ret.Options = parsed.Options ?? ret.Options;
            }
            catch { /* TODO: Error reporting */  }

            return ret;
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