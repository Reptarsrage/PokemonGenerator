namespace PokemonGenerator.Enumerations
{
    public enum Entropy
    {
        Low, // Pokemon adhere to all standards, moves are calculated based on priority and IV/EV values are gaussian curved
        Medium, // TODO Pokemon adhere to all standards, moves are random but strictly enforced and IV/EV values are random
        High, // TODO Pokemon base stats are adhered to but movesets, evolutions, and IV/EV levels are not
        Chaos // TODO Pokemon base stats are randomly generated
    }
}