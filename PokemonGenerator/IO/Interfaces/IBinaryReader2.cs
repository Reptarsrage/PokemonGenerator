using System.IO;

namespace PokemonGenerator.IO
{
    public interface IBinaryReader2
    {
        long Position { get; }

        void Open(string fileName);
        void Open(Stream stream);
        void Close();
        ushort ReadUInt16();
        ushort ReadUInt16LittleEndian();
        uint ReadUInt24();
        uint ReadUInt32();
        ulong ReadUInt64();
        string ReadString(int length, ICharset charset);
        void Seek(long offset, SeekOrigin origin);
        byte ReadByte();
        byte[] ReadBytes(int count);
    }
}