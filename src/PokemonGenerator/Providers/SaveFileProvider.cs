using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Repositories;
using System.IO;

namespace PokemonGenerator.Providers
{
    /// <summary>
    /// Allows saving and loading from a pokemon save file
    /// </summary>
    public interface ISaveFileProvider
    {
        /// <summary>
        /// Writes a <see cref="SaveFileModel" /> to a Gold/Silver save file
        /// </summary>
        /// <param name="outname">Full path to the file to write to</param>
        /// <param name="filename">Full path to Tthe input file (needed so that we don't accidentally delete it if it's also the output file.)</param>
        /// <param name="save">The <see cref="SaveFileModel" /> to use when saving. </param>
        void Save(string outname, string filename, SaveFileModel save);

        /// <summary>
        /// Reads the Pokemon Gold/Silver save file and deserializes it into a <see cref="SaveFileModel" />
        /// </summary>
        /// <param name="filename">Full path to the save file to read.</param>
        /// <returns></returns>
        SaveFileModel Load(string filename);
    }

    /// <inheritdoc />
    public class SaveFileProvider : ISaveFileProvider
    {
        private readonly ISaveFileRepository _saveFileRepository;

        public SaveFileProvider(ISaveFileRepository saveFileRepository)
        {
            _saveFileRepository = saveFileRepository;
        }

        /// <inheritdoc />
        public void Save(string outname, string filename, SaveFileModel save)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The provided input save file was not found or inaccessible. '{filename}'.");
            }

            if (File.Exists(outname) && !Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Delete(outname);
            }

            if (!Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Copy(filename, outname);
            }

            _saveFileRepository.Serialize(outname, save);
        }

        /// <inheritdoc />
        public SaveFileModel Load(string filename)
        {
            return _saveFileRepository.Deserialize(filename);
        }
    }
}