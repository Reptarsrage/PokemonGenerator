using Newtonsoft.Json;
using NUnit.Framework;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System.IO;
using System.Reflection;

namespace PokemonGenerator.Tests.IO_Tests
{
    [TestFixture]
    public class PersistentConfigManagerTests
    {
        private readonly IPersistentConfigManager _manager;
        private readonly string _outDir;
        private readonly PersistentConfig _testConfig;

        public PersistentConfigManagerTests()
        {
            _manager = new PersistentConfigManager();
            _outDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Out");
            _testConfig = new PersistentConfig(new PokemonGeneratorConfig(), new PokeGeneratorOptions());
        }

        [SetUp]
        public void SetUp()
        {
            if (!Directory.Exists(_outDir))
            {
                Directory.CreateDirectory(_outDir);
            }
        }

        [TearDown]
        public void CleanUp()
        {
            if (Directory.Exists(_outDir))
            {
                Directory.Delete(_outDir, true);
            }
        }

        [Test]
        [Category("Integration")]
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
                    Assert.AreEqual(propertyInfo.GetValue(_testConfig.Options), propertyInfo.GetValue(loaded.Options), propertyInfo.Name);
                }
            }
        }

        [Test]
        [Category("Integration")]
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
                    Assert.AreEqual(propertyInfo.GetValue(_testConfig.Configuration), propertyInfo.GetValue(loaded.Configuration), propertyInfo.Name);
                }
            }
        }


        [Test]
        [Category("Integration")]
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
                    Assert.AreEqual(propertyInfo.GetValue(_testConfig.Configuration), propertyInfo.GetValue(saved.Configuration), propertyInfo.Name);
                }
            }
        }

        [Test]
        [Category("Integration")]
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
                    Assert.AreEqual(propertyInfo.GetValue(_testConfig.Options), propertyInfo.GetValue(saved.Options), propertyInfo.Name);
                }
            }
        }

        [Test]
        [Category("Integration")]
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

        [Test]
        [Category("Integration")]
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

        [Test]
        [Category("Integration")]
        public void LoadNotFoundConfigurationsTest()
        {
            var outFile = Path.Combine(_outDir, "fake.json");
            _manager.ConfigFilePath = outFile;
            var loaded = _manager.Load();

            Assert.NotNull(loaded?.Configuration);
            Assert.NotNull(loaded?.Options);
        }

        [Test]
        [Category("Integration")]
        public void SaveNullTest()
        {
            var outFile = Path.Combine(_outDir, "test.json");
            _manager.ConfigFilePath = outFile;
            Assert.DoesNotThrow(() => _manager.Save(_testConfig));
        }
    }
}