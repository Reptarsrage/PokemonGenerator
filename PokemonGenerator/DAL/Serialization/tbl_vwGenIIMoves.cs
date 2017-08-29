using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGenerator.DAL.Serialization
{
    internal partial class tbl_vwGenIIMoves
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int moveId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(79)]
        public string moveName { get; set; }

        [StringLength(79)]
        public string identifier { get; set; }

        public short? power { get; set; }

        public short? pp { get; set; }

        [StringLength(79)]
        public string damageType { get; set; }

        public string effect { get; set; }
    }
}