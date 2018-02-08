using System.Collections.Generic;

namespace PokemonGenerator.Models.Configuration
{
    public class Team
    {
        public Team()
        {
            MemberIds = new List<int>();
        }

        public List<int> MemberIds { get; set; }
    }
}