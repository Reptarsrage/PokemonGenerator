namespace PokemonGenerator.DAL.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_vwTMs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int move_id { get; set; }
    }
}
