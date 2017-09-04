namespace PokemonGenerator.Editors
{
    public interface IP64ConfigEditor
    {
        string FileName { get; set; }

        string GetRecentRom();
    }
}