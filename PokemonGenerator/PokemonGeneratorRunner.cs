using PokemonGenerator.Enumerations;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace PokemonGenerator
{
    public class PokemonGeneratorRunner : IPokemonGeneratorRunner
    {
        private readonly IPokemonGenerator _pokemonGenerator;
        private readonly IPokeSerializer _pokeSerializer;
        private readonly IPokeDeserializer _pokeDeserializer;

        public PokemonGeneratorRunner(IPokemonGenerator pokemonGenerator, IPokeSerializer pokeSerializer, IPokeDeserializer pokeDeserializer)
        {
            _pokemonGenerator = pokemonGenerator;
            _pokeSerializer = pokeSerializer;
            _pokeDeserializer = pokeDeserializer;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public void Run(string contentDirectory, string outputDirectory, PokeGeneratorArguments options)
        {
            if (string.IsNullOrWhiteSpace(contentDirectory) || !Directory.Exists(contentDirectory))
            {
                throw new ArgumentException("contentDirectory");
            }

            if (string.IsNullOrWhiteSpace(outputDirectory) || outputDirectory.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                throw new ArgumentException("outputDirectory");
            }

            if (options == null)
                throw new ArgumentException("options");

            var sav = ReadSavProperties(options.InputSavOne);

            // Generate Player One and Team
            sav.Playername = options.NameOne;
            CopyAndGen(options.OutputSav1, options.InputSavOne, sav, options.Level);

            // Generate Player Two and Team
            sav.Playername = options.NameTwo;
            CopyAndGen(options.OutputSav2, options.InputSavTwo, sav, options.Level);
        }

        /// <summary>
        /// Rakes a sav file, copies it (or replaces it), generates a team of six pokemon, and saves the team to the output file.
        /// </summary>
        /// <param name="out">Full path to the ouput file.</param>
        /// <param name="in">Full path to the input file.</param>
        /// <param name="gen">The <see cref="PokemonGenerator"/> to use.</param>
        /// <param name="sav">The <see cref="SAVFileModel" to use when saving./></param>
        /// <param name="level">The level to generate pokemon at. Must be 5-100.</param>
        private void CopyAndGen(string @out, string @in, SAVFileModel sav, int level)
        {
            if (!Directory.Exists(Path.GetDirectoryName(@out)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(@out));
            }

            var list = _pokemonGenerator.GenerateRandomPokemon(level, Entropy.Low); // TODO: Entropy stuffs
            sav.TeamPokemonlist = list;
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