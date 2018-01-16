using Moq;
using PokemonGenerator.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace PokemonGenerator.Tests.IO_Tests
{
    public class BinaryWriter2Tests : IDisposable
    {
        private readonly IBinaryWriter2 _bwriter;
        private readonly Mock<ICharset> _charsetMock;
        private MemoryStream _testStream;

        public BinaryWriter2Tests()
        {
            _testStream = new MemoryStream();
            _bwriter = new BinaryWriter2();
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
            _bwriter.Open(_testStream);
            Assert.True(_testStream.CanWrite);
        }

        [Fact]
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

        [Fact]
        public void CloseStreamTest()
        {
            _bwriter.Open(_testStream);
            _bwriter.Close();
            Assert.False(_testStream.CanWrite);
        }

        [Fact]
        public void CloseStreamMultipleTest()
        {
            _bwriter.Open(_testStream);
            _bwriter.Close();
            _bwriter.Close();
            _bwriter.Close();
            _bwriter.Close();
            Assert.False(_testStream.CanWrite);
        }

        [Theory]
        [InlineData((ushort)99)]
        [InlineData((ushort)1)]
        [InlineData((ushort)5003)]
        [InlineData(UInt16.MaxValue)]
        [InlineData(UInt16.MinValue)]
        public void WriteUInt16Test(ushort val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt16(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(ushort));
            var result = BitConverter.ToUInt16(bytes, 0);

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((uint)99)]
        [InlineData((uint)1)]
        [InlineData((uint)500003)]
        [InlineData((uint)16777215)]
        [InlineData((uint)0)]
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
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((uint)99)]
        [InlineData((uint)1)]
        [InlineData((uint)500003)]
        [InlineData(UInt32.MaxValue)]
        [InlineData(UInt32.MinValue)]
        public void WriteUInt32Test(uint val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt32(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(uint));
            var result = BitConverter.ToUInt32(bytes, 0);

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData((ulong)99)]
        [InlineData((ulong)1)]
        [InlineData((ulong)5000000003)]
        [InlineData(UInt64.MaxValue)]
        [InlineData(UInt64.MinValue)]
        public void WriteUInt64Test(ulong val)
        {
            // Write
            _bwriter.Open(_testStream);
            _bwriter.WriteUInt64(val);

            // Read
            var bytes = ReadAsBigEndian(0, sizeof(ulong));
            var result = BitConverter.ToUInt64(bytes, 0);

            // Assert
            Assert.Equal(val, result);
        }

        [Theory]
        [InlineData("Test", 4)]
        [InlineData("", 0)]
        [InlineData("T", 1)]
        [InlineData("Test", 11)]
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
            Assert.Equal(PadString(test, length), result);
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