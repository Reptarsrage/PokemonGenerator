using PokemonGenerator.Models;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IPokeSerializer
    {
        void SerializeSAVFileModal(string outFilePath, SAVFileModel sav);
        void SerializeSAVFileModal(Stream stream, SAVFileModel sav);
    }
}