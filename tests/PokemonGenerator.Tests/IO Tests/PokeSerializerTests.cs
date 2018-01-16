using Moq;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace PokemonGenerator.Tests.IO_Tests
{
    public class PokeSerializerTests : SerializerTestsBase, IDisposable
    {
        private IPokeSerializer _serializer;
        private Mock<ICharset> _charsetMock;
        private Mock<IBinaryReader2> _breaderMock;
        private Mock<IBinaryWriter2> _bwriterMock;
        private MemoryStream _testStream;
        private BinaryWriter _testWriter;
        private BinaryReader _testReader;
        private int offset;
        private byte buffer;

        public PokeSerializerTests()
        {
            _charsetMock = new Mock<ICharset>(MockBehavior.Strict);
            _breaderMock = new Mock<IBinaryReader2>(MockBehavior.Strict);
            _bwriterMock = new Mock<IBinaryWriter2>(MockBehavior.Strict);
            _testStream = new MemoryStream();
            _testWriter = new BinaryWriter(_testStream);
            _testReader = new BinaryReader(_testStream);
        }

        public void Dispose()
        {
            _testReader.Dispose();
            _testWriter.Dispose();
            _testStream.Dispose();

            _serializer = null;
            _breaderMock = null;
            _bwriterMock = null;
            _charsetMock = null;
        }

        [Fact]
        public void SerializeSAVFileModalHasCorrectLengthTest()
        {
            var model = BuildTestModel();

            // Mock
            SetUpMocksForSerialization();

            // Run
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Assert
            _testReader.BaseStream.Seek(0, SeekOrigin.Begin);
            var result = _testReader.ReadBytes((int)_testStream.Length);

            Assert.NotNull(result);
            Assert.Equal(result.Length, 0x7E2E); // End of PC Box 14 Pokémon list

            // Verify
            _charsetMock.VerifyAll();
            _bwriterMock.VerifyAll();
            _breaderMock.VerifyAll();
        }

        [Fact]
        public void SerializeSAVFileModalDoesNotOverwriteBeginningTest()
        {
            var model = BuildTestModel();

            // Mock
            SetUpMocksForSerialization();

            // Write Some inital bytes to the beginning
            _testWriter.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < 0x2000 / sizeof(UInt32); i++)
            {
                _testWriter.Write(0xdeadbeef);
            }

            // Run
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Assert
            _testReader.BaseStream.Seek(0, SeekOrigin.Begin);
            var result = _testReader.ReadBytes(0x2000);

            // Read our inital bytes from the beginning
            _testReader.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < 0x2000 / sizeof(UInt32); i++)
            {
                Assert.Equal(0xdeadbeef, _testReader.ReadUInt32());
            }

            // Verify
            _charsetMock.VerifyAll();
            _bwriterMock.VerifyAll();
            _breaderMock.VerifyAll();
        }

        [Fact]
        public void SerializeSAVFileModalBasicValuesTest()
        {
            var model = BuildTestModel();

            // Mock
            SetUpMocksForSerialization();

            // Run
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Assert
            _testReader.BaseStream.Seek(0, SeekOrigin.Begin);
            var result = _testReader.ReadBytes(0x2000);

            // Read trainer name
            _testReader.BaseStream.Seek(0x200B, SeekOrigin.Begin);
            Assert.True(PadString(model.PlayerName, 11).Equals(Encoding.ASCII.GetString(_testReader.ReadBytes(11))), "Player Name");

            // Read rival name
            _testReader.BaseStream.Seek(0x2021, SeekOrigin.Begin);
            Assert.True(PadString(model.RivalName, 11).Equals(Encoding.ASCII.GetString(_testReader.ReadBytes(11))), "Rival Name");

            // Read Time played
            _testReader.BaseStream.Seek(0x2053, SeekOrigin.Begin);
            Assert.True(model.TimePlayed.Equals(_testReader.ReadUInt32()), "Time played");

            // Read Team Pokemon List Size
            _testReader.BaseStream.Seek(0x288A, SeekOrigin.Begin);
            Assert.True(6 == _testReader.ReadByte(), "Team Pokemon List Size");

            // Read Team Pokemon List Species
            var counter = 0;
            foreach (var pokemon in model.TeamPokemonList.Pokemon)
            {
                _testReader.BaseStream.Seek(0x2892 + counter * 0x30, SeekOrigin.Begin);
                Assert.True(pokemon.SpeciesId.Equals(_testReader.ReadByte()), $"Pokemon ({counter}) Species");
                counter++;
            }

            // Verify
            _charsetMock.VerifyAll();
            _bwriterMock.VerifyAll();
            _breaderMock.VerifyAll();
        }

        [Fact]
        public void SerializeSAVFileModalComplexValuesTest()
        {
            var model = BuildTestModel();

            // Mock
            SetUpMocksForSerialization();

            // Run
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Assert
            var counter = 0;
            foreach (var expectedPokemon in model.TeamPokemonList.Pokemon)
            {
                _testReader.BaseStream.Seek(0x2892 + counter * 0x30, SeekOrigin.Begin);

                byte buffer;
                var actualPokemon = new Pokemon
                {
                    SpeciesId = _testReader.ReadByte(),
                    HeldItem = _testReader.ReadByte(),
                    MoveIndex1 = _testReader.ReadByte(),
                    MoveIndex2 = _testReader.ReadByte(),
                    MoveIndex3 = _testReader.ReadByte(),
                    MoveIndex4 = _testReader.ReadByte(),
                    TrainerId = _testReader.ReadUInt16(),
                    Experience = BitConverter.ToUInt32(_testReader.ReadBytes(3).Cast<byte>().Concat(new byte[] { 0x00 }).ToArray(), 0),
                    HitPointsEV = _testReader.ReadUInt16(),
                    AttackEV = _testReader.ReadUInt16(),
                    DefenseEV = _testReader.ReadUInt16(),
                    SpeedEV = _testReader.ReadUInt16(),
                    SpecialEV = _testReader.ReadUInt16()
                };

                buffer = _testReader.ReadByte();
                actualPokemon.AttackIV = (byte)(buffer >> 4);
                actualPokemon.DefenseIV = (byte)(0xf & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.SpeedIV = (byte)(buffer >> 4);
                actualPokemon.SpecialIV = (byte)(0xf & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.Move1PowerPointsUps = (byte)(buffer >> 6);
                actualPokemon.Move1PowerPointsCurrent = (byte)(0x3f & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.Move2PowerPointsUps = (byte)(buffer >> 6);
                actualPokemon.Move2PowerPointsCurrent = (byte)(0x3f & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.Move3PowerPointsUps = (byte)(buffer >> 6);
                actualPokemon.Move3PowerPointsCurrent = (byte)(0x3f & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.Move4PowerPointsUps = (byte)(buffer >> 6);
                actualPokemon.Move4PowerPointsCurrent = (byte)(0x3f & buffer);

                actualPokemon.Friendship = _testReader.ReadByte();

                buffer = _testReader.ReadByte();
                actualPokemon.PokerusStrain = (byte)(buffer >> 4);
                actualPokemon.PokerusDuration = (byte)(0xf & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.CaughtTime = (byte)(buffer >> 6);
                actualPokemon.CaughtLevel = (byte)(0x3f & buffer);

                buffer = _testReader.ReadByte();
                actualPokemon.OTGender = (byte)(buffer >> 7);
                actualPokemon.CaughtLocation = (byte)(0x7f & buffer);

                actualPokemon.Level = _testReader.ReadByte();

                actualPokemon.Status = _testReader.ReadByte();
                actualPokemon.Unused = _testReader.ReadByte();
                actualPokemon.CurrentHp = _testReader.ReadUInt16();
                actualPokemon.MaxHp = _testReader.ReadUInt16();
                actualPokemon.Attack = _testReader.ReadUInt16();
                actualPokemon.Defense = _testReader.ReadUInt16();
                actualPokemon.Speed = _testReader.ReadUInt16();
                actualPokemon.SpAttack = _testReader.ReadUInt16();
                actualPokemon.SpDefense = _testReader.ReadUInt16();
                actualPokemon.OTName = expectedPokemon.OTName;
                actualPokemon.Name = expectedPokemon.Name;

                AssertPokemonEqualityThorough(expectedPokemon, actualPokemon);
                counter++;
            }

            // Verify
            _charsetMock.VerifyAll();
            _bwriterMock.VerifyAll();
            _breaderMock.VerifyAll();
        }

        [Fact]
        public void SerializeSAVFileModalCheckSumTest()
        {
            var model = BuildTestModel();

            // Mock
            SetUpMocksForSerialization();

            // Run
            _serializer = new PokeSerializer(_charsetMock.Object, _bwriterMock.Object, _breaderMock.Object);
            _serializer.SerializeSAVFileModal(_testStream, model);

            // Assert
            _testReader.BaseStream.Seek(0, SeekOrigin.Begin);
            var result = _testReader.ReadBytes(0x2000);

            // Calculate Checksum
            ushort checksum = 0;
            _testReader.BaseStream.Seek(0x2009, SeekOrigin.Begin);
            while (_testReader.BaseStream.Position <= 0x2D68)
            {
                checksum += _testReader.ReadByte();
            }

            // Read Checksum 1
            _testReader.BaseStream.Seek(0x2D69, SeekOrigin.Begin);
            Assert.True(checksum.Equals(_testReader.ReadUInt16()), "Checksum 1");

            // Verify
            _charsetMock.VerifyAll();
            _bwriterMock.VerifyAll();
            _breaderMock.VerifyAll();
        }

        private static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        private void SetUpMocksForSerialization()
        {
            // Mock Charset
            //_charsetMock.Setup(c => c.DecodeString(It.IsNotNull<byte[]>())).Returns<byte[]>(b => Encoding.ASCII.GetString(b));
            _charsetMock.Setup(c => c.EncodeString(It.IsNotNull<string>(), It.Is<int>(i => i >= 0))).Returns<string, int>((s, i) => Encoding.ASCII.GetBytes(PadString(s, i)));

            // Mock Writer
            _bwriterMock.Setup(b => b.Write(It.IsAny<byte>())).Callback<byte>(_testWriter.Write);
            _bwriterMock.Setup(b => b.WriteUInt16(It.IsAny<ushort>())).Callback<ushort>(_testWriter.Write);
            _bwriterMock.Setup(b => b.WriteInt16LittleEndian(It.IsAny<ushort>())).Callback<ushort>(_testWriter.Write);
            _bwriterMock.Setup(b => b.WriteUInt32(It.IsAny<uint>())).Callback<uint>(_testWriter.Write);
            _bwriterMock.Setup(b => b.WriteUInt24(It.IsAny<uint>())).Callback<uint>(i =>
            {
                var bytes = BitConverter.GetBytes(i);
                byte[] subBytes = new byte[3];
                Array.Copy(bytes, subBytes, 3);
                _testWriter.Write(subBytes);
            });
            _bwriterMock.Setup(b => b.WriteUInt64(It.IsAny<ulong>())).Callback<ulong>(_testWriter.Write);
            _bwriterMock.Setup(b => b.WriteString(It.IsNotNull<string>(), It.Is<int>(i => i >= 0), _charsetMock.Object))
                .Callback<string, int, ICharset>((s, i, c) => _testWriter.Write(c.EncodeString(s, i)));
            _bwriterMock.Setup(b => b.Seek(It.IsAny<long>(), It.IsAny<SeekOrigin>())).Callback<long, SeekOrigin>((l, o) => _testWriter.Seek((int)l, o));
            _bwriterMock.Setup(b => b.Fill(It.IsAny<byte>(), It.IsAny<int>())).Callback<byte, int>((v1, v2) =>
            {
                for (int i = 0; i < v2; i++)
                {
                    _testWriter.Write(v1);
                }
            });
            _bwriterMock.Setup(b => b.Open(_testStream));
            _bwriterMock.Setup(b => b.Close());

            // Mock Reader
            _breaderMock.Setup(b => b.Seek(It.IsAny<long>(), It.IsAny<SeekOrigin>())).Callback<long, SeekOrigin>((l, o) => _testReader.BaseStream.Seek((int)l, o));
            _breaderMock.SetupGet(b => b.Position).Returns(() => { return _testReader.BaseStream.Position; });
            _breaderMock.Setup(b => b.ReadByte()).Returns(() => _testReader.ReadByte());
            _breaderMock.Setup(b => b.Open(_testStream));
            _breaderMock.Setup(b => b.Close());
        }

        private void AddBitsToBuffer(BitArray bits, int length)
        {
            for (int i = 0; i < length; i++)
            {
                buffer |= (byte)((bits[i] ? 1 : 0) << offset++);

                if (offset == 8)
                {
                    _testWriter.Write(buffer);
                    buffer = 0x0;
                    offset = 0;
                }
            }
        }

        private string PadString(string s, int i)
        {
            return s.PadRight(i, '`');
        }
    }
}