using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PokemonGenerator.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PokemonGenerator.Tests.IO_Tests
{
    [TestFixture]
    public class BinaryWriter2Tests
    {
        private readonly IBinaryWriter2 _bwriter;
        private readonly Mock<ICharset> _charsetMock;
        private MemoryStream _testStream;

        public BinaryWriter2Tests()
        {
            _bwriter = new BinaryWriter2();
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
            _bwriter.Open(_testStream);
            Assert.True(_testStream.CanWrite);
        }

        [Test]
        [Category("Unit")]
        public void OpenStreamDisposedOldTest()
        {
            using (var tempStream = new MemoryStream())
            {
                _bwriter.Open(_testStream);
                Assert.True(_testStream.CanWrite);
                _bwriter.Open(tempStream);
                Assert.False(_testStream.CanWrite);
            }
        }

        [Test]
        [Category("Unit")]
        public void CloseStreamTest()
        {
            _bwriter.Open(_testStream);
            _bwriter.Close();
            Assert.False(_testStream.CanWrite);
        }

        [Test]
        [Category("Unit")]
        public void CloseStreamMultipleTest()
        {
            _bwriter.Open(_testStream);
            _bwriter.Close();
            _bwriter.Close();
            _bwriter.Close();
            _bwriter.Close();
            Assert.False(_testStream.CanWrite);
        }

        [Test]
        [Category("Unit")]
        [TestCase((ushort)99)]
        [TestCase((ushort)1)]
        [TestCase((ushort)5003)]
        [TestCase(UInt16.MaxValue)]
        [TestCase(UInt16.MinValue)]
        public void WriteUInt16Test(ushort val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt16(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(ushort));
            var result = BitConverter.ToUInt16(bytes, 0);

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
        public void WriteUInt24Test(uint val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt24(val);

            // Read
            var bytes = ReadAsBigEndian(0, 3);
            var paddedbytes = new byte[4] { bytes[0], bytes[1], bytes[2], 0x00 };
            var result = BitConverter.ToUInt32(paddedbytes, 0);

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
        public void WriteUInt32Test(uint val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt32(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(uint));
            var result = BitConverter.ToUInt32(bytes, 0);

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
        public void WriteUInt64Test(ulong val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt64(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(ulong));
            var result = BitConverter.ToUInt64(bytes, 0);

            // Assert
            Assert.AreEqual(val, result);
        }

        [Test]
        [Category("Unit")]
        [TestCase("Test", 4)]
        [TestCase("", 0)]
        [TestCase("T", 1)]
        [TestCase("Test", 11)]
        public void WriteStringTest(string test, int length)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteString(test, length, _charsetMock.Object);

            // Read
            var buffer = new byte[length];
            _testStream.Seek(0, SeekOrigin.Begin);
            _testStream.Read(buffer, 0, length);
            var result = _charsetMock.Object.DecodeString(buffer);

            // Assert
            Assert.AreEqual(PadString(test, length), result);
        }

        private byte[] ReadAsBigEndian(int offset, int length)
        {
            var buffer = new byte[length];
            _testStream.Seek(0, SeekOrigin.Begin);
            _testStream.Read(buffer, offset, length);
            return buffer.Cast<byte>().Reverse().ToArray();
        }

        private string PadString(string s, int i)
        {
            return s.PadRight(i, '`');
        }
    }
}