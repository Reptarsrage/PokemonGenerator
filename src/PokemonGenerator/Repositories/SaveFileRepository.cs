using PokemonGenerator.IO;
using PokemonGenerator.Models.Serialization;
using System.IO;

namespace PokemonGenerator.Repositories
{
    public interface ISaveFileRepository
    {
        /// <summary>
        /// Completely parses a given file stream into a <see cref="SaveFileModel"/> .
        /// </summary>
        SaveFileModel Deserialize(string filename);

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SaveFileModel"/> .
        /// </summary>
        SaveFileModel Deserialize(Stream stream);

        /// <summary>
        /// Completely serialized a given <see cref="SaveFileModel"/> into the file.
        /// </summary>
        void Serialize(string outFilePath, SaveFileModel save);
        
        /// <summary>
        /// Completely serialized a given <see cref="SaveFileModel"/> into the file stream.
        /// </summary>
        void Serialize(Stream stream, SaveFileModel save);
    }

    public partial class SaveFileRepository : ISaveFileRepository
    {
        private readonly ICharset _charset;
        private readonly IBinaryWriter2 _bwriter;
        private readonly IBinaryReader2 _breader;

        public SaveFileRepository(
            ICharset charset,
            IBinaryWriter2 bwriter,
            IBinaryReader2 breader
            )
        {
            _charset = charset;
            _bwriter = bwriter;
            _breader = breader;
        }

        /// <inheritdoc />
        public SaveFileModel Deserialize(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                _breader.Open(stream);
                var ret = Deserialize();
                _breader.Close();
                return ret;
            }
        }

        /// <inheritdoc />
        public SaveFileModel Deserialize(Stream stream)
        {
            _breader.Open(stream);
            var ret = Deserialize();
            _breader.Close();
            return ret;
        }

        /// <inheritdoc />
        public void Serialize(string fileName, SaveFileModel save)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                _bwriter.Open(stream);
                _breader.Open(stream);
                Serialize(save);
                _breader.Close();
                _bwriter.Close();
            }
        }

        /// <inheritdoc />
        public void Serialize(Stream stream, SaveFileModel save)
        {
            _bwriter.Open(stream);
            _breader.Open(stream);
            Serialize(save);
            _breader.Close();
            _bwriter.Close();
        }
    }
}