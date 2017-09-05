using PokemonGenerator.Enumerations;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Validators;
using System;
using System.Diagnostics;
using System.IO;

namespace PokemonGenerator
{
    public class PokemonGeneratorRunner : IPokemonGeneratorRunner
    {
        private readonly IPokemonGeneratorWorker _pokemonGenerator;
        private readonly IPokeSerializer _pokeSerializer;
        private readonly IPokeDeserializer _pokeDeserializer;
        private readonly IPokeGeneratorOptionsValidator _optionsValidator;

        public PokemonGeneratorRunner(IPokemonGeneratorWorker pokemonGenerator, IPokeSerializer pokeSerializer,
            IPokeDeserializer pokeDeserializer, IPokeGeneratorOptionsValidator optionsValidator)
        {
            _pokemonGenerator = pokemonGenerator;
            _pokeSerializer = pokeSerializer;
            _pokeDeserializer = pokeDeserializer;
            _optionsValidator = optionsValidator;
        }

        public void Run(PersistentConfig configOptions)
        {
            if (!_optionsValidator.Validate(configOptions.Options))
                throw new ArgumentException("configOptions");

            var options = configOptions.Options;
            _pokemonGenerator.Config = configOptions.Configuration;

            var sav = ReadSavProperties(options.InputSaveOne);

            // Generate Player One and Team
            sav.PlayerName = options.NameOne;
            CopyAndGen(options.OutputSaveOne, options.InputSaveOne, sav, options.Level);

            // Generate Player Two and Team
            sav.PlayerName = options.NameTwo;
            CopyAndGen(options.OutputSaveTwo, options.InputSaveTwo, sav, options.Level);
        }

        /// <summary>
        /// Rakes a sav file, copies it (or replaces it), generates a team of six pokemon, and saves the team to the output file.
        /// </summary>
        /// <param name="out">Full path to the ouput file.</param>
        /// <param name="in">Full path to the input file.</param>
        /// <param name="gen">The <see cref="PokemonGeneratorWorker"/> to use.</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving./></param>
        /// <param name="level">The level to generate pokemon at. Must be 5-100.</param>
        private void CopyAndGen(string @out, string @in, SAVFileModel sav, int level)
        {
            if (!Directory.Exists(Path.GetDirectoryName(@out)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(@out));
            }

            var list = _pokemonGenerator.GenerateRandomPokemon(level, Entropy.Low); // TODO: Entropy stuffs
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

            _pokeSerializer.SerializeSAVFileModal(outname, sav);
        }

        /// <summary>
        /// Reads the Pokemon Gold/Silver sav file and deserializes it into a <see cref="SAVFileModel" modal./> 
        /// </summary>
        /// <param name="filename">Full path to the sav file to read.</param>
        /// <returns></returns>
        private SAVFileModel ReadSavProperties(string filename)
        {
            return _pokeDeserializer.ParseSAVFileModel(filename);
        }
    }
}