using PokemonGenerator.Models;

namespace PokemonGenerator.Validators
{
    public interface IPokeGeneratorOptionsValidator
    {
        bool Validate(PokeGeneratorOptions options);
        bool ValidateEntropy(string entropy);
        bool ValidateFileOption(string path, string expectedExtension);
        bool ValidateFilePathOption(string path, string expectedExtension);
        bool ValidateGame(string game);
        bool ValidateLevel(int level);
        bool ValidateName(string name);
        bool ValidateUniquePath(string path1, string path2);
    }
}