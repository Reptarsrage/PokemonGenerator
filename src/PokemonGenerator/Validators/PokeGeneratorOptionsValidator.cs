using PokemonGenerator.Enumerations;
using PokemonGenerator.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PokemonGenerator.Validators
{
    public interface IPokeGeneratorOptionsValidator
    {
        bool Validate(PokeGeneratorOptions options);
        bool ValidateFileOption(string path, string expectedExtension);
        bool ValidateFilePathOption(string path, string expectedExtension);
        bool ValidateGame(string game);
        bool ValidateLevel(int level);
        bool ValidateName(string name);
        bool ValidateUniquePath(string path1, string path2);
    }

    public class PokeGeneratorOptionsValidator : IPokeGeneratorOptionsValidator
    {
        public bool Validate(PokeGeneratorOptions options)
        {
            var good = true;

            good &= ValidateFileOption(options.InputSaveOne, ".sav");
            good &= ValidateFileOption(options.InputSaveTwo, ".sav");
            good &= ValidateFilePathOption(options.OutputSaveOne, ".sav");
            good &= ValidateFilePathOption(options.OutputSaveTwo, ".sav");
            good &= ValidateLevel(options.Level);
            good &= ValidateGame(options.GameOne);
            good &= ValidateGame(options.GameTwo);
            good &= ValidateName(options.NameOne);
            good &= ValidateName(options.NameTwo);

            // Check two outs are unique
            return good && ValidateUniquePath(options.OutputSaveOne, options.OutputSaveTwo);
        }

        public bool ValidateUniquePath(string path1, string path2)
        {
            return !string.IsNullOrWhiteSpace(path1) && 
                !string.IsNullOrWhiteSpace(path2) && 
                !Path.GetFullPath(path1).Equals(Path.GetFullPath(path2), StringComparison.OrdinalIgnoreCase);
        }

        public bool ValidateGame(string game)
        {
            return !string.IsNullOrWhiteSpace(game) && Enum.GetNames(typeof(PokemonGame)).ToList().Contains(game);
        }

        public bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length <= 8 && !(new Regex("[^A-Za-z0-9]").IsMatch(name));
        }

        public bool ValidateLevel(int level)
        {
            return 5 <= level && level <= 100;
        }

        public bool ValidateFileOption(string path, string expectedExtension)
        {
            return !string.IsNullOrEmpty(path) &&
                File.Exists(path) &&
                Path.GetExtension(path).Equals(expectedExtension, StringComparison.OrdinalIgnoreCase);
        }

        public bool ValidateFilePathOption(string path, string expectedExtension)
        {
            return !string.IsNullOrEmpty(path) &&
                path.IndexOfAny(Path.GetInvalidPathChars()) == -1 &&
                Path.GetExtension(path).Equals(expectedExtension, StringComparison.OrdinalIgnoreCase);
        }
    }
}