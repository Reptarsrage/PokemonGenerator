namespace PokemonGenerator.Models.Configuration
{
    public class PokemonLiklihood
    {
        public double Ignored { get; set; }

        public double Standard { get; set; }

        public double Legendary { get; set; }

        public double Special { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(PokemonLiklihood))
                return false;

            var pObj = (PokemonLiklihood)obj;

            return pObj.Ignored == Ignored &&
                pObj.Standard == Standard &&
                pObj.Legendary == Legendary &&
                pObj.Special == Special;
        }
    }
}