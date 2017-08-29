using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGenerator.DAL.Serialization
{
    internal partial class tbl_vwGenIIItems
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int game_index { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(79)]
        public string identifier { get; set; }
    }
}