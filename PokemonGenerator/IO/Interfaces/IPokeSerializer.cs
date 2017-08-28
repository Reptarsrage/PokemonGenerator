using PokemonGenerator.Modals;

namespace PokemonGenerator.IO
{
    internal interface IPokeSerializer
    {
        void SerializeSAVFileModal(string @out, Charset charset, SAVFileModel sav);
    }
}