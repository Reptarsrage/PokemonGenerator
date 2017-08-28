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
    internal class BinaryReader2 : BinaryReader
    {
        private int offset = 0;
        private byte buffer = 0;

        public BinaryReader2(Stream stream) : base(stream) { }

        public string ReadString(int length, Charset charset)
        {
            byte[] data = new byte[length];
            for (int i = 0; i < length; i++)
            {
                data[i] = ReadByte();
            }
            return charset.decodeString(data);
        }

        public ushort ReadInt16LittleEndian()
        {
            ushort ch1 = ReadByte();
            ushort ch2 = ReadByte();
            return (ushort)((ch1 << 0) + (ch2 << 8));
        }

        public new ushort ReadInt16()
        {
            ushort ch1 = ReadByte();
            ushort ch2 = ReadByte();
            return (ushort)((ch1 << 8) + (ch2 << 0));
        }


        public uint ReadInt24()
        {
            uint ch1 = ReadByte();
            uint ch2 = ReadByte();
            uint ch3 = ReadByte();
            return (ch1 << 16) + (ch2 << 8) + (ch3 << 0);
        }

        public new uint ReadInt32()
        {
            uint ch1 = ReadByte();
            uint ch2 = ReadByte();
            uint ch3 = ReadByte();
            uint ch4 = ReadByte();
            return (ch1 << 24) + (ch2 << 16) + (ch3 << 8) + (ch4 << 0);
        }

        public new ulong ReadInt64()
        {
            ulong ch1 = ReadByte();
            ulong ch2 = ReadByte();
            ulong ch3 = ReadByte();
            ulong ch4 = ReadByte();
            ulong ch5 = ReadByte();
            ulong ch6 = ReadByte();
            ulong ch7 = ReadByte();
            ulong ch8 = ReadByte();
            return (ch1 << 56) + (ch2 << 48) + (ch3 << 40) + (ch4 << 32) + (ch5 << 24) + (ch6 << 16) + (ch7 << 8) + (ch8 << 0);
        }

        public new bool ReadBoolean()
        {
            var ch1 = ReadByte();
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
                    buffer = ReadByte();
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
    }
}