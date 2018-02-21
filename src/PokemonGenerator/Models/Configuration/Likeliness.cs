namespace PokemonGenerator.Models.Configuration
{
    public struct Likeliness
    {
        public const double None = 0d;
        public const double Extremely_Low = 0.001d;
        public const double Very_Low = 0.05d;
        public const double Low = 0.10d;
        public const double Medium_Low = 0.30d;
        public const double Medium = 0.50d;
        public const double Medium_High = 0.60d;
        public const double High = 0.70d;
        public const double Very_High = 0.90d;
        public const double Full = 1.00d;
    }
}