namespace PokemonGenerator.IO
{
    public interface ICharset
    {
        string DecodeString(byte[] data);
        byte[] EncodeString(string value, int length);
    }
}