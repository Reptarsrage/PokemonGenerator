using CommandLine;
using PokemonGenerator.Enumerations;
using System.ComponentModel;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class PokeGeneratorOptions
    {
        public PokeGeneratorOptions()
        {
            InputSaveOne = string.Empty;
            InputSaveTwo = string.Empty;
            OutputSaveOne = string.Empty;
            OutputSaveTwo = string.Empty;
            GameOne = PokemonGame.Gold.ToString();
            GameTwo = PokemonGame.Gold.ToString();
            NameOne = string.Empty;
            NameTwo = string.Empty;
            Level = 50;
        }

        [Option("i1",
                Required = false,
                HelpText = "The Pokemon Gold/Silver emulator sav file to modify for player 1. " +
                           "A default sav file is used when this parameter in omitted.",
                Default = "Gold.sav")]
        [DefaultValue("")]
        public string InputSaveOne { get; set; }

        [Option("i2",
                Required = false,
                HelpText = "The Pokemon Gold/Silver emulator sav file to modify for player 2. " +
                           "A default sav file is used when this parameter in omitted.",
                Default = "Gold.sav")]
        [DefaultValue("")]
        public string InputSaveTwo { get; set; }

        [Option("o1",
                Required = false,
                HelpText = "The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 1. " +
                "Defaults to 'Player1.sav' on the current user's desktop.",
                Default = "Player1.sav")]
        [DefaultValue("")]
        public string OutputSaveOne { get; set; }

        [Option("o2",
                Required = false,
                HelpText = "The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 2. " +
                "Defaults to 'Player2.sav' on the current user's desktop.",
                Default = "Player2.sav")]
        [DefaultValue("")]
        public string OutputSaveTwo { get; set; }

        [Option('l',
                "level",
                Required = false,
                HelpText = "The Pokemon level to generate for.",
                Default = 50)]
        [DefaultValue(0)]
        public int Level { get; set; }

        [Option("g1",
                Required = false,
                HelpText = "The Game to use for player 1 (Gold or Silver).",
                Default = "Gold")]
        [DefaultValue("")]
        public string GameOne { get; set; }

        [Option("g2",
                Required = false,
                HelpText = "The Game to use for player 2 (Gold or Silver).",
                Default = "Gold")]
        [DefaultValue("")]
        public string GameTwo { get; set; }

        [Option("n1",
                Required = false,
                HelpText = "The Name to use for player 1.",
                Default = "Player1")]
        [DefaultValue("")]
        public string NameOne { get; set; }

        [Option("n2",
                Required = false,
                HelpText = "The Name to use for player 2.",
                Default = "Player2")]
        [DefaultValue("")]
        public string NameTwo { get; set; }

        [DefaultValue("")]
        public string Project64Location { get; set; }
    }
}