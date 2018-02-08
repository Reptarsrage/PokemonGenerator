using System;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using PokemonGenerator.IO;
using Xunit;

namespace PokemonGenerator.Tests.Unit.IO_Tests
{
    public class BinaryReader2Tests : IDisposable
    {
        private readonly IBinaryReader2 _breader;
        private readonly Mock<ICharset> _charsetMock;
        private MemoryStream _testStream;

        public BinaryReader2Tests()
        {
            _testStream = new MemoryStream();
            _breader = new BinaryReader2();
            _charsetMock = new Mock<ICharset>();
            _charsetMock.Setup(c => c.DecodeString(It.IsNotNull<byte[]>())).Returns<byte[]>(b => Encoding.ASCII.GetString(b));
            _charsetMock.Setup(c => c.EncodeString(It.IsNotNull<string>(), It.Is<int>(i => i >= 0))).Returns<string, int>((s, i) => Encoding.ASCII.GetBytes(PadString(s, i)));

        }

        public void Dispose()
        {
            _testStream.Close();
            _testStream.Dispose();
        }

        [Fact]
        public void OpenStreamTest()
        {
            _breader.Open(_testStream);
            Assert.True(_testStream.CanRead);
        }

        [Fact]
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

        [Fact]
        public void CloseStreamTest()
        {
            _breader.Open(_testStream);
            _breader.Close();
            Assert.False(_testStream.CanRead);
        }

        [Fact]
        public void CloseStreamMultipleTest()
        {
            _breader.Open(_testStream);
            _breader.Close();
            _breader.Close();
            _breader.Close();
            _breader.Close();
            Assert.False(_testStream.CanRead);
        }

        [Theory]
        [InlineData((ushort)99)]
        [InlineData((ushort)1)]
        [InlineData((ushort)5003)]
        [InlineData(UInt16.MaxValue)]
        [InlineData(UInt16.MinValue)]
        public void ReadUInt16Test(ushort val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(ushort));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt16();

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((uint)99)]
        [InlineData((uint)1)]
        [InlineData((uint)500003)]
        [InlineData((uint)16777215)]
        [InlineData((uint)0)]
        public void ReadUInt24Test(uint val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val).Take(3).ToArray(), 0, 3);

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt24();

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((uint)99)]
        [InlineData((uint)1)]
        [InlineData((uint)500003)]
        [InlineData(UInt32.MaxValue)]
        [InlineData(UInt32.MinValue)]
        public void ReadUInt32Test(uint val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(uint));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt32();

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((ulong)99)]
        [InlineData((ulong)1)]
        [InlineData((ulong)5000000003)]
        [InlineData(UInt64.MaxValue)]
        [InlineData(UInt64.MinValue)]
        public void ReadUInt64Test(ulong val)
        {
            // Write
            WriteAsBigEndian(BitConverter.GetBytes(val), 0, sizeof(ulong));

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadUInt64();

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData("Test", 4)]
        [InlineData("", 0)]
        [InlineData("T", 1)]
        [InlineData("Test", 11)]
        public void ReadStringTest(string test, int length)
        {
            // Write
            var s = PadString(test, length);
            _testStream.Write(_charsetMock.Object.EncodeString(s, length), 0, length);

            // Read
            _testStream.Seek(0, SeekOrigin.Begin);
            _breader.Open(_testStream);
            var result = _breader.ReadString(length, _charsetMock.Object);

            // Assert
            Assert.Equal(s, result);
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