namespace PokemonGenerator.Models.Dto
{
    public class BaseStats
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string Types { get; set; }

        public int Hp { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int SpAttack { get; set; }

        public int SpDefense { get; set; }

        public int Speed { get; set; }

        public string GrowthRate { get; set; }
    }
}
