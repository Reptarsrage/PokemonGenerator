using Moq;
using NUnit.Framework;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System.IO;
using System.Linq;
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

            // Run
            _deserializer = new PokeDeserializer(_breaderMock.Object, _charsetMock.Object);
            var resultModel = _deserializer.ParseSAVFileModel(_testStream);

            // Assert some values
            Assert.AreEqual(_expectedModel.Checksum1, resultModel.Checksum1, "Checksum1");
        }

        private void AssertSavModelEqualityThorough(SAVFileModel expectedModel, SAVFileModel actualModel)
        {
            Assert.AreEqual(expectedModel.PlayerName, actualModel.PlayerName, "Player Name");
            Assert.AreEqual(expectedModel.RivalName, actualModel.RivalName, "Player Rival Name");
            Assert.AreEqual(expectedModel.TimePlayed, actualModel.TimePlayed, "Time Played");
            Assert.AreEqual(expectedModel.Money, actualModel.Money, "Money");
            Assert.AreEqual(expectedModel.JohtoBadges, actualModel.JohtoBadges, "Johto Badges");
            Assert.AreEqual(expectedModel.TMpocket.TMs.Length, actualModel.TMpocket.TMs.Length, "TMs");
            Assert.AreEqual(expectedModel.TMpocket.HMs.Length, actualModel.TMpocket.HMs.Length, "HMs");
            Assert.AreEqual(expectedModel.PocketItemList.Count, actualModel.PocketItemList.Count, "Pocket Item List");
            Assert.AreEqual(expectedModel.PocketKeyItemList.Count, actualModel.PocketKeyItemList.Count, "Pocket Key Item List");
            Assert.AreEqual(expectedModel.PocketBallItemList.Count, actualModel.PocketBallItemList.Count, "Pocket Ball Item List");
            Assert.AreEqual(expectedModel.PCItemList.Count, actualModel.PCItemList.Count, "PC Item List");
            Assert.AreEqual(expectedModel.CurrentPCBoxNumber, actualModel.CurrentPCBoxNumber, "Current PC Box Number");
            Assert.AreEqual(expectedModel.PCBoxNames.Length, actualModel.PCBoxNames.Length, "PC Box Names");
            Assert.AreEqual(expectedModel.TeamPokemonList.Count, actualModel.TeamPokemonList.Count, "Team Pokemon List");
            Assert.AreEqual(expectedModel.TeamPokemonList.OTNames.Length, actualModel.TeamPokemonList.OTNames.Length, "Team Pokemon List");
            Assert.AreEqual(expectedModel.TeamPokemonList.Names.Length, actualModel.TeamPokemonList.Names.Length, "Team Pokemon List");
            Assert.AreEqual(expectedModel.TeamPokemonList.Pokemon.Length, actualModel.TeamPokemonList.Pokemon.Length, "Team Pokemon List");
            Assert.AreEqual(expectedModel.PokedexOwned.Length, actualModel.PokedexOwned.Length, "Pokedex Owned");
            Assert.AreEqual(expectedModel.PokedexSeen.Length, actualModel.PokedexSeen.Length, "Pokedex Seen");
            Assert.AreEqual(expectedModel.CurrentBoxPokemonlist.Count, actualModel.CurrentBoxPokemonlist.Count, "Pokedex Seen");
            Assert.AreEqual(expectedModel.Boxes[0].Count, actualModel.Boxes[0].Count, "Box1 Count");
            Assert.AreEqual(expectedModel.Boxes[1].Count, actualModel.Boxes[1].Count, "Box2 Count");

            for (var i = 0; i < expectedModel.TeamPokemonList.Count; i++)
            {
                var pokemonExpected = expectedModel.TeamPokemonList.Pokemon[i];
                var pokemonActual = actualModel.TeamPokemonList.Pokemon[i];
                AssertPokemonEqualityThorough(pokemonExpected, pokemonActual);
            }

            for (var i = 0; i < expectedModel.PCBoxNames.Length; i++)
            {
                Assert.AreEqual(expectedModel.PCBoxNames[i], actualModel.PCBoxNames[i], $"PC BOX{i + 1} Name");
            }

            var test = actualModel.CurrentBoxPokemonlist.Pokemon.Cast<Pokemon>().Select(p => p.Species).ToList();
            for (var i = 0; i < expectedModel.CurrentBoxPokemonlist.Pokemon.Length; i++)
            {
                var pokemonExpected = expectedModel.CurrentBoxPokemonlist.Pokemon[i];
                var pokemonActual = actualModel.CurrentBoxPokemonlist.Pokemon[i];
                Assert.AreEqual(pokemonExpected.Species, pokemonActual.Species, "Pokemon Species");
            }
        }

        private void AssertPokemonEqualityThorough(Pokemon pokemonExpected, Pokemon pokemonActual)
        {
            Assert.AreEqual(pokemonExpected.Species, pokemonActual.Species, "Pokemon Species");
            Assert.AreEqual(pokemonExpected.heldItem, pokemonActual.heldItem, "Pokemon heldItem");
            Assert.AreEqual(pokemonExpected.moveIndex1, pokemonActual.moveIndex1, "Pokemon moveIndex1");
            Assert.AreEqual(pokemonExpected.moveIndex2, pokemonActual.moveIndex2, "Pokemon moveIndex2");
            Assert.AreEqual(pokemonExpected.moveIndex3, pokemonActual.moveIndex3, "Pokemon moveIndex3");
            Assert.AreEqual(pokemonExpected.moveIndex4, pokemonActual.moveIndex4, "Pokemon moveIndex4");
            Assert.AreEqual(pokemonExpected.trainerID, pokemonActual.trainerID, "Pokemon trainerID");
            Assert.AreEqual(pokemonExpected.experience, pokemonActual.experience, "Pokemon experience");
            Assert.AreEqual(pokemonExpected.hpEV, pokemonActual.hpEV, "Pokemon hpEV");
            Assert.AreEqual(pokemonExpected.attackEV, pokemonActual.attackEV, "Pokemon attackEV");
            Assert.AreEqual(pokemonExpected.defenseEV, pokemonActual.defenseEV, "Pokemon defenseEV");
            Assert.AreEqual(pokemonExpected.speedEV, pokemonActual.speedEV, "Pokemon speedEV");
            Assert.AreEqual(pokemonExpected.specialEV, pokemonActual.specialEV, "Pokemon specialEV");
            Assert.AreEqual(pokemonExpected.attackIV, pokemonActual.attackIV, "Pokemon attackIV");
            Assert.AreEqual(pokemonExpected.defenseIV, pokemonActual.defenseIV, "Pokemon defenseIV");
            Assert.AreEqual(pokemonExpected.speedIV, pokemonActual.speedIV, "Pokemon speedIV");
            Assert.AreEqual(pokemonExpected.specialIV, pokemonActual.specialIV, "Pokemon specialIV");
            Assert.AreEqual(pokemonExpected.ppUps1, pokemonActual.ppUps1, "Pokemon ppUps1");
            Assert.AreEqual(pokemonExpected.currentPP1, pokemonActual.currentPP1, "Pokemon currentPP1");
            Assert.AreEqual(pokemonExpected.ppUps2, pokemonActual.ppUps2, "Pokemon ppUps2");
            Assert.AreEqual(pokemonExpected.currentPP2, pokemonActual.currentPP2, "Pokemon currentPP2");
            Assert.AreEqual(pokemonExpected.ppUps3, pokemonActual.ppUps3, "Pokemon ppUps3");
            Assert.AreEqual(pokemonExpected.currentPP3, pokemonActual.currentPP3, "Pokemon currentPP3");
            Assert.AreEqual(pokemonExpected.ppUps4, pokemonActual.ppUps4, "Pokemon ppUps4");
            Assert.AreEqual(pokemonExpected.currentPP4, pokemonActual.currentPP4, "Pokemon currentPP4");
            Assert.AreEqual(pokemonExpected.friendship, pokemonActual.friendship, "Pokemon friendship");
            Assert.AreEqual(pokemonExpected.pokerusStrain, pokemonActual.pokerusStrain, "Pokemon pokerusStrain");
            Assert.AreEqual(pokemonExpected.pokerusDuration, pokemonActual.pokerusDuration, "Pokemon pokerusDuration");
            Assert.AreEqual(pokemonExpected.caughtTime, pokemonActual.caughtTime, "Pokemon caughtTime");
            Assert.AreEqual(pokemonExpected.caughtLevel, pokemonActual.caughtLevel, "Pokemon caughtLevel");
            Assert.AreEqual(pokemonExpected.OTGender, pokemonActual.OTGender, "Pokemon OTGender");
            Assert.AreEqual(pokemonExpected.caughtLocation, pokemonActual.caughtLocation, "Pokemon caughtLocation");
            Assert.AreEqual(pokemonExpected.level, pokemonActual.level, "Pokemon level");
            Assert.AreEqual(pokemonExpected.status, pokemonActual.status, "Pokemon status");
            Assert.AreEqual(pokemonExpected.unused, pokemonActual.unused, "Pokemon unused");
            Assert.AreEqual(pokemonExpected.currentHp, pokemonActual.currentHp, "Pokemon currentHp");
            Assert.AreEqual(pokemonExpected.maxHp, pokemonActual.maxHp, "Pokemon maxHp");
            Assert.AreEqual(pokemonExpected.attack, pokemonActual.attack, "Pokemon attack");
            Assert.AreEqual(pokemonExpected.defense, pokemonActual.defense, "Pokemon defense");
            Assert.AreEqual(pokemonExpected.speed, pokemonActual.speed, "Pokemon speed");
            Assert.AreEqual(pokemonExpected.spAttack, pokemonActual.spAttack, "Pokemon spAttack");
            Assert.AreEqual(pokemonExpected.spDefense, pokemonActual.spDefense, "Pokemon spDefense");
            Assert.AreEqual(pokemonExpected.OTName, pokemonActual.OTName, "Pokemon OTName");
            Assert.AreEqual(pokemonExpected.Name, pokemonActual.Name, "Pokemon Name");
        }

        private SAVFileModel _expectedModel = new SAVFileModel
        {
            PlayerName = "Justin",
            RivalName = "ASSBITE",
            TimePlayed = 10755348,
            Money = 20271,
            JohtoBadges = 6,
            TMpocket = new TMPocket
            {
                TMs = new byte[50],
                HMs = new byte[7]
            },
            PocketItemList = new ItemList(19)
            {
                Count = 19
            },
            PocketKeyItemList = new ItemList(6)
            {
                Count = 6
            },
            PocketBallItemList = new ItemList(3)
            {
                Count = 3
            },
            PCItemList = new ItemList(6)
            {
                Count = 6
            },
            CurrentPCBoxNumber = 0,
            PCBoxNames = new string[14] {
                "BOX1",
                "BOX2",
                "BOX3",
                "BOX4",
                "BOX5",
                "BOX6",
                "BOX7",
                "BOX8",
                "BOX9",
                "BOX10",
                "BOX11",
                "BOX12",
                "BOX13",
                "BOX14",
            },
            PokedexOwned = new bool[256],
            PokedexSeen = new bool[256],
            CurrentBoxPokemonlist = new PokeList(20)
            {
                Pokemon = new Pokemon[20]
                {
                    new Pokemon { Species = 180 },
                    new Pokemon { Species = 72 },
                    new Pokemon { Species = 128 },
                    new Pokemon { Species = 234 },
                    new Pokemon { Species = 132 },
                    new Pokemon { Species = 96 },
                    new Pokemon { Species = 27 },
                    new Pokemon { Species = 201 },
                    new Pokemon { Species = 179 },
                    new Pokemon { Species = 95 },
                    new Pokemon { Species = 175 },
                    new Pokemon { Species = 185 },
                    new Pokemon { Species = 75 },
                    new Pokemon { Species = 160 },
                    new Pokemon { Species = 133 },
                    new Pokemon { Species = 33 },
                    new Pokemon { Species = 64 },
                    new Pokemon { Species = 241 },
                    new Pokemon { Species = 82 },
                    new Pokemon { Species = 213 }
                }
            },
            Boxes = new PokeList[14]
            {
                new PokeList(20),
                new PokeList(3),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0),
                new PokeList(0)
            },
            Checksum1 = 4902,
            TeamPokemonList = new PokeList(6)
            {
                Pokemon = new Pokemon[6]
                {
                    new Pokemon
                    {
                        Species = 149,
                        heldItem = 80,
                        moveIndex1 = 87,
                        moveIndex2 = 126,
                        moveIndex3 = 8,
                        moveIndex4 = 95,
                        trainerID = 3370,
                        experience = 35000,
                        hpEV = 65535,
                        attackEV = 65535,
                        defenseEV = 65535,
                        speedEV = 65535,
                        specialEV = 65535,
                        attackIV = 15,
                        defenseIV = 15,
                        speedIV = 15,
                        specialIV = 15,
                        ppUps1 = 3,
                        currentPP1 = 16,
                        ppUps2 = 3,
                        currentPP2 = 8,
                        ppUps3 = 3,
                        currentPP3 = 24,
                        ppUps4 = 3,
                        currentPP4 = 32,
                        friendship = 255,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 2,
                        caughtLevel = 5,
                        OTGender = 0,
                        caughtLocation = 18,
                        level = 30,
                        status = 0,
                        unused = 0,
                        currentHp = 122,
                        maxHp = 122,
                        attack = 113,
                        defense = 90,
                        speed = 81,
                        spAttack = 93,
                        spDefense = 93,
                        OTName = "Justin",
                        Name = "DRAGONITE"
                    },
                    new Pokemon
                    {
                        Species = 83,
                        heldItem = 77,
                        moveIndex1 = 64,
                        moveIndex2 = 28,
                        moveIndex3 = 31,
                        moveIndex4 = 19,
                        trainerID = 3370,
                        experience = 10700,
                        hpEV = 3613,
                        attackEV = 2863,
                        defenseEV = 3311,
                        speedEV = 3451,
                        specialEV = 2477,
                        attackIV = 4,
                        defenseIV = 9,
                        speedIV = 5,
                        specialIV = 15,
                        ppUps1 = 0,
                        currentPP1 = 35,
                        ppUps2 = 0,
                        currentPP2 = 15,
                        ppUps3 = 0,
                        currentPP3 = 20,
                        ppUps4 = 0,
                        currentPP4 = 15,
                        friendship = 119,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 0,
                        caughtLevel = 0,
                        OTGender = 0,
                        caughtLocation = 0,
                        level = 22,
                        status = 0,
                        unused = 0,
                        currentHp = 61,
                        maxHp = 61,
                        attack = 38,
                        defense = 36,
                        speed = 36,
                        spAttack = 39,
                        spDefense = 41,
                        OTName = "Justin",
                        Name = "FARFETCH'D"
                    },
                    new Pokemon
                    {
                        Species = 58,
                        heldItem = 49,
                        moveIndex1 = 44,
                        moveIndex2 = 36,
                        moveIndex3 = 52,
                        moveIndex4 = 43,
                        trainerID = 3370,
                        experience = 24580,
                        hpEV = 4316,
                        attackEV = 4799,
                        defenseEV = 4535,
                        speedEV = 4538,
                        specialEV = 3616,
                        attackIV = 0,
                        defenseIV = 8,
                        speedIV = 5,
                        specialIV = 14,
                        ppUps1 = 0,
                        currentPP1 = 25,
                        ppUps2 = 0,
                        currentPP2 = 20,
                        ppUps3 = 0,
                        currentPP3 = 25,
                        ppUps4 = 0,
                        currentPP4 = 30,
                        friendship = 142,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 0,
                        caughtLevel = 0,
                        OTGender = 0,
                        caughtLocation = 0,
                        level = 26,
                        status = 0,
                        unused = 0,
                        currentHp = 69,
                        maxHp = 69,
                        attack = 45,
                        defense = 36,
                        speed = 43,
                        spAttack = 52,
                        spDefense = 42,
                        OTName = "Justin",
                        Name = "GROWLITH",
                    },
                    new Pokemon
                    {
                        Species = 147,
                        heldItem = 0,
                        moveIndex1 = 82,
                        moveIndex2 = 57,
                        moveIndex3 = 86,
                        moveIndex4 = 239,
                        trainerID = 3370,
                        experience = 19953,
                        hpEV = 6146,
                        attackEV = 7681,
                        defenseEV = 6816,
                        speedEV = 7558,
                        specialEV = 5838,
                        attackIV = 6,
                        defenseIV = 9,
                        speedIV = 15,
                        specialIV = 4,
                        ppUps1 = 0,
                        currentPP1 = 10,
                        ppUps2 = 0,
                        currentPP2 = 15,
                        ppUps3 = 0,
                        currentPP3 = 20,
                        ppUps4 = 0,
                        currentPP4 = 20,
                        friendship = 138,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 0,
                        caughtLevel = 0,
                        OTGender = 0,
                        caughtLocation = 0,
                        level = 25,
                        status = 0,
                        unused = 0,
                        currentHp = 63,
                        maxHp = 63,
                        attack = 45,
                        defense = 37,
                        speed = 42,
                        spAttack = 36,
                        spDefense = 36,
                        OTName = "Justin",
                        Name = "DRATINI",
                    },
                    new Pokemon
                    {
                        Species = 100,
                        heldItem = 0,
                        moveIndex1 = 33,
                        moveIndex2 = 103,
                        moveIndex3 = 49,
                        moveIndex4 = 120,
                        trainerID = 3370,
                        experience = 12651,
                        hpEV = 112,
                        attackEV = 146,
                        defenseEV = 95,
                        speedEV = 129,
                        specialEV = 136,
                        attackIV = 12,
                        defenseIV = 6,
                        speedIV = 3,
                        specialIV = 2,
                        ppUps1 = 0,
                        currentPP1 = 35,
                        ppUps2 = 0,
                        currentPP2 = 40,
                        ppUps3 = 0,
                        currentPP3 = 20,
                        ppUps4 = 0,
                        currentPP4 = 5,
                        friendship = 86,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 0,
                        caughtLevel = 0,
                        OTGender = 0,
                        caughtLocation = 0,
                        level = 23,
                        status = 0,
                        unused = 0,
                        currentHp = 52,
                        maxHp = 52,
                        attack = 25,
                        defense = 31,
                        speed = 53,
                        spAttack = 31,
                        spDefense = 31,
                        OTName = "Justin",
                        Name = "VOLTORB",
                    },
                    new Pokemon
                    {
                        Species = 30,
                        heldItem = 0,
                        moveIndex1 = 10,
                        moveIndex2 = 33,
                        moveIndex3 = 40,
                        moveIndex4 = 24,
                        trainerID = 3370,
                        experience = 7896,
                        hpEV = 4028,
                        attackEV = 4394,
                        defenseEV = 4143,
                        speedEV = 4293,
                        specialEV = 3202,
                        attackIV = 5,
                        defenseIV = 15,
                        speedIV = 8,
                        specialIV = 12,
                        ppUps1 = 0,
                        currentPP1 = 35,
                        ppUps2 = 0,
                        currentPP2 = 35,
                        ppUps3 = 0,
                        currentPP3 = 35,
                        ppUps4 = 0,
                        currentPP4 = 30,
                        friendship = 135,
                        pokerusStrain = 0,
                        pokerusDuration = 0,
                        caughtTime = 0,
                        caughtLevel = 0,
                        OTGender = 0,
                        caughtLocation = 0,
                        level = 22,
                        status = 0,
                        unused = 0,
                        currentHp = 71,
                        maxHp = 71,
                        attack = 38,
                        defense = 44,
                        speed = 36,
                        spAttack = 37,
                        spDefense = 37,
                        OTName = "Justin",
                        Name = "NIDORINA",
                    }
                }
            }
        };
    }
}