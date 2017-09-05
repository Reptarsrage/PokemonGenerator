using System;
using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    /// <summary>
    /// Contains all tools needed to read from a pokemon Gold/Silver sav file. <para/> 
    /// 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    internal class BinaryReader2 : IBinaryReader2, IDisposable
    {
        internal BinaryReader Reader { get; private set; }
        private int offset = 0;
        private byte buffer = 0;

        public long Position => Reader?.BaseStream?.Position ?? 0;

        public virtual void Open(string fileName)
        {
            if (Reader != null) Reader.Dispose();
            Reader = new BinaryReader(File.OpenRead(fileName));
        }

        public virtual void Open(Stream stream)
        {
            if (Reader != null) Reader.Dispose();
            Reader = new BinaryReader(stream);
        }

        public virtual void Close()
        {
            if (Reader != null)
            {
                Reader.Close();
                Reader.Dispose();
            }
        }

        public string ReadString(int length, ICharset charset)
        {
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = Reader.ReadByte();
            }
            return charset.DecodeString(data);
        }

        public ushort ReadInt16LittleEndian()
        {
            ushort ch1 = Reader.ReadByte();
            ushort ch2 = Reader.ReadByte();
            return (ushort)((ch1 << 0) + (ch2 << 8));
        }

        public ushort ReadInt16()
        {
            ushort ch1 = Reader.ReadByte();
            ushort ch2 = Reader.ReadByte();
            return (ushort)((ch1 << 8) + (ch2 << 0));
        }


        public uint ReadInt24()
        {
            uint ch1 = Reader.ReadByte();
            uint ch2 = Reader.ReadByte();
            uint ch3 = Reader.ReadByte();
            return (ch1 << 16) + (ch2 << 8) + (ch3 << 0);
        }

        public uint ReadInt32()
        {
            uint ch1 = Reader.ReadByte();
            uint ch2 = Reader.ReadByte();
            uint ch3 = Reader.ReadByte();
            uint ch4 = Reader.ReadByte();
            return (ch1 << 24) + (ch2 << 16) + (ch3 << 8) + (ch4 << 0);
        }

        public ulong ReadInt64()
        {
            ulong ch1 = Reader.ReadByte();
            ulong ch2 = Reader.ReadByte();
            ulong ch3 = Reader.ReadByte();
            ulong ch4 = Reader.ReadByte();
            ulong ch5 = Reader.ReadByte();
            ulong ch6 = Reader.ReadByte();
            ulong ch7 = Reader.ReadByte();
            ulong ch8 = Reader.ReadByte();
            return (ch1 << 56) + (ch2 << 48) + (ch3 << 40) + (ch4 << 32) + (ch5 << 24) + (ch6 << 16) + (ch7 << 8) + (ch8 << 0);
        }

        public bool ReadBoolean()
        {
            var ch1 = Reader.ReadByte();
            return (ch1 != 0);
        }

        public BitArray ReadBits(int numBits)
        {
            int bitsToRead = numBits;
            int ret = 0;
            int length;
            while (bitsToRead > 0)
            {
                if (offset == 0)
                {
                    buffer = Reader.ReadByte();
                    offset = 8;
                }
                length = Math.Min(offset, bitsToRead);
                offset -= length;
                ret <<= length;
                ret |= (int)((buffer >> offset) & (1 << length) - 1);
                bitsToRead -= length;
            }
            if (offset == 0)
            {
                buffer = 0;
            }
            return IntToBitArrayOfSize(ret, numBits);
        }

        private BitArray IntToBitArrayOfSize(int val, int size)
        {
            var ret = new BitArray(size, false);
            for (int i = 0; i < size; i++)
            {
                ret.Set(i, (val >> i & 1) == 1);
            }
            return ret;
        }

        public BitArray ReadBit()
        {
            var ch1 = ReadBits(1);
            return ch1;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            Reader.BaseStream.Seek(offset, origin);
        }

        public byte ReadByte()
        {
            return Reader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return Reader.ReadBytes(count);
        }

        public void Dispose()
        {
            Close();
        }
    }
}