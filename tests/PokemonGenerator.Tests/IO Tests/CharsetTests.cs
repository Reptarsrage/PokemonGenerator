using PokemonGenerator.IO;
using System;
using Xunit;

namespace PokemonGenerator.Tests.IO_Tests
{
    public class CharsetTests
    {
        private readonly ICharset _charset;

        public CharsetTests()
        {
            _charset = new Charset();
        }

        [Fact]
        public void EncodeStringBasicTest()
        {
            var result = _charset.EncodeString("Test", 4);
            var expected = new byte[] { 0x93, 0xA4, 0xB2, 0xB3 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void EncodeStringAlphaUpperCaseTest()
        {
            var result = _charset.EncodeString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 26);
            var expected = new byte[26];
            for (byte b = 0x80, i = 0; i < 26; i++, b++)
            {
                expected[i] = b;
            }
            Assert.Equal(expected, result);
        }

        [Fact]
        public void EncodeStringAlphaLowerCaseTest()
        {
            var result = _charset.EncodeString("abcdefghijklmnopqrstuvwxyz", 26);
            var expected = new byte[26];
            for (byte b = 0xA0, i = 0; i < 26; i++, b++)
            {
                expected[i] = b;
            }
            Assert.Equal(expected, result);
        }


        [Fact]
        public void EncodeStringNumericTest()
        {
            var result = _charset.EncodeString("0123456789", 10);
            var expected = new byte[10];
            for (byte b = 0xF6, i = 0; i < 10; i++, b++)
            {
                expected[i] = b;
            }
            Assert.Equal(expected, result);
        }

        [Fact]
        public void EncodeStringNullEndingTest()
        {
            var result = _charset.EncodeString("Test", 8);
            var expected = new byte[] { 0x93, 0xA4, 0xB2, 0xB3, 0x50, 0x50, 0x50, 0x50 };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeStringBasicTest()
        {
            var encoded = new byte[] { 0x93, 0xA4, 0xB2, 0xB3 };
            var expected = "Test";
            var result = _charset.DecodeString(encoded);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeStringNullEndingTest()
        {
            var encoded = new byte[] { 0x93, 0xA4, 0xB2, 0xB3, 0x50, 0x50, 0x50, 0x50 };
            var expected = "Test";
            var result = _charset.DecodeString(encoded);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DecodeStringNullArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.DecodeString(null));
        }

        [Fact]
        public void DecodeStringEmptyArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.DecodeString(new byte[] { }));
        }

        [Fact]
        public void EncodeStringNullArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString(null, 10));
        }

        [Fact]
        public void EncodeStringEmptyArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString("", 10));
        }

        [Fact]
        public void EncodeStringZeroLengthArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString("Test", 0));
        }
    }
}
