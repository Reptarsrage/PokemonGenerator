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
        bool Validate(Options options);
        bool ValidateFileOption(string path, string expectedExtension);
        bool ValidateFilePathOption(string path, string expectedExtension);
        bool ValidateGame(string game);
        bool ValidateLevel(int level);
        bool ValidateName(string name);
        bool ValidateUniquePath(string path1, string path2);
    }

    public class PokeGeneratorOptionsValidator : IPokeGeneratorOptionsValidator
    {
        public bool Validate(Options options)
        {
            var good = true;

            good &= ValidateFileOption(options.PlayerOne.InputSaveLocation, ".sav");
            good &= ValidateFileOption(options.PlayerTwo.InputSaveLocation, ".sav");
            good &= ValidateFilePathOption(options.PlayerOne.OutputSaveLocation, ".sav");
            good &= ValidateFilePathOption(options.PlayerTwo.OutputSaveLocation, ".sav");
            good &= ValidateLevel(options.Level);
            good &= ValidateGame(options.PlayerOne.GameVersion);
            good &= ValidateGame(options.PlayerTwo.GameVersion);
            good &= ValidateName(options.PlayerOne.Name);
            good &= ValidateName(options.PlayerTwo.Name);

            // Check two outs are unique
            return good && ValidateUniquePath(options.PlayerOne.OutputSaveLocation, options.PlayerTwo.OutputSaveLocation);
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