/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>

namespace PokemonGenerator.Modals
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    class ItemEntry
    {
        public byte Count; // 1
        public byte Index; // 1

        /// <summary>
        /// Prettry prints the ItemEntry contents
        /// </summary>
        public override string ToString()
        {
            return $"{Index} x ({Count})";
        }
    }
}
