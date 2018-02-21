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
        private readonly SaveFileModel _expectedModel;
        private readonly Stream _testStream;
        private readonly StreamShim _testStreamShim;

        public SaveFileRepositoryTests()
        {
            _testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav");
            using (var fileStream = new FileStream(_testFile, FileMode.Open, FileAccess.Read))
            {
                _testStream = new MemoryStream();
                fileStream.CopyTo(_testStream);
                _testStreamShim = new StreamShim(_testStream);
            }

            _saveFileRepository = new SaveFileRepository(new Charset(), new BinaryWriter2(), new BinaryReader2()); ;
            _expectedModel = BuildTestModel();
        }

        public void Dispose()
        {
            _testStream?.Close();
            _testStream?.Dispose();
        }

        [Fact]
        public void SerializeSaveFileModalHasCorrectLengthTest()
        {
            // Generate
            var resultModel = _saveFileRepository.Deserialize(_testStreamShim);

            // Assert
            Assert.NotNull(resultModel);
            Assert.Equal(6, resultModel.TeamPokemonList.Count);
        }

        [Fact]
        public void SerializeSaveFileModalBadChecksumTest()
        {
            // Setup
            _testStreamShim.Seek(0x2D69, SeekOrigin.Begin);
            _testStreamShim.Write(new byte[2] { 0xbe, 0xef }, 0, 2);
            _testStreamShim.Seek(0, SeekOrigin.Begin);

            // Generate
            Assert.Throws<InvalidDataException>(() => _saveFileRepository.Deserialize(_testStreamShim));

        }

        [Fact]
        public void SerializeAndDeserializeSaveFileModalTest()
        {
            // Serilize model
            _saveFileRepository.Serialize(_testStreamShim, _expectedModel);

            // Deserialize Model
            _testStreamShim.Seek(0, SeekOrigin.Begin);
            var resultModel = _saveFileRepository.Deserialize(_testStreamShim);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Fact]
        public void SerializeSaveFileModalCorrectValuesTest()
        {
            // Generate
            var resultModel = _saveFileRepository.Deserialize(_testStreamShim);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Fact]
        public void SerializeSaveFileModalChecksumTest()
        {
            // Generate
            var resultModel = _saveFileRepository.Deserialize(_testStreamShim);

            // Assert some values
            Assert.Equal(_expectedModel.Checksum1, resultModel.Checksum1);
        }
    }
}