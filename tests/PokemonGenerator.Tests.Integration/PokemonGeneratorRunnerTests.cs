using PokemonGenerator.Managers;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Enumerations;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using Xunit;

namespace PokemonGenerator.Tests.Integration
{
    public class PokemonGeneratorRunnerTests : IDisposable
    {
        private IGeneratorManager _manager;
        private readonly ISaveFileRepository _deserializer;
        private readonly PersistentConfig _opts;
        private readonly string _contentDir;
        private readonly string _outputDir;

        public PokemonGeneratorRunnerTests()
        {
            _contentDir = Directory.GetCurrentDirectory();
            _outputDir = Path.Combine(_contentDir, $"Test-{Guid.NewGuid()}");
            _opts = new PersistentConfig
            {
                Options = new ProgramOptions
                {
                    PlayerOne = new PlayerOptions
                    {
                        GameVersion = PokemonGame.Gold.ToString(),
                        InputSaveLocation = Path.Combine(_contentDir, "gold.sav"),
                        OutputSaveLocation = Path.Combine(_outputDir, "out1.sav"),
                        Name = "Test1"
                    },
                    PlayerTwo = new PlayerOptions
                    {
                        GameVersion = PokemonGame.Gold.ToString(),
                        InputSaveLocation = Path.Combine(_contentDir, "gold.sav"),
                        OutputSaveLocation = Path.Combine(_outputDir, "out2.sav"),
                        Name = "Test2"
                    },
                    Level = 100
                }
            };

            AppDomain.CurrentDomain.SetData("DataDirectory", _contentDir);
            var options = DependencyInjector.Get<IOptions<PersistentConfig>>();
            options.Value.Options = _opts.Options;
            _manager = DependencyInjector.Get<IGeneratorManager>();
            _deserializer = DependencyInjector.Get<ISaveFileRepository>();
        }

        public void Dispose()
        {
            if (Directory.Exists(_outputDir))
            {
                Directory.Delete(_outputDir, true);
            }
            _manager = null;
        }

        [Fact]
        public void RunOutputExistsTest()
        {
            _manager.Generate();

            Assert.True(File.Exists(Path.Combine(_outputDir, _opts.Options.PlayerOne.OutputSaveLocation)));
            Assert.True(File.Exists(Path.Combine(_outputDir, _opts.Options.PlayerTwo.OutputSaveLocation)));
        }

        [Fact]
        public void RunOutputValidTest()
        {
            _manager.Generate();

            SaveFileModel model1 = null, model2 = null;
            try
            {
                model1 = _deserializer.Deserialize(Path.Combine(_outputDir, _opts.Options.PlayerOne.OutputSaveLocation));
                model2 = _deserializer.Deserialize(Path.Combine(_outputDir, _opts.Options.PlayerTwo.OutputSaveLocation));
            }
            catch (Exception e)
            {
                Assert.True(false, $"Failed to parse the output save files: {e}");
            }

            Assert.NotNull(model1);
            Assert.NotNull(model2);

            // Basic checks
            Assert.Equal("Test1", model1.PlayerName);
            Assert.Equal("Test2", model2.PlayerName);
            Assert.Equal(_opts.Configuration.TeamSize, model1.TeamPokemonList.Pokemon.Count());
            Assert.Equal(_opts.Configuration.TeamSize, model2.TeamPokemonList.Pokemon.Count());
            foreach (var pokemon in model1.TeamPokemonList.Pokemon)
            {
                Assert.Equal(100, pokemon.Level);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("\t     \n\r")]
        [InlineData("/Fakey/Fake/Dir/")]
        [InlineData(@"C:\%^&#%$&((&#$\")]
        [InlineData(@"myFile.txt")]
        public void RunInvalidContentPathTest(string path)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _opts.Options.PlayerOne.InputSaveLocation = path;
                _opts.Options.PlayerTwo.InputSaveLocation = path;
                _manager.Generate();
            });
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("\t     \n\r")]
        [InlineData(@"C:\<>:""/\|?*")]
        public void RunInvalidOutputPathTest(string path)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _opts.Options.PlayerOne.OutputSaveLocation = path;
                _opts.Options.PlayerTwo.OutputSaveLocation = path;
                _manager.Generate();
            });
        }
    }
}
