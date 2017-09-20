namespace PokemonGenerator.Utilities
{
    interface IPokemonStatUtility
    {
        uint CalculateExperiencePoints(string experienceGroup, int level);
        uint CalculateHitPoints(double baseHitPoints, double iv, double ev, double level);
        uint CalculateStat(double baseHitPoints, double iv, double ev, double level);
    }
}