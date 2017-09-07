using Moq;
using NUnit.Framework;
using PokemonGenerator.IO;
using System.IO;
using System.Reflection;

namespace PokemonGenerator.Tests.IO_Tests
{
    [TestFixture]
    public class PokeDeserializerTests : SerializerTestsBase
    {
        private IPokeDeserializer _deserializer;
        private IPokeSerializer _serializer;
        private Mock<Charset> _charsetMock;
        private Mock<BinaryReader2> _breaderMock;
        private Mock<BinaryWriter2> _bwriterMock;
        private Stream _testStream;

        [SetUp]
        public void SetUp()
        {
            _bwriterMock = new Mock<BinaryWriter2>
            {
                CallBase = true
            };
            _bwriterMock.Setup(b => b.Close());

            _breaderMock = new Mock<BinaryReader2>
            {
                CallBase = true
            };
            _breaderMock.Setup(b => b.Close());

            _charsetMock = new Mock<Charset>
            {
                CallBase = true
            };
        }

        [TearDown]
        public void CleanUp()
        {
            _testStream.Close();
            _testStream.Dispose();

            _deserializer = null;
            _serializer = null;
            _bwriterMock = null;
            _breaderMock = null;
            _charsetMock = null;
        }

        [Test]
        [Category("Integration")]
        public void SerializeSAVFileModalHasCorrectLengthTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Gold.sav"));

            // Run
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert
            Assert.NotNull(resultModel);
            Assert.AreEqual(6, resultModel.TeamPokemonList.Count);
        }

        [Test]
        [Category("Integration")]
        public void SerializeSAVFileModalBadChecksumTest()
        {
            // Setup
            _testStream = new MemoryStream(File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Gold.sav")));
            _testStream.Seek(0x2D69, SeekOrigin.Begin);
            _testStream.Write(new byte[2] { 0xbe, 0xef }, 0, 2);

            // Run
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            Assert.Throws<InvalidDataException>(() => _deserializer.ParseSAVFileModel(_testStream));
        }

        [Test]
        [Category("Integration")]
        public void SerializeAndDeserializeSAVFileModalTest()
        {
            // generate a model
            var model = BuildTestModel();

            // Serilize model
            _testStream = new MemoryStream();
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Deserialize Model
            _testStream.Seek(0, SeekOrigin.Begin);
            _breaderMock.Setup(b => b.Open(It.IsAny<Stream>())); // necessary to avoid closing stream
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(model, resultModel);
        }

        [Test]
        [Category("Integration")]
        public void SerializeSAVFileModalCorrectValuesTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Gold.sav"));
            var _expectedModel = BuildTestModel();

            // Run
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            AssertSavModelEqualityThorough(_expectedModel, resultModel);
        }

        [Test]
        [Category("Integration")]
        public void SerializeSAVFileModalChecksumTest()
        {
            // Setup
            _testStream = File.OpenRead(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Gold.sav"));
            var _expectedModel = BuildTestModel();

            // Run
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            Assert.AreEqual(_expectedModel.Checksum1, resultModel.Checksum1, "Checksum1");
        }
    }
}