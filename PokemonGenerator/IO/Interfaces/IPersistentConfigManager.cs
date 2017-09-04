using PokemonGenerator.Models;

namespace PokemonGenerator.IO
{
    public interface IPersistentConfigManager
    {
        string ConfigFilePath { get; set; }

        PersistentConfig Load();
        void Save(PersistentConfig config);
    }
}