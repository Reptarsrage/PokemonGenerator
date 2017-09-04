using NUnit.Framework;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System;
using System.IO;
using System.Linq;

namespace PokemonGenerator.Tests
{
    [TestFixture]
    public class PokemonGeneratorRunnerTests
    {
        private IPokemonGeneratorRunner _runner;
        private IPokeDeserializer _deserializer;
        private PersistentConfig _opts;
        private string _contentDir;
        private string _outputDir;

        [SetUp]
        public void Init()
        {
            _contentDir = GUIProgram.AssemblyDirectory;
            _outputDir = Path.Combine(GUIProgram.AssemblyDirectory, "Out");
            _opts = new PersistentConfig
            {
                Options = new PokeGeneratorOptions {
                    EntropyVal = "Low",
                    GameOne = "Gold",
                    GameTwo = "Gold",
                    InputSaveOne = Path.Combine(_contentDir, "gold.sav"),
                    InputSaveTwo = Path.Combine(_contentDir, "gold.sav"),
                    OutputSaveOne = Path.Combine(_outputDir, "out1.sav"),
                    OutputSaveTwo = Path.Combine(_outputDir, "out2.sav"),
                    NameOne = "Test1",
                    NameTwo = "Test2",
                    Level = 100
                },
                Configuration = new PokemonGeneratorConfig()
            };

            using (var injector = new NinjectWrapper())
            {
                _runner = injector.Get<IPokemonGeneratorRunner>();
                _deserializer = injector.Get<IPokeDeserializer>();
            }
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(_outputDir))
            {
                Directory.Delete(_outputDir, true);
            }
            _runner = null;
        }

        [Test]
        [Category("Integration")]
        public void RunOutputExistsTest()
        {
            _runner.Run(_opts);

            Assert.True(File.Exists(Path.Combine(_outputDir, _opts.Options.OutputSaveOne)));
            Assert.True(File.Exists(Path.Combine(_outputDir, _opts.Options.OutputSaveTwo)));
        }

        [Test]
        [Category("Integration")]
        public void RunOutputValidTest()
        {
            _runner.Run(_opts);

            SAVFileModel model1 = null, model2 = null;
            try
            {
                model1 = _deserializer.ParseSAVFileModel(Path.Combine(_outputDir, _opts.Options.OutputSaveOne));
                model2 = _deserializer.ParseSAVFileModel(Path.Combine(_outputDir, _opts.Options.OutputSaveTwo));

            }
            catch (Exception e)
            {
                Assert.Fail($"Failed to parse the output save files: {e}");
            }

            Assert.NotNull(model1);
            Assert.NotNull(model2);

            // Basic checks
            Assert.AreEqual("Test1", model1.Playername, "Name not set correctly");
            Assert.AreEqual("Test2", model2.Playername, "Name not set correctly");
            Assert.AreEqual(_opts.Configuration.TEAM_SIZE, model1.TeamPokemonlist.Pokemon.Count(), "Team not set correctly");
            Assert.AreEqual(_opts.Configuration.TEAM_SIZE, model2.TeamPokemonlist.Pokemon.Count(), "Team not set correctly");
            foreach (var pokemon in model1.TeamPokemonlist.Pokemon)
            {
                Assert.AreEqual(100, pokemon.level, "Level not set correctly");
            }
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("\t     \n\r")]
        [TestCase("/Fakey/Fake/Dir/")]
        [TestCase(@"C:\%^&#%$&((&#$\")]
        [TestCase(@"myFile.txt")]
        [Category("Integration")]
        public void RunInvalidContentPathTest(string path)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _opts.Options.InputSaveOne = path;
                _opts.Options.InputSaveTwo = path;
                _runner.Run(_opts);
            });
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("\t     \n\r")]
        [TestCase(@"C:\<>:""/\|?*")]
        [Category("Integration")]
        public void RunInvalidOutputPathTest(string path)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                _opts.Options.OutputSaveOne = path;
                _opts.Options.OutputSaveTwo = path;
                _runner.Run(_opts);
            });
        }
    }
}
