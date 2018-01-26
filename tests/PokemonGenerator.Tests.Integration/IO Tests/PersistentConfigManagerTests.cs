using Microsoft.Extensions.Options;
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
        private readonly IOptions<PersistentConfig> _testConfig;
        private readonly string _outFile;
        private readonly string _outDir;

        public PersistentConfigManagerTests()
        {
            var contentDir = Directory.GetCurrentDirectory();
            _outDir = Path.Combine(contentDir, $"Test-{Guid.NewGuid()}");
            AppDomain.CurrentDomain.SetData("DataDirectory", contentDir);

            // Check directory exists
            if (!Directory.Exists(_outDir))
            {
                Directory.CreateDirectory(_outDir);
            }

            _testConfig = Options.Create(new PersistentConfig());
            _manager = new PersistentConfigManager(_testConfig);
            _outFile = Path.Combine(contentDir, PersistentConfigManager.ConfigFileName);
        }

        public void Dispose()
        {
            // Clean directory
            if (Directory.Exists(_outDir))
            {
                Directory.Delete(_outDir, true);
            }

            // Clean app setttings
            if (File.Exists(_outFile))
            {
                File.Delete(_outFile);
            }
        }

        [Fact]
        public void SaveValidConfigurationsTest()
        {
            _manager.Save();
            var saved = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_outFile), new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

            foreach (var propertyInfo in typeof(PokemonGeneratorConfig).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Value.Configuration), (propertyInfo.GetValue(saved.Configuration)));
                }
            }
        }

        [Fact]
        public void SaveValidOptionsTest()
        {
            _manager.Save();
            var saved = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_outFile));

            foreach (var propertyInfo in typeof(PokeGeneratorOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propertyInfo.CanWrite)
                {
                    Assert.Equal(propertyInfo.GetValue(_testConfig.Value.Options), (propertyInfo.GetValue(saved.Options)));
                }
            }
        }
    }
}