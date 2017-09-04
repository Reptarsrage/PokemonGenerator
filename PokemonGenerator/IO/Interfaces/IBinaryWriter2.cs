using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    public interface IBinaryWriter2
    {
        string FileName { get; }

        void Open(string fileName);
        void Close();
        void WriteBit(BitArray b);
        void WriteBits(BitArray bitArray, int length);
        void WriteBoolean(bool b);
        void WriteInt16(ushort i);
        void WriteInt16LittleEndian(ushort i);
        void WriteInt24(uint i);
        void WriteInt32(uint i);
        void WriteInt64(ulong i);
        void writeString(string s, int length, ICharset charset);
        void Write(byte b);
        void Seek(long offset, SeekOrigin origin);
        void Fill(byte v1, int v2);
    }
}