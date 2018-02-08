using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Repositories;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace PokemonGenerator.Tests.Integration.IO_Tests
{
    public class PersistentConfigManagerTests : IDisposable
    {
        private readonly IConfigRepository _repository;
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
            _repository = new ConfigRepository(_testConfig);
            _outFile = Path.Combine(contentDir, ConfigRepository.ConfigFileName);
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
            _repository.Save();
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
            _repository.Save();
            var saved = JsonConvert.DeserializeObject<PersistentConfig>(File.ReadAllText(_outFile));

            // Base Object
            Assert.Equal(_testConfig.Value.Options.Level, saved.Options.Level);
            Assert.Equal(_testConfig.Value.Options.Project64Location, saved.Options.Project64Location);

            // Players
            Assert.Equal(_testConfig.Value.Options.PlayerOne.GameVersion, saved.Options.PlayerOne.GameVersion);
            Assert.Equal(_testConfig.Value.Options.PlayerOne.Name, saved.Options.PlayerOne.Name);
            Assert.Equal(_testConfig.Value.Options.PlayerOne.InputSaveLocation, saved.Options.PlayerOne.InputSaveLocation);
            Assert.Equal(_testConfig.Value.Options.PlayerOne.OutputSaveLocation, saved.Options.PlayerOne.OutputSaveLocation);

            Assert.Equal(_testConfig.Value.Options.PlayerTwo.GameVersion, saved.Options.PlayerTwo.GameVersion);
            Assert.Equal(_testConfig.Value.Options.PlayerTwo.Name, saved.Options.PlayerTwo.Name);
            Assert.Equal(_testConfig.Value.Options.PlayerTwo.InputSaveLocation, saved.Options.PlayerTwo.InputSaveLocation);
            Assert.Equal(_testConfig.Value.Options.PlayerTwo.OutputSaveLocation, saved.Options.PlayerTwo.OutputSaveLocation);
        }
    }
}