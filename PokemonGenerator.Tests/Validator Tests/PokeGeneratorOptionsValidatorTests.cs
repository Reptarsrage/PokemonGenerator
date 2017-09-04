using NUnit.Framework;
using PokemonGenerator.Enumerations;
using PokemonGenerator.Validators;
using System;
using System.IO;
using System.Reflection;

namespace PokemonGenerator.Tests.Validator_Tests
{

    [TestFixture]
    class PokeGeneratorOptionsValidatorTests
    {
        private readonly IPokeGeneratorOptionsValidator _validator;

        public PokeGeneratorOptionsValidatorTests()
        {
            _validator = new PokeGeneratorOptionsValidator();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("ThisNameIsFarTooLong")]
        [TestCase("         ")]
        [TestCase("123456789")]
        [TestCase("Player_1")]
        [TestCase("Player 1")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateNameFailsTest(string name)
        {
            Assert.False(_validator.ValidateName(name));
        }

        [Test]
        [TestCase("Player1")]
        [TestCase("P")]
        [TestCase("1")]
        [TestCase("12345678")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateNameSucceedsTest(string name)
        {
            Assert.True(_validator.ValidateName(name));
        }

        [Test]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        [TestCase(0)]
        [TestCase(101)]
        [TestCase(4)]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateLevelFailsTest(int level)
        {
            Assert.False(_validator.ValidateLevel(level));
        }

        [Test]
        [TestCase(5)]
        [TestCase(100)]
        [TestCase(99)]
        [TestCase(6)]
        [TestCase(50)]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateLevelSucceedsTest(int level)
        {
            Assert.True(_validator.ValidateLevel(level));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateEntropySucceedsWithEnumValsTest()
        {
            foreach (var name in Enum.GetNames(typeof(Entropy)))
            {
                Assert.True(_validator.ValidateEntropy(name));
            }
        }

        [Test]
        [TestCase("Fake")]
        [TestCase("LOW")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateEntropyFailsTest(string entropy)
        {
            Assert.False(_validator.ValidateEntropy(entropy));
        }

        [Test]
        [TestCase("Gold")]
        [TestCase("Silver")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateGameSucceedsTest(string game)
        {
            Assert.True(_validator.ValidateGame(game));
        }

        [Test]
        [TestCase("Ruby")]
        [TestCase("Emerald")]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateGameFailsTest(string game)
        {
            Assert.False(_validator.ValidateGame(game));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("\t     \n\r")]
        [TestCase("/Fakey/Fake/Dir/")]
        [TestCase(@"C:\%^&#%$&((&#$\")]
        [TestCase(@"myFile.txt")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFileOptionFailsTest(string path)
        {
            Assert.False(_validator.ValidateFileOption(path, ".sav"));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFileOptionFailsDueToExtensionTest()
        {
            var actualFile = Assembly.GetExecutingAssembly().Location;
            Assert.False(_validator.ValidateFileOption(actualFile, ".fake"));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFileOptionSucceedsTest()
        {
            var actualFile = Assembly.GetExecutingAssembly().Location;
            Assert.True(_validator.ValidateFileOption(actualFile, ".dll"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("\t     \n\r")]
        [TestCase(@"C:\<>:""/\|?*")]
        [Category("Integration")]
        public void PokeGeneratorOptionsValidateFilePathOptionFailsTest(string path)
        {
            Assert.False(_validator.ValidateFilePathOption(path, ".sav"));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFilePathOptionFailsDueToExtensionTest()
        {
            var actualFile = Assembly.GetExecutingAssembly().Location;
            Assert.False(_validator.ValidateFilePathOption(actualFile, ".fake"));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFilePathOptionSucceedsTest()
        {
            var actualFile = Assembly.GetExecutingAssembly().Location;
            Assert.True(_validator.ValidateFilePathOption(actualFile, ".dll"));
        }

        [Test]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateFilePathOptionDoesNotxistSucceedsTest()
        {
            var actualFile = Assembly.GetExecutingAssembly().Location;
            var couldBeAFile = Path.Combine(Path.GetDirectoryName(actualFile), "Fake/FakeyFakeTown/Test", Path.GetFileName(actualFile));
            Assert.True(_validator.ValidateFilePathOption(actualFile, ".dll"));
        }

        [Test]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        [TestCase(null, null)]
        [TestCase("  ", "       ")]
        [TestCase("  ", @"C:\ActualFile.txt")]
        [TestCase(@"C:\FakeFile.txt", @"C:\FakeFile.txt")]
        [TestCase(@"C:\FakeFILE.txt", @"C:\FakeFile.txt")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateUniquePathFailsTest(string path1, string path2)
        {
            Assert.False(_validator.ValidateUniquePath(path1, path2));
        }

        [Test]
        [TestCase(@"C:\ActualFile.txt", @"C:\ActualFile2.txt")]
        [TestCase(@"C:\One\FakeFile.txt", @"C:\Two\FakeFile.txt")]
        [Category("Unit")]
        public void PokeGeneratorOptionsValidateUniquePathSucceedsTest(string path1, string path2)
        {
            Assert.True(_validator.ValidateUniquePath(path1, path2));
        }
    }
}