using PokemonGenerator.Models;

namespace PokemonGenerator.Tests.IO_Tests
{
    public abstract class SerializerTestsBase
    {
        protected virtual SAVFileModel BuildTestModel()
        {
            var model = new SAVFileModel
            {
                Options = 23,
                PlayerTrainerID = 13,
                PlayerName = "Tester",
                UnusedPlayersmomname = "Mom",
                RivalName = "Rival",
                UnusedRedsname = "Red",
                UnusedBluesname = "Blue",
                Daylightsavings = false,
                TimePlayed = 12345,
                Playerpalette = 0xA,
                Money = 321,
                JohtoBadges = 0x8,
                TMpocket = new TMPocket
                {
                    HMs = new byte[7],
                    TMs = new byte[50],
                },
                PocketItemList = new ItemList(0),
                PocketKeyItemList = new ItemList(0),
                PocketBallItemList = new ItemList(0),
                PCItemList = new ItemList(0),
                CurrentPCBoxNumber = 0x1,
                PCBoxNames = new string[14],
                TeamPokemonList = new PokeList(6),
                PokedexOwned = new bool[256],
                PokedexSeen = new bool[256],
                CurrentBoxPokemonlist = new PokeList(0),
                Playergender = 0x1,
                Boxes = new PokeList[14],
                Checksum1 = 0xdead,
                Checksum2 = 0xbeef,
            };
            for (int i = 0; i < model.Boxes.Length; i++)
            {
                model.Boxes[i] = new PokeList(0);
                model.PCBoxNames[i] = i.ToString();
            }

            for (int i = 0; i < model.TeamPokemonList.Count; i++)
            {
                model.TeamPokemonList.Pokemon[i] = new Pokemon
                {
                    OTName = i.ToString(),
                    Name = i.ToString(),
                    Species = (byte)i,
                    Types = new string[] { "Fake" },
                    MoveName1 = "Move1",
                    MoveName2 = "Move2",
                    MoveName3 = "Move3",
                    MoveName4 = "Move4"
                };
                model.TeamPokemonList.Species[i] = model.TeamPokemonList.Pokemon[i].Species;
                model.TeamPokemonList.OTNames[i] = model.TeamPokemonList.Pokemon[i].OTName;
                model.TeamPokemonList.Names[i] = model.TeamPokemonList.Pokemon[i].Name;
            }

            return model;
        }
    }
}
