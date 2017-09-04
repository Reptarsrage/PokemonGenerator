namespace PokemonGenerator.IO
{
    public interface ICharset
    {
        string decodeString(byte[] data);
        byte[] encodeString(string value, int length);
    }
}