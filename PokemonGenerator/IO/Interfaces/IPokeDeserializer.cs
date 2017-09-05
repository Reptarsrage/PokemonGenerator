using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IPokeDeserializer
    {
        SAVFileModel ParseSAVFileModel(string filename);
        SAVFileModel ParseSAVFileModel(Stream stream);
    }
}