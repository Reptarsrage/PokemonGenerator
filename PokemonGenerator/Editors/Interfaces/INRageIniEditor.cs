using System;

namespace PokemonGenerator.Editors
{
    public interface INRageIniEditor
    {
        Tuple<string, string> GetRomAndSavFileLocation(int playerNum);
        bool ChangeSavLocations(string text1, string text2);
    }
}