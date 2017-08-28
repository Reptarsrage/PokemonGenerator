using System;

namespace PokemonGeneratorGUI.Editors
{
    public interface INRageIniEditor
    {
        Tuple<string, string> GetRomAndSavFileLocation(int playerNum);
    }
}