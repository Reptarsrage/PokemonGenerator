using PokemonGenerator.Enumerations;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using System.Diagnostics;
using System.IO;

namespace PokemonGenerator
{
    public class PokemonGeneratorRunner : IPokemonGeneratorRunner
    {
        private readonly string contentDirectory;
        private readonly string outputDirectory;
        private readonly IPokemonGenerator pokemonGenerator;
        private readonly IPokeSerializer pokeSerializer;
        private readonly IPokeDeserializer pokeDeserializer;
        private readonly Charset charset;
        private readonly PokeGeneratorArguments options;

        public PokemonGeneratorRunner(string contentDirectory, string outputDirectory, PokeGeneratorArguments options)
        {
            this.contentDirectory = contentDirectory;
            this.outputDirectory = outputDirectory;
            this.options = options;
            pokemonGenerator = new PokemonGenerator();
            charset = new Charset();
            pokeDeserializer = new PokeDeserializer();
            pokeSerializer = new PokeSerializer();
        }

        public void Run()
        {
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
            var list = pokemonGenerator.GenerateRandomPokemon(level, Entropy.Low); // TODO: Entropy stuffs
            sav.TeamPokémonlist = list;
            WriteSavProperties(@out, @in, sav);
            ReadSavProperties(@out);
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

            pokeSerializer.SerializeSAVFileModal(outname, charset, sav);
        }

        /// <summary>
        /// Reads the Pokemon Gold/Silver sav file and deserializes it into a <see cref="SAVFileModel" modal./> 
        /// </summary>
        /// <param name="filename">Full path to the sav file to read.</param>
        /// <returns></returns>
        private SAVFileModel ReadSavProperties(string filename)
        {
            return pokeDeserializer.ParseSAVFileModel(filename, charset);
        }
    }
}