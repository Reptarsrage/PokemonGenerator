using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Serialization;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using PokemonGenerator.Validators;
using System;
using System.Diagnostics;
using System.IO;

namespace PokemonGenerator.Managers
{
    public interface IPokemonGeneratorManager
    {
        void Run(PersistentConfig configOptions);
    }

    public class PokemonGeneratorManager : IPokemonGeneratorManager
    {
        private readonly IPokemonTeamProvider _pokemonProvider;
        private readonly ISaveFileRepository _saveFileRepository;
        private readonly IPokeGeneratorOptionsValidator _optionsValidator;

        public PokemonGeneratorManager(IPokemonTeamProvider pokemonProvider,
            ISaveFileRepository saveFileRepository,
            IPokeGeneratorOptionsValidator optionsValidator)
        {
            _pokemonProvider = pokemonProvider;
            _saveFileRepository = saveFileRepository;
            _optionsValidator = optionsValidator;
        }

        public void Run(PersistentConfig configOptions)
        {
            if (!_optionsValidator.Validate(configOptions.Options))
                throw new ArgumentException("configOptions");

            var options = configOptions.Options;

            var sav = ReadSavProperties(options.PlayerOne.InputSaveLocation);

            // Generate Player One and Team
            sav.PlayerName = options.PlayerOne.Name;
            CopyAndGen(options.PlayerOne.OutputSaveLocation, options.PlayerOne.InputSaveLocation, sav, options.Level);

            // Generate Player Two and Team
            sav.PlayerName = options.PlayerTwo.Name;
            CopyAndGen(options.PlayerTwo.OutputSaveLocation, options.PlayerTwo.InputSaveLocation, sav, options.Level);
        }

        /// <summary>
        /// Rakes a sav file, copies it (or replaces it), generates a team of six pokemon, and saves the team to the output file.
        /// </summary>
        /// <param name="out">Full path to the ouput file.</param>
        /// <param name="in">Full path to the input file.</param>
        /// <param name="gen">The <see cref="PokemonTeamProvider"/> to use.</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving./></param>
        /// <param name="level">The level to generate pokemon at. Must be 5-100.</param>
        private void CopyAndGen(string @out, string @in, SAVFileModel sav, int level)
        {
            if (!Directory.Exists(Path.GetDirectoryName(@out)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(@out));
            }

            var list = _pokemonProvider.GenerateRandomPokemonTeam(level);
            sav.TeamPokemonList = list;
            WriteSavProperties(@out, @in, sav);
            ReadSavProperties(@out); // Verification only
            Debug.Print($"Created file {@out}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outname">Full path to the file to write to</param>
        /// <param name="filename">Full path to Tthe input file (needed so that we don't accidentally delete it if it's also the output file.)</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving.</param>
        private void WriteSavProperties(string outname, string filename, SAVFileModel sav)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"The provided input sav file was not found or inaccessible. '{filename}'.");
            }

            if (File.Exists(outname) && !Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Delete(outname);
            }

            if (!Path.GetFullPath(filename).Equals(Path.GetFullPath(outname)))
            {
                File.Copy(filename, outname);
            }

            _saveFileRepository.Serialize(outname, sav);
        }

        /// <summary>
        /// Reads the Pokemon Gold/Silver sav file and deserializes it into a <see cref="SAVFileModel" modal./> 
        /// </summary>
        /// <param name="filename">Full path to the sav file to read.</param>
        /// <returns></returns>
        private SAVFileModel ReadSavProperties(string filename)
        {
            return _saveFileRepository.Deserialize(filename);
        }
    }
}