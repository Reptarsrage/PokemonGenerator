namespace PokemonGenerator.DAL.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_vwEvolutions
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(79)]
        public string identifier { get; set; }

        public int? evolvedFromPrevID { get; set; }

        public int? evolution_trigger_id { get; set; }

        public int? minimum_level { get; set; }
    }
}
