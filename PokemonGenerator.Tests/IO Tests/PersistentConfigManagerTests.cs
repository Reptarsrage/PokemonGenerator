using Newtonsoft.Json;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace PokemonGenerator.Tests.IO_Tests
{
    public class PersistentConfigManagerTests : IDisposable
    {
        private readonly IPersistentConfigManager _manager;
        private readonly string _outDir;
        private readonly PersistentConfig _testConfig;

        public PersistentConfigManagerTests()
        {
            _manager = new PersistentConfigManager();
            _outDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Out");
            _testConfig = new PersistentConfig(new PokemonGeneratorConfig(), new PokeGeneratorOptions());

            // Check directory exists
            if (!Directory.Exists(_outDir))
            {
                Directory.CreateDirectory(_outDir);
            }
        }

        public void Dispose()
        {
            // Clean directory
            if (Directory.Exists(_outDir))
            {
                Directory.Delete(_outDir, true);
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LoadValidOptionsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            File.WriteAllText(outFile, JsonConvert.SerializeObject(_testConfig));
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            foreach (var propertyInfo in typeof(PokeGeneratorOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Options), (propertyInfo.GetValue(loaded.Options)));
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LoadValidConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            File.WriteAllText(outFile, JsonConvert.SerializeObject(_testConfig));
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            foreach (var propertyInfo in typeof(PokemonGeneratorConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Configuration), (propertyInfo.GetValue(loaded.Configuration)));
                }
            }
        }


        [Fact]
        [Trait("Category", "Integration")]
        public void SaveValidConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            _manager.ConfigFilePath = outFile;
            _manager.Save(_testConfig);
            var saved = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(outFile), new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

            foreach (var propertyInfo in typeof(PokemonGeneratorConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Configuration), (propertyInfo.GetValue(saved.Configuration)));
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void SaveValidOptionsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            _manager.ConfigFilePath = outFile;
            _manager.Save(_testConfig);
            var saved = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(outFile));

            foreach (var propertyInfo in typeof(PokeGeneratorOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Options), (propertyInfo.GetValue(saved.Options)));
                }
            }
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LoadMissingConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            var config = new PersistentConfig(new PokemonGeneratorConfig(), new PokeGeneratorOptions());
            config.Configuration.MoveEffectFilters.Remove("heal");
            File.WriteAllText(outFile, JsonConvert.SerializeObject(config));
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            Assert.False(loaded.Configuration.MoveEffectFilters.ContainsKey("heal"));
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LoadEmptyConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            var config = new PersistentConfig(null, new PokeGeneratorOptions());
            File.WriteAllText(outFile, JsonConvert.SerializeObject(_testConfig));
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            Assert.NotNull(loaded?.Configuration);
            Assert.NotNull(loaded?.Options);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void LoadNotFoundConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "fake.json");
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            Assert.NotNull(loaded?.Configuration);
            Assert.NotNull(loaded?.Options);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void SaveNullTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            _manager.ConfigFilePath = outFile;
            _manager.Save(_testConfig);
        }
    }
}