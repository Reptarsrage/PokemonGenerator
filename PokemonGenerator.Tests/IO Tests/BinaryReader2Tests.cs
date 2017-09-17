using Moq;
using NUnit.Framework;
using PokemonGenerator.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PokemonGenerator.Tests.IO_Tests
{
    [TestFixture]
    public class BinaryReader2Tests
    {
        private readonly IBinaryReader2 _breader;
        private readonly Mock<ICharset> _charsetMock;
        private MemoryStream _testStream;

        public BinaryReader2Tests()
        {
            _breader = new BinaryReader2();
            _charsetMock = new Mock<ICharset>();
            _charsetMock.Setup(c => c.DecodeString(It.IsNotNull<byte[]>())).Returns<byte[]>(b => Encoding.ASCII.GetString(b));
            _charsetMock.Setup(c => c.EncodeString(It.IsNotNull<string>(), It.Is<int>(i => i >= 0))).Returns<string, int>((s, i) => Encoding.ASCII.GetBytes(PadString(s, i)));

        }

        [SetUp]
        public void SetUp()
        {
            _testStream = new MemoryStream();
        }

        [TearDown]
        public void TearDown()
        {
            _testStream.Close();
            _testStream.Dispose();
        }

        [Test]
        [Category("Unit")]
        public void OpenStreamTest()
        {
            _breader.Open(_testStream);
            Assert.True(_testStream.CanRead);
        }

        [Test]
        [Category("Unit")]
        public void OpenStreamDisposedOldTest()
        {
            using (var tempStream = new MemoryStream())
            {
                _breader.Open(_testStream);
                Assert.True(_testStream.CanRead);
                _breader.Open(tempStream);
                Assert.False(_testStream.CanRead);
            }
        }

        [Test]
        [Category("Unit")]
        public void CloseStreamTest()
        {
            _breader.Open(_testStream);
            _breader.Close();
            Assert.False(_testStream.CanRead);
        }

        [Test]
        [Category("Unit")]
        public void CloseStreamMultipleTest()
        {
            _breader.Open(_testStream);
            _breader.Close();
            _breader.Close();
            _breader.Close();
            _breader.Close();
            Assert.False(_testStream.CanRead);
        }

        [Test]
        [Category("Unit")]
        [TestCase((ushort)99)]
        [TestCase((ushort)1)]
        [TestCase((ushort)5003)]
        [TestCase(UInt16.MaxValue)]
        [TestCase(UInt16.MinValue)]
        public void ReadUInt16Test(ushort val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(ushort));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt16();

            // Assert
            Assert.AreEqual(val, result);
        }

        [Test]
        [Category("Unit")]
        [TestCase((uint)99)]
        [TestCase((uint)1)]
        [TestCase((uint)500003)]
        [TestCase((uint)16777215)]
        [TestCase((uint)0)]
        public void ReadUInt24Test(uint val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val).Take(3).ToArray(), 0, 3);

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt24();

            // Assert
            Assert.AreEqual(val, result);
        }

        [Test]
        [Category("Unit")]
        [TestCase((uint)99)]
        [TestCase((uint)1)]
        [TestCase((uint)500003)]
        [TestCase(UInt32.MaxValue)]
        [TestCase(UInt32.MinValue)]
        public void ReadUInt32Test(uint val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(uint));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt32();

            // Assert
            Assert.AreEqual(val, result);
        }

        [Test]
        [Category("Unit")]
        [TestCase((ulong)99)]
        [TestCase((ulong)1)]
        [TestCase((ulong)5000000003)]
        [TestCase(UInt64.MaxValue)]
        [TestCase(UInt64.MinValue)]
        public void ReadUInt64Test(ulong val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(ulong));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt64();

            // Assert
            Assert.AreEqual(val, result);
        }

        [Test]
        [Category("Unit")]
        [TestCase("Test", 4)]
        [TestCase("", 0)]
        [TestCase("T", 1)]
        [TestCase("Test", 11)]
        public void ReadStringTest(string test, int length)
        {
            // Write
            var s = PadString(test, length);
            _testStream.Write(_charsetMock.Object.EncodeString(s, length), 0,length);

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadString(length, _charsetMock.Object);

            // Assert
            Assert.AreEqual(s, result);
        }

        private void WriteAsBigEndian(byte[] buffer, int offset, int length)
        {
            _testStream.Seek(0, SeekOrigin.Begin);
            _testStream.Write(buffer.Cast<byte>().Reverse().ToArray(), offset, length);
        }

        private string PadString(string s, int i)
        {
            return s.PadRight(i, '`');
        }
    }
}