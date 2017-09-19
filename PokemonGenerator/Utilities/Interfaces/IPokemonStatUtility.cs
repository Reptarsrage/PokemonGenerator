namespace PokemonGenerator.Utilities
{
    interface IPokemonStatUtility
    {
        uint CalculateExperiencePoints(string group, int n);
        uint CalculateHitPoints(double @base, double iv, double ev, double lvl);
        uint CalculateStat(double @base, double iv, double ev, double lvl);
    }
}