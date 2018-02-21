﻿using PokemonGenerator.Models.Serialization;

namespace PokemonGenerator.Tests.Unit.Models
{
    public class MockSaveFileModelFactory
    {
        public static SaveFileModel CreateTestModel()
        {
            return new SaveFileModel
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
                PCBoxNames = new string[14]
                {
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
                        new Pokemon {SpeciesId = 180, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 72, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 128, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 234, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 132, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 96, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 27, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 201, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 179, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 95, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 175, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 185, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 75, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 160, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 133, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 33, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 64, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 241, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 82, OTName = "Empty", Name = "Empty"},
                        new Pokemon {SpeciesId = 213, OTName = "Empty", Name = "Empty"}
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
        }
    }
}