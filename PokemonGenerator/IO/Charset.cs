using System.Text;

namespace PokemonGenerator.IO
{
    public interface ICharset
    {
        string DecodeString(byte[] data);
        byte[] EncodeString(string value, int length);
    }

    /// <summary>
    /// The Charset used in pokemon Gold/Silver version.
    /// <para/>
    /// See: http://bulbapedia.bulbagarden.net/wiki/Character_encoding_in_Generation_II
    /// </summary>
    internal class Charset : ICharset
    {
        private const byte NULL_TERMINATOR = 0x50;

        private char[] charset = { '_', '?', '?', '?', '?', 'ガ', 'ギ', 'グ', 'ゲ', 'ゴ', 'ザ', 'ジ', 'ズ', 'ゼ', 'ゾ', 'ダ', 'ヂ',
            'ヅ', 'デ', 'ド', '?', '?', '?', '?', '?',
            'バ', 'ビ', 'ブ', 'ボ', '?', '?', '?', '?', '?', '?', '?', '?', '?', 'が', 'ぎ', 'ぐ', 'げ',
            'ご', 'ざ', 'じ', 'ず', 'ぜ', 'ぞ', 'だ', 'ぢ', 'づ', 'で', 'ど', '?', '?',
            '?', '?', '?', 'ば', 'び', 'ぶ', 'べ', 'ぼ', '?', 'パ', 'ピ', 'プ', 'ポ', 'ぱ', 'ぴ', 'ぷ', 'ぺ',
            'ぽ', '?', '?', '?', '?', '?', '?', '?', '@', '?', '?', '?', '#',
            '?', '?', '?', '?', '?', '?', '?', '?', '~', '?', '?', '?', '?', '?', '?', '?', '?',
            '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
            '?', '№', '…', '?', '?', '?', '┌', '─', '┐', '│', '└', '┘', ' ', 'A', 'B', 'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
            'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '(', ')', ':', ';', '[', ']', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'é', 'ď', 'ľ', 'ś', 'ť', 'ù',
            'た', 'ち', 'つ', 'て', 'と', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ひ', 'ふ',
            'へ', 'ほ', 'ま', 'み', 'む', 'め', 'も', 'や', 'ゆ', 'よ', 'ら', 'り', 'る', 'れ', 'ろ', 'わ', 'を',
            'ん', 'っ', '\'', '{', '}', '-', 'ŕ', 'ŉ', '?', '!', '.', '?', '?',
            '?', '?', '▶', '?', '^', '¥', '*', '?', '/', ',', '&', '0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9' };

        /// <summary>
        /// Encodes a c# string into a pokemon string.
        /// </summary>
        public byte[] EncodeString(string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new System.ArgumentException("value");
            }

            if (length <= 0)
            {
                throw new System.ArgumentException("length");
            }

            var data = new byte[length];
            for (var i = 0; i < length; i++)
            {
                if (i < value.Length)
                {
                    data[i] = LookupChar(value[i]);
                }
                else
                {
                    data[i] = NULL_TERMINATOR;
                }
            }
            return data;
        }

        /// <summary>
        /// Decodes a pokemon string into a c# string.
        /// </summary>
        public string DecodeString(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                throw new System.ArgumentException("data");
            }

            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == NULL_TERMINATOR)
                {
                    break;
                }
                else
                {
                    builder.Append(charset[data[i]]);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Looks up a char in the charset table
        /// </summary>
        private byte LookupChar(char value)
        {
            for (var i = 0; i < charset.Length; i++)
            {
                if (charset[i] == value)
                {
                    return (byte)i;
                }
            }
            return 80;
        }
    }
}