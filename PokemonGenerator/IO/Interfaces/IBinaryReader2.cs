using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IBinaryReader2
    {
        string FileName { get; }
        long Position { get; }

        void Open(string fileName);
        void Open(Stream stream);
        void Close();
        BitArray ReadBit();
        BitArray ReadBits(int numBits);
        bool ReadBoolean();
        ushort ReadInt16();
        ushort ReadInt16LittleEndian();
        uint ReadInt24();
        uint ReadInt32();
        ulong ReadInt64();
        string ReadString(int length, ICharset charset);
        void Seek(long offset, SeekOrigin origin);
        byte ReadByte();
        byte[] ReadBytes(int count);
    }
}