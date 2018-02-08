using PokemonGenerator.Enumerations;

namespace PokemonGenerator.Models.Configuration
{
    /// <summary>
    /// Player options
    /// </summary>
    public class PlayerOptions
    {
        public PlayerOptions()
        {
            InputSaveLocation = string.Empty;
            OutputSaveLocation = string.Empty;
            GameVersion = PokemonGame.Gold.ToString();
            Name = "PLAYER2";
            Team = new Team();
        }

        /// <summary>
        /// The Pokemon Gold/Silver emulator sav file to modify for the player
        /// A default sav file is used when this parameter in omitted
        /// </summary>
        public string InputSaveLocation { get; set; }


        /// <summary>
        /// The path to the desired output location for the Pokemon Gold/Silver emulator sav file for the player
        /// </summary>
        public string OutputSaveLocation { get; set; }

        /// <summary>
        /// The Game to use for the player (Gold or Silver)
        /// </summary>
        public string GameVersion { get; set; }


        /// <summary>
        /// The Name to use for the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pokemon Team
        /// </summary>
        public Team Team { get; set; }
    }
}