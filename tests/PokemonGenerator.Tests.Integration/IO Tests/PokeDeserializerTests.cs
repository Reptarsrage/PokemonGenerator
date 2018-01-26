using PokemonGenerator.IO;
using System;
using System.IO;
using Xunit;

namespace PokemonGenerator.Tests.IO_Tests
{
    public class PokeDeserializerTests : SerializerTestsBase, IDisposable
    {
        private readonly string _testFile; 
        private IPokeDeserializer _deserializer;
        private IPokeSerializer _serializer;
        private Charset _charset;
        private BinaryReader2 _breader;
        private BinaryWriter2 _bwriter;
        private Stream _testStream;

        public PokeDeserializerTests()
        {
            _testFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav");
            _bwriter = new BinaryWriter2();
            _breader = new BinaryReader2();
            _charset = new Charset();
        }

        public void Dispose()
        {
            _testStream.Close();
            _testStream.Dispose();

            _deserializer = null;
            _serializer = null;
            _bwriter = null;
            _breader = null;
            _charset = null;
        }

        [Fact]
        public void SerializeSAVFileModalHasCorrectLengthTest()
        {
            // Setup
            _testStream = File.OpenRead(_testFile);

            // Run
            _deserializer = new PokeDeserializer(_breader, _charset);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert
            Assert.NotNull(resultModel);
            Assert.Equal(6, resultModel.TeamPokemonList.Count);
        }

        [Fact]
        public void SerializeSAVFileModalBadChecksumTest()
        {
            // Setup
            _testStream = new MemoryStream(File.ReadAllBytes(_testFile));
            _testStream.Seek(0x2D69, SeekOrigin.Begin);
            _testStream.Write(new byte[2] { 0xbe, 0xef }, 0, 2);

            // Run
            _deserializer = new PokeDeserializer(_breader, _charset);
            Assert.Throws<InvalidDataException>(() => _deserializer.ParseSAVFileModel(_testStream));
        }

        [Fact]
        public void SerializeAndDeserializeSAVFileModalTest()
        {
            // generate a model
            var model = BuildTestModel();

            // Serilize model
            _testStream = new FileStream(_testFile, FileMode.OpenOrCreate);
            _serializer = new PokeSerializer(_charset, _bwriter, _breader);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Deserialize Model
            _testStream = new FileStream(_testFile, FileMode.OpenOrCreate);
            _deserializer = new PokeDeserializer(_breader, _charset);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(model, resultModel);
        }

        [Fact]
        public void SerializeSAVFileModalCorrectValuesTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav"));
            var _expectedModel = BuildTestModel();

            // Run
            _deserializer = new PokeDeserializer(_breader, _charset);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Fact]
        public void SerializeSAVFileModalChecksumTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gold.sav"));
            var _expectedModel = BuildTestModel();

            // Run
            _deserializer = new PokeDeserializer(_breader, _charset);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            Assert.True(_expectedModel.Checksum1.Equals(resultModel.Checksum1), "Checksum1");
        }
    }
}