using PokemonGenerator.Models;

namespace PokemonGenerator.IO
{
    public interface IPokeDeserializer
    {
        SAVFileModel ParseSAVFileModel(string filename);
    }
}