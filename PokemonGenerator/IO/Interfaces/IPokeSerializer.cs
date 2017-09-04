using PokemonGenerator.Models;

namespace PokemonGenerator.IO
{
    public interface IPokeSerializer
    {
        void SerializeSAVFileModal(string @out, SAVFileModel sav);
    }
}