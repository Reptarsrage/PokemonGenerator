using System.Text;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class ItemList
    {
        public byte Count; // 1
        public ItemEntry[] ItemEntries; // Capacity * 2 + 2
        public byte Terminator; // 1

        /// <summary>
        /// Initializes <see cref="ItemList"/> with given capacity.
        /// </summary>
        public ItemList(int capacity)
        {
            ItemEntries = new ItemEntry[capacity];
            Count = (byte)capacity;
            for (var i = 0; i < capacity; i++)
            {
                ItemEntries[i] = new ItemEntry();
            }
        }

        /// <summary>
        /// Prettry prints the ItemList contents
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"{Count} items");
            for (int i = 0; i < Count; i++)
            {
                builder.AppendLine($"\t{ItemEntries[i].ToString()}");
            }
            return builder.ToString();
        }
    }
}