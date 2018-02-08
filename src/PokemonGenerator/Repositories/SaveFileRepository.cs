using PokemonGenerator.IO;
using PokemonGenerator.Models.Serialization;
using System.IO;

namespace PokemonGenerator.Repositories
{
    public interface ISaveFileRepository
    {
        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        SAVFileModel Deserialize(string filename);

        /// <summary>
        /// Completely parses a given file stream into a <see cref="SAVFileModel"/> .
        /// </summary>
        SAVFileModel Deserialize(Stream stream);

        /// <summary>
        /// Completely serialized a given <see cref="SAVFileModel"/> into the file.
        /// </summary>
        void Serialize(string outFilePath, SAVFileModel sav);
        
        /// <summary>
        /// Completely serialized a given <see cref="SAVFileModel"/> into the file stream.
        /// </summary>
        void Serialize(Stream stream, SAVFileModel sav);
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
        public SAVFileModel Deserialize(string fileName)
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
        public SAVFileModel Deserialize(Stream stream)
        {
            _breader.Open(stream);
            var ret = Deserialize();
            _breader.Close();
            return ret;
        }

        /// <inheritdoc />
        public void Serialize(string fileName, SAVFileModel sav)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                _bwriter.Open(stream);
                _breader.Open(stream);
                Serialize(sav);
                _breader.Close();
                _bwriter.Close();
            }
        }

        /// <inheritdoc />
        public void Serialize(Stream stream, SAVFileModel sav)
        {
            _bwriter.Open(stream);
            _breader.Open(stream);
            Serialize(sav);
            _breader.Close();
            _bwriter.Close();
        }
    }
}