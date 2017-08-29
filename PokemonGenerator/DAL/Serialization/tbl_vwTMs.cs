using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonGenerator.DAL.Serialization
{
    internal partial class tbl_vwTMs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int move_id { get; set; }
    }
}