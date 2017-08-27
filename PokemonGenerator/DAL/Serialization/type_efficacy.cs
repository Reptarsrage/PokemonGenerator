using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGenerator.DAL.Serialization
{
    public partial class type_efficacy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int damage_type_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int target_type_id { get; set; }

        public int damage_factor { get; set; }

        public virtual type type { get; set; }

        public virtual type type1 { get; set; }
    }
}
