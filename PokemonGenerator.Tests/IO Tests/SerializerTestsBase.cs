using NUnit.Framework;
using PokemonGenerator.Models;
using System.Linq;

namespace PokemonGenerator.Tests.IO_Tests
{
    public abstract class SerializerTestsBase
    {
        protected virtual SAVFileModel BuildTestModel()
        {
            var model = new SAVFileModel
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
                PocketItemList = new ItemList(19),
                PocketKeyItemList = new ItemList(6),
                PocketBallItemList = new ItemList(3),
                PCItemList = new ItemList(6),
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
                    new Pokemon { SpeciesId = 180, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 72, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 128, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 234, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 132, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 96, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 27, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 201, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 179, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 95, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 175, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 185, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 75, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 160, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 133, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 33, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 64, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 241, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 82, OTName = "Empty", Name = "Empty" },
                    new Pokemon { SpeciesId = 213, OTName = "Empty", Name = "Empty" }
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
                        SpeciesId = 149,
                        HeldItem = 80,
                        MoveIndex1 = 87,
                        MoveIndex2 = 126,
                        MoveIndex3 = 8,
                        MoveIndex4 = 95,
                        TrainerId = 3370,
                        Experience = 35000,
                        HitPointsEV = 65535,
                        AttackEV = 65535,
                        DefenseEV = 65535,
                        SpeedEV = 65535,
                        SpecialEV = 65535,
                        AttackIV = 15,
                        DefenseIV = 15,
                        SpeedIV = 15,
                        SpecialIV = 15,
                        Move1PowerPointsUps = 3,
                        Move1PowerPointsCurrent = 16,
                        Move2PowerPointsUps = 3,
                        Move2PowerPointsCurrent = 8,
                        Move3PowerPointsUps = 3,
                        Move3PowerPointsCurrent = 24,
                        Move4PowerPointsUps = 3,
                        Move4PowerPointsCurrent = 32,
                        Friendship = 255,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 2,
                        CaughtLevel = 5,
                        OTGender = 0,
                        CaughtLocation = 18,
                        Level = 30,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 122,
                        MaxHp = 122,
                        Attack = 113,
                        Defense = 90,
                        Speed = 81,
                        SpAttack = 93,
                        SpDefense = 93,
                        OTName = "Justin",
                        Name = "DRAGONITE"
                    },
                    new Pokemon
                    {
                        SpeciesId = 83,
                        HeldItem = 77,
                        MoveIndex1 = 64,
                        MoveIndex2 = 28,
                        MoveIndex3 = 31,
                        MoveIndex4 = 19,
                        TrainerId = 3370,
                        Experience = 10700,
                        HitPointsEV = 3613,
                        AttackEV = 2863,
                        DefenseEV = 3311,
                        SpeedEV = 3451,
                        SpecialEV = 2477,
                        AttackIV = 4,
                        DefenseIV = 9,
                        SpeedIV = 5,
                        SpecialIV = 15,
                        Move1PowerPointsUps = 0,
                        Move1PowerPointsCurrent = 35,
                        Move2PowerPointsUps = 0,
                        Move2PowerPointsCurrent = 15,
                        Move3PowerPointsUps = 0,
                        Move3PowerPointsCurrent = 20,
                        Move4PowerPointsUps = 0,
                        Move4PowerPointsCurrent = 15,
                        Friendship = 119,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 0,
                        CaughtLevel = 0,
                        OTGender = 0,
                        CaughtLocation = 0,
                        Level = 22,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 61,
                        MaxHp = 61,
                        Attack = 38,
                        Defense = 36,
                        Speed = 36,
                        SpAttack = 39,
                        SpDefense = 41,
                        OTName = "Justin",
                        Name = "FARFETCH'D"
                    },
                    new Pokemon
                    {
                        SpeciesId = 58,
                        HeldItem = 49,
                        MoveIndex1 = 44,
                        MoveIndex2 = 36,
                        MoveIndex3 = 52,
                        MoveIndex4 = 43,
                        TrainerId = 3370,
                        Experience = 24580,
                        HitPointsEV = 4316,
                        AttackEV = 4799,
                        DefenseEV = 4535,
                        SpeedEV = 4538,
                        SpecialEV = 3616,
                        AttackIV = 0,
                        DefenseIV = 8,
                        SpeedIV = 5,
                        SpecialIV = 14,
                        Move1PowerPointsUps = 0,
                        Move1PowerPointsCurrent = 25,
                        Move2PowerPointsUps = 0,
                        Move2PowerPointsCurrent = 20,
                        Move3PowerPointsUps = 0,
                        Move3PowerPointsCurrent = 25,
                        Move4PowerPointsUps = 0,
                        Move4PowerPointsCurrent = 30,
                        Friendship = 142,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 0,
                        CaughtLevel = 0,
                        OTGender = 0,
                        CaughtLocation = 0,
                        Level = 26,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 69,
                        MaxHp = 69,
                        Attack = 45,
                        Defense = 36,
                        Speed = 43,
                        SpAttack = 52,
                        SpDefense = 42,
                        OTName = "Justin",
                        Name = "GROWLITH",
                    },
                    new Pokemon
                    {
                        SpeciesId = 147,
                        HeldItem = 0,
                        MoveIndex1 = 82,
                        MoveIndex2 = 57,
                        MoveIndex3 = 86,
                        MoveIndex4 = 239,
                        TrainerId = 3370,
                        Experience = 19953,
                        HitPointsEV = 6146,
                        AttackEV = 7681,
                        DefenseEV = 6816,
                        SpeedEV = 7558,
                        SpecialEV = 5838,
                        AttackIV = 6,
                        DefenseIV = 9,
                        SpeedIV = 15,
                        SpecialIV = 4,
                        Move1PowerPointsUps = 0,
                        Move1PowerPointsCurrent = 10,
                        Move2PowerPointsUps = 0,
                        Move2PowerPointsCurrent = 15,
                        Move3PowerPointsUps = 0,
                        Move3PowerPointsCurrent = 20,
                        Move4PowerPointsUps = 0,
                        Move4PowerPointsCurrent = 20,
                        Friendship = 138,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 0,
                        CaughtLevel = 0,
                        OTGender = 0,
                        CaughtLocation = 0,
                        Level = 25,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 63,
                        MaxHp = 63,
                        Attack = 45,
                        Defense = 37,
                        Speed = 42,
                        SpAttack = 36,
                        SpDefense = 36,
                        OTName = "Justin",
                        Name = "DRATINI",
                    },
                    new Pokemon
                    {
                        SpeciesId = 100,
                        HeldItem = 0,
                        MoveIndex1 = 33,
                        MoveIndex2 = 103,
                        MoveIndex3 = 49,
                        MoveIndex4 = 120,
                        TrainerId = 3370,
                        Experience = 12651,
                        HitPointsEV = 112,
                        AttackEV = 146,
                        DefenseEV = 95,
                        SpeedEV = 129,
                        SpecialEV = 136,
                        AttackIV = 12,
                        DefenseIV = 6,
                        SpeedIV = 3,
                        SpecialIV = 2,
                        Move1PowerPointsUps = 0,
                        Move1PowerPointsCurrent = 35,
                        Move2PowerPointsUps = 0,
                        Move2PowerPointsCurrent = 40,
                        Move3PowerPointsUps = 0,
                        Move3PowerPointsCurrent = 20,
                        Move4PowerPointsUps = 0,
                        Move4PowerPointsCurrent = 5,
                        Friendship = 86,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 0,
                        CaughtLevel = 0,
                        OTGender = 0,
                        CaughtLocation = 0,
                        Level = 23,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 52,
                        MaxHp = 52,
                        Attack = 25,
                        Defense = 31,
                        Speed = 53,
                        SpAttack = 31,
                        SpDefense = 31,
                        OTName = "Justin",
                        Name = "VOLTORB",
                    },
                    new Pokemon
                    {
                        SpeciesId = 30,
                        HeldItem = 0,
                        MoveIndex1 = 10,
                        MoveIndex2 = 33,
                        MoveIndex3 = 40,
                        MoveIndex4 = 24,
                        TrainerId = 3370,
                        Experience = 7896,
                        HitPointsEV = 4028,
                        AttackEV = 4394,
                        DefenseEV = 4143,
                        SpeedEV = 4293,
                        SpecialEV = 3202,
                        AttackIV = 5,
                        DefenseIV = 15,
                        SpeedIV = 8,
                        SpecialIV = 12,
                        Move1PowerPointsUps = 0,
                        Move1PowerPointsCurrent = 35,
                        Move2PowerPointsUps = 0,
                        Move2PowerPointsCurrent = 35,
                        Move3PowerPointsUps = 0,
                        Move3PowerPointsCurrent = 35,
                        Move4PowerPointsUps = 0,
                        Move4PowerPointsCurrent = 30,
                        Friendship = 135,
                        PokerusStrain = 0,
                        PokerusDuration = 0,
                        CaughtTime = 0,
                        CaughtLevel = 0,
                        OTGender = 0,
                        CaughtLocation = 0,
                        Level = 22,
                        Status = 0,
                        Unused = 0,
                        CurrentHp = 71,
                        MaxHp = 71,
                        Attack = 38,
                        Defense = 44,
                        Speed = 36,
                        SpAttack = 37,
                        SpDefense = 37,
                        OTName = "Justin",
                        Name = "NIDORINA",
                    }
                }
                }
            };
            return model;
        }

        protected virtual void AssertPokemonEqualityThorough(Pokemon pokemonExpected, Pokemon pokemonActual)
        {
            Assert.AreEqual(pokemonExpected.SpeciesId, pokemonActual.SpeciesId, "Pokemon Species");
            Assert.AreEqual(pokemonExpected.HeldItem, pokemonActual.HeldItem, "Pokemon heldItem");
            Assert.AreEqual(pokemonExpected.MoveIndex1, pokemonActual.MoveIndex1, "Pokemon moveIndex1");
            Assert.AreEqual(pokemonExpected.MoveIndex2, pokemonActual.MoveIndex2, "Pokemon moveIndex2");
            Assert.AreEqual(pokemonExpected.MoveIndex3, pokemonActual.MoveIndex3, "Pokemon moveIndex3");
            Assert.AreEqual(pokemonExpected.MoveIndex4, pokemonActual.MoveIndex4, "Pokemon moveIndex4");
            Assert.AreEqual(pokemonExpected.TrainerId, pokemonActual.TrainerId, "Pokemon trainerID");
            Assert.AreEqual(pokemonExpected.Experience, pokemonActual.Experience, "Pokemon experience");
            Assert.AreEqual(pokemonExpected.HitPointsEV, pokemonActual.HitPointsEV, "Pokemon hpEV");
            Assert.AreEqual(pokemonExpected.AttackEV, pokemonActual.AttackEV, "Pokemon attackEV");
            Assert.AreEqual(pokemonExpected.DefenseEV, pokemonActual.DefenseEV, "Pokemon defenseEV");
            Assert.AreEqual(pokemonExpected.SpeedEV, pokemonActual.SpeedEV, "Pokemon speedEV");
            Assert.AreEqual(pokemonExpected.SpecialEV, pokemonActual.SpecialEV, "Pokemon specialEV");
            Assert.AreEqual(pokemonExpected.AttackIV, pokemonActual.AttackIV, "Pokemon attackIV");
            Assert.AreEqual(pokemonExpected.DefenseIV, pokemonActual.DefenseIV, "Pokemon defenseIV");
            Assert.AreEqual(pokemonExpected.SpeedIV, pokemonActual.SpeedIV, "Pokemon speedIV");
            Assert.AreEqual(pokemonExpected.SpecialIV, pokemonActual.SpecialIV, "Pokemon specialIV");
            Assert.AreEqual(pokemonExpected.Move1PowerPointsUps, pokemonActual.Move1PowerPointsUps, "Pokemon ppUps1");
            Assert.AreEqual(pokemonExpected.Move1PowerPointsCurrent, pokemonActual.Move1PowerPointsCurrent, "Pokemon currentPP1");
            Assert.AreEqual(pokemonExpected.Move2PowerPointsUps, pokemonActual.Move2PowerPointsUps, "Pokemon ppUps2");
            Assert.AreEqual(pokemonExpected.Move2PowerPointsCurrent, pokemonActual.Move2PowerPointsCurrent, "Pokemon currentPP2");
            Assert.AreEqual(pokemonExpected.Move3PowerPointsUps, pokemonActual.Move3PowerPointsUps, "Pokemon ppUps3");
            Assert.AreEqual(pokemonExpected.Move3PowerPointsCurrent, pokemonActual.Move3PowerPointsCurrent, "Pokemon currentPP3");
            Assert.AreEqual(pokemonExpected.Move4PowerPointsUps, pokemonActual.Move4PowerPointsUps, "Pokemon ppUps4");
            Assert.AreEqual(pokemonExpected.Move4PowerPointsCurrent, pokemonActual.Move4PowerPointsCurrent, "Pokemon currentPP4");
            Assert.AreEqual(pokemonExpected.Friendship, pokemonActual.Friendship, "Pokemon friendship");
            Assert.AreEqual(pokemonExpected.PokerusStrain, pokemonActual.PokerusStrain, "Pokemon pokerusStrain");
            Assert.AreEqual(pokemonExpected.PokerusDuration, pokemonActual.PokerusDuration, "Pokemon pokerusDuration");
            Assert.AreEqual(pokemonExpected.CaughtTime, pokemonActual.CaughtTime, "Pokemon caughtTime");
            Assert.AreEqual(pokemonExpected.CaughtLevel, pokemonActual.CaughtLevel, "Pokemon caughtLevel");
            Assert.AreEqual(pokemonExpected.OTGender, pokemonActual.OTGender, "Pokemon OTGender");
            Assert.AreEqual(pokemonExpected.CaughtLocation, pokemonActual.CaughtLocation, "Pokemon caughtLocation");
            Assert.AreEqual(pokemonExpected.Level, pokemonActual.Level, "Pokemon level");
            Assert.AreEqual(pokemonExpected.Status, pokemonActual.Status, "Pokemon status");
            Assert.AreEqual(pokemonExpected.Unused, pokemonActual.Unused, "Pokemon unused");
            Assert.AreEqual(pokemonExpected.CurrentHp, pokemonActual.CurrentHp, "Pokemon currentHp");
            Assert.AreEqual(pokemonExpected.MaxHp, pokemonActual.MaxHp, "Pokemon maxHp");
            Assert.AreEqual(pokemonExpected.Attack, pokemonActual.Attack, "Pokemon attack");
            Assert.AreEqual(pokemonExpected.Defense, pokemonActual.Defense, "Pokemon defense");
            Assert.AreEqual(pokemonExpected.Speed, pokemonActual.Speed, "Pokemon speed");
            Assert.AreEqual(pokemonExpected.SpAttack, pokemonActual.SpAttack, "Pokemon spAttack");
            Assert.AreEqual(pokemonExpected.SpDefense, pokemonActual.SpDefense, "Pokemon spDefense");
            Assert.AreEqual(pokemonExpected.OTName, pokemonActual.OTName, "Pokemon OTName");
            Assert.AreEqual(pokemonExpected.Name, pokemonActual.Name, "Pokemon Name");
        }


        protected virtual void AssertSavModelEqualityThorough(SAVFileModel expectedModel, SAVFileModel actualModel)
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

            var test = actualModel.CurrentBoxPokemonlist.Pokemon.Cast<Pokemon>().Select(p => p.SpeciesId).ToList();
            for (var i = 0; i < expectedModel.CurrentBoxPokemonlist.Pokemon.Length; i++)
            {
                var pokemonExpected = expectedModel.CurrentBoxPokemonlist.Pokemon[i];
                var pokemonActual = actualModel.CurrentBoxPokemonlist.Pokemon[i];
                Assert.AreEqual(pokemonExpected.SpeciesId, pokemonActual.SpeciesId, "Pokemon Species");
            }
        }
    }
}