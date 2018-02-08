using System;
using System.IO;
using PokemonGenerator.Models.Serialization;

namespace PokemonGenerator.IO
{
    public interface IBinaryWriter2
    {
        void Open(string fileName);
        void Open(Stream stream);
        void Close();
        void WriteUInt16(ushort i);
        void WriteInt16LittleEndian(ushort i);
        void WriteUInt24(uint i);
        void WriteUInt32(uint i);
        void WriteUInt64(ulong i);
        void WriteString(string s, int length, ICharset charset);
        void Write(byte b);
        void Seek(long offset, SeekOrigin origin);
        void Fill(byte v1, int v2);
    }

    /// <summary>
    /// Binary writer for <see cref="SaveFileModel"/> serialization
    /// Unless otherwise noted, values are big-endian and either unsigned or two's complement.
    /// </summary>
    public class BinaryWriter2 : IBinaryWriter2, IDisposable
    {
        internal BinaryWriter Writer { get; private set; }

        public virtual void Open(string fileName)
        {
            Writer?.Dispose();
            Writer = new BinaryWriter(File.OpenWrite(fileName));
        }

        public virtual void Open(Stream stream)
        {
            Writer?.Dispose();
            Writer = new BinaryWriter(stream);
        }

        public virtual void Close()
        {
            if (Writer == null) return;
            Writer.Close();
            Writer.Dispose();
        }

        public void WriteString(string s, int length, ICharset charset)
        {
            var data = charset.EncodeString(s, length);
            for (var i = 0; i < length; i++)
            {
                Writer.Write(data[i]);
            }
        }

        public void WriteInt16LittleEndian(ushort i)
        {
            Writer.Write(i);
        }

        public void WriteUInt16(ushort i)
        {
            var ch1 = (byte)(i >> 8);
            var ch2 = (byte)(i & 0xff);
            Writer.Write(new[] { ch1, ch2 });
        }

        public void WriteUInt24(uint i)
        {
            var ch1 = (byte)(i >> 16);
            var ch2 = (byte)((i >> 8) & 0xff);
            var ch3 = (byte)(i & 0xff);
            Writer.Write(new[] { ch1, ch2, ch3 });
        }

        public void WriteUInt32(uint i)
        {
            var ch1 = (byte)(i >> 24);
            var ch2 = (byte)((i >> 16) & 0xff);
            var ch3 = (byte)((i >> 8) & 0xff);
            var ch4 = (byte)(i & 0xff);
            Writer.Write(new[] { ch1, ch2, ch3, ch4 });
        }

        public void WriteUInt64(ulong i)
        {
            var ch1 = (byte)(i >> 56);
            var ch2 = (byte)((i >> 48) & 0xff);
            var ch3 = (byte)((i >> 40) & 0xff);
            var ch4 = (byte)((i >> 32) & 0xff);
            var ch5 = (byte)((i >> 24) & 0xff);
            var ch6 = (byte)((i >> 16) & 0xff);
            var ch7 = (byte)((i >> 8) & 0xff);
            var ch8 = (byte)(i & 0xff);
            Writer.Write(new[] { ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8 });
        }

        public void Fill(byte v1, int v2)
        {
            for (var i = 0; i < v2; i++)
            {
                Writer.Write(v1);
            }
        }

        public void Write(byte b)
        {
            Writer.Write(b);
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            Writer.BaseStream.Seek(offset, origin);
        }

        public void Dispose()
        {
            Close();
        }
    }
}