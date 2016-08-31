namespace PokemonGenerator.DAL.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public type()
        {
            type_efficacy = new HashSet<type_efficacy>();
            type_efficacy1 = new HashSet<type_efficacy>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(79)]
        public string identifier { get; set; }

        public int generation_id { get; set; }

        public int? damage_class_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<type_efficacy> type_efficacy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<type_efficacy> type_efficacy1 { get; set; }
    }
}
