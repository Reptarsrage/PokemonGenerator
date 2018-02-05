namespace PokemonGenerator.Models
{
    /// <summary>
    /// Options include player and GUI related settings but not generator or randomizer 
    /// related settings.
    /// </summary>
    public class PokeGeneratorOptions
    {
        public PokeGeneratorOptions()
        {
            Level = 50;
            PlayerOne = new PokeGeneratorPlayerOptions();
            PlayerTwo = new PokeGeneratorPlayerOptions();
        }

        /// <summary>
        /// Player one options
        /// </summary>
        public PokeGeneratorPlayerOptions PlayerOne { get; set; }

        /// <summary>
        /// Player two options
        /// </summary>
        public PokeGeneratorPlayerOptions PlayerTwo { get; set; }

        /// <summary>
        /// The Pokemon level to generate for
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Location of the Project64 executable
        /// </summary>
        public string Project64Location { get; set; }
    }
}