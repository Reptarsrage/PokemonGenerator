using System;

namespace PokemonGenerator.Editors
{
    public interface INRageIniEditor
    {
        string FileName { get; set; }

        Tuple<string, string> GetRomAndSavFileLocation(int playerNum);
        bool ChangeSavLocations(string text1, string text2);
    }
}