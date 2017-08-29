using NUnit.Framework;
using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.Tests.Unit
{
    [TestFixture]
    public class PokemonGeneratorRunnerTests
    {
        private IPokemonGeneratorRunner _runner;
        private string _contentDir;
        private string _outputDir;

        [SetUp]
        public void Init()
        {
            _contentDir = PokemonGeneratorRunner.AssemblyDirectory;
            _outputDir = Path.Combine(PokemonGeneratorRunner.AssemblyDirectory, "Out");
            _runner = null;
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(_outputDir))
            {
                Directory.Delete(_outputDir, true);
            }
        }

        [Test]
        public void RunTest()
        {
            var opts = new PokeGeneratorArguments
            {
                EntropyVal = "Low",
                GameOne = "Gold",
                GameTwo = "Gold",
                InputSavOne = Path.Combine(_contentDir, "gold.sav"),
                InputSavTwo = Path.Combine(_contentDir, "gold.sav"),
                OutputSav1 = Path.Combine(_outputDir, "out1.sav"),
                OutputSav2 = Path.Combine(_outputDir, "out2.sav"),
                NameOne = "Test1",
                NameTwo = "Test2",
                Level = 100
            };
            _runner = new PokemonGeneratorRunner(_contentDir, _outputDir, opts);
            _runner.Run();


            Assert.True(File.Exists(Path.Combine(_outputDir, opts.OutputSav1)));
            Assert.True(File.Exists(Path.Combine(_outputDir, opts.OutputSav2)));
        } 
    }
}
