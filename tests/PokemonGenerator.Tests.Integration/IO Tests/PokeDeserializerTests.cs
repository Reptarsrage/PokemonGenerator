using PokemonGenerator.IO;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using System;
using System.IO;
using Xunit;

namespace PokemonGenerator.Tests.Integration.IO_Tests
{
    public class SaveFileRepositoryTests : SerializerTestsBase, IDisposable
    {
        private readonly string _testFile;
        private readonly ISaveFileRepository _saveFileRepository;
        private readonly SAVFileModel _expectedModel;
        private Stream _testStream;

        public SaveFileRepositoryTests()
        {
            _testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav");
            _saveFileRepository = new SaveFileRepository(new Charset(), new BinaryWriter2(), new BinaryReader2()); ;
            _expectedModel = BuildTestModel();
        }

        public void Dispose()
        {
            _testStream.Close();
            _testStream.Dispose();
        }

        [Fact]
        public void SerializeSaveFileModalHasCorrectLengthTest()
        {
            // Setup
            _testStream = File.OpenRead(_testFile);

            // Run
            var resultModel = _saveFileRepository.Deserialize(_testStream);

            // Assert
            Assert.NotNull(resultModel);
            Assert.Equal(6, resultModel.TeamPokemonList.Count);
        }

        [Fact]
        public void SerializeSaveFileModalBadChecksumTest()
        {
            // Setup
            _testStream = new MemoryStream(File.ReadAllBytes(_testFile));
            _testStream.Seek(0x2D69, SeekOrigin.Begin);
            _testStream.Write(new byte[2] { 0xbe, 0xef }, 0, 2);

            // Run
            Assert.Throws<InvalidDataException>(() => _saveFileRepository.Deserialize(_testStream));
        }

        [Fact]
        public void SerializeAndDeserializeSaveFileModalTest()
        {
            // Serilize model
            _testStream = new FileStream(_testFile, FileMode.OpenOrCreate);
            _saveFileRepository.Serialize(_testStream, _expectedModel);

            // Deserialize Model
            _testStream = new FileStream(_testFile, FileMode.OpenOrCreate);
            var resultModel = _saveFileRepository.Deserialize(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Fact]
        public void SerializeSaveFileModalCorrectValuesTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav"));

            // Run
            var resultModel = _saveFileRepository.Deserialize(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Fact]
        public void SerializeSaveFileModalChecksumTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav"));

            // Run
            var resultModel = _saveFileRepository.Deserialize(_testStream);

            // Assert some values
            Assert.True(_expectedModel.Checksum1.Equals(resultModel.Checksum1), "Checksum1");
        }
    }
}