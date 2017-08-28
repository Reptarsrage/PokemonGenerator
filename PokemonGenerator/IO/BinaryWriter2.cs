using System;
using System.Collections;
using System.IO;

namespace PokemonGenerator.IO
{
    /// <summary>
    /// Contains all tools needed to write to a pokemon Gold/Silver sav file.
    /// 
    /// <para/> 
    /// See information available here for a detailed explaination: <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Save_data_structure_in_Generation_II <para/> 
    /// http://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_structure_in_Generation_II
    /// </summary>
    internal class BinaryWriter2 : BinaryWriter
    {
        private int offset = 0;
        private byte buffer = 0;

        public BinaryWriter2(Stream stream) : base(stream) { }

        public void writeString(string s, int length, Charset charset)
        {
            byte[] data = charset.encodeString(s, length);
            for (int i = 0; i < length; i++)
            {
                Write(data[i]);
            }
        }

        public void WriteInt16LittleEndian(ushort i)
        {
            Write(i);
        }

        public void WriteInt16(ushort i)
        {

            byte ch1 = (byte)(i >> 8);
            byte ch2 = (byte)(i & 0xff);
            Write(new byte[] { ch1, ch2 });
        }


        public void WriteInt24(uint i)
        {
            byte ch1 = (byte)(i >> 16);
            byte ch2 = (byte)((i >> 8) & 0xff);
            byte ch3 = (byte)(i & 0xff);
            Write(new byte[] { ch1, ch2, ch3 });
        }

        public void WriteInt32(uint i)
        {
            byte ch1 = (byte)(i >> 24);
            byte ch2 = (byte)((i >> 16) & 0xff);
            byte ch3 = (byte)((i >> 8) & 0xff);
            byte ch4 = (byte)(i & 0xff);
            Write(new byte[] { ch1, ch2, ch3, ch4 });
        }

        public void WriteInt64(ulong i)
        {
            byte ch1 = (byte)(i >> 56);
            byte ch2 = (byte)((i >> 48) & 0xff);
            byte ch3 = (byte)((i >> 40) & 0xff);
            byte ch4 = (byte)((i >> 32) & 0xff);
            byte ch5 = (byte)((i >> 24) & 0xff);
            byte ch6 = (byte)((i >> 16) & 0xff);
            byte ch7 = (byte)((i >> 8) & 0xff);
            byte ch8 = (byte)(i & 0xff);
            Write(new byte[] { ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8 });
        }

        public void WriteBoolean(bool b)
        {
            Write(b);
        }

        public void WriteBits(BitArray bitArray, int length)
        {
            int data = BitArrayToInt(bitArray);
            var bitsToWrite = length;
            int shift;
            while (bitsToWrite > 0)
            {
                shift = Math.Min(8 - offset, bitsToWrite);
                offset += shift;
                buffer <<= shift;
                buffer |= (byte)(data & (1 << shift) - 1);
                data >>= shift;
                bitsToWrite -= shift;
                if (offset == 8)
                {
                    offset = 0;
                    Write(buffer);
                    buffer = 0;
                }
            }
        }

        private int BitArrayToInt(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];
        }

        public void WriteBit(BitArray b)
        {
            WriteBits(b, 1);
        }

        internal void Fill(byte v1, int v2)
        {
            for (int i = 0; i < v2; i++)
            {
                Write(v1);
            }
        }
    }
}