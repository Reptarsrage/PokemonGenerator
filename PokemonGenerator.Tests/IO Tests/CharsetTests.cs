using Xunit;
using PokemonGenerator.IO;
using System;

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
        [Trait("Category","Unit")]
        public void EncodeStringBasicTest()
        {
            var result = _charset.EncodeString("Test", 4);
            var expected = new byte[] { 0x93, 0xA4, 0xB2, 0xB3 };
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category","Unit")]
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
        [Trait("Category","Unit")]
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
        [Trait("Category","Unit")]
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
        [Trait("Category","Unit")]
        public void EncodeStringNullEndingTest()
        {
            var result = _charset.EncodeString("Test", 8);
            var expected = new byte[] { 0x93, 0xA4, 0xB2, 0xB3, 0x50, 0x50, 0x50, 0x50 };
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category","Unit")]
        public void DecodeStringBasicTest()
        {
            var encoded = new byte[] { 0x93, 0xA4, 0xB2, 0xB3 };
            var expected = "Test";
            var result = _charset.DecodeString(encoded);
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category","Unit")]
        public void DecodeStringNullEndingTest()
        {
            var encoded = new byte[] { 0x93, 0xA4, 0xB2, 0xB3, 0x50, 0x50, 0x50, 0x50 };
            var expected = "Test";
            var result = _charset.DecodeString(encoded);
            Assert.Equal(expected, result);
        }

        [Fact]
        [Trait("Category","Unit")]
        public void DecodeStringNullArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.DecodeString(null));
        }

        [Fact]
        [Trait("Category","Unit")]
        public void DecodeStringEmptyArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.DecodeString(new byte[] { }));
        }

        [Fact]
        [Trait("Category","Unit")]
        public void EncodeStringNullArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString(null, 10));
        }

        [Fact]
        [Trait("Category","Unit")]
        public void EncodeStringEmptyArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString("", 10));
        }

        [Fact]
        [Trait("Category","Unit")]
        public void EncodeStringZeroLengthArgumentExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => _charset.EncodeString("Test", 0));
        }
    }
}
