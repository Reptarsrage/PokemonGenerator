using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;
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
            EntropyVal = "Low";
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
                DefaultValue = "Gold.sav")]
        [DefaultValue("")]
        public string InputSaveOne { get; set; }

        [Option("i2", 
                Required = false, 
                HelpText = "The Pokemon Gold/Silver emulator sav file to modify for player 2. " +
                           "A default sav file is used when this parameter in omitted.", 
                DefaultValue = "Gold.sav")]
        [DefaultValue("")]
        public string InputSaveTwo { get; set; }

        [Option("o1", 
                Required = false, 
                HelpText = "The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 1. " +
                "Defaults to 'Player1.sav' on the current user's desktop.", 
                DefaultValue = "Player1.sav")]
        [DefaultValue("")]
        public string OutputSaveOne { get; set; }

        [Option("o2", 
                Required = false, 
                HelpText = "The path to the desired output location for the Pokemon Gold/Silver emulator sav file for player 2. " +
                "Defaults to 'Player2.sav' on the current user's desktop.", 
                DefaultValue = "Player2.sav")]
        [DefaultValue("")]
        public string OutputSaveTwo { get; set; }

        [Option('l', 
                "level", 
                Required = false, 
                HelpText = "The Pokemon level to generate for.", 
                DefaultValue = 50)]
        [DefaultValue(0)]
        public int Level { get; set; }

        [Option('e', 
                "entropy", 
                Required = false, 
                HelpText = "Amount of randomness to use when generating Pokemon. See README for full info.", 
                DefaultValue = "Low")]
        [DefaultValue("")]
        public string EntropyVal { get; set; }

        [JsonIgnore]
        public Entropy Entropy { get; set; }

        [Option('e',
                "entropy",
                Required = false,
                HelpText = "The Game to use for player 1 (Gold or Silver).",
                DefaultValue = "Gold")]
        [DefaultValue("")]
        public string GameOne { get; set; }

        [Option('e',
                "entropy",
                Required = false,
                HelpText = "The Game to use for player 2 (Gold or Silver).",
                DefaultValue = "Gold")]
        [DefaultValue("")]
        public string GameTwo { get; set; }

        [Option('e',
                "entropy",
                Required = false,
                HelpText = "The Name to use for player 1.",
                DefaultValue = "Player1")]
        [DefaultValue("")]
        public string NameOne { get; set; }

        [Option('e',
                "entropy",
                Required = false,
                HelpText = "The Name to use for player 2.",
                DefaultValue = "Player2")]
        [DefaultValue("")]
        public string NameTwo { get; set; }

        [DefaultValue("")]
        public string Project64Location { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}