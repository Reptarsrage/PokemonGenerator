using PokemonGenerator.DAL.Serialization;
using System.Data.Entity;

namespace PokemonGenerator.DAL
{
    /// <summary>
    /// Auto-Generated. DO NOT MODIFY!
    /// </summary>
    internal partial class ThePokeBase : DbContext
    {
        public ThePokeBase()
            : base("name=ThePokeBase")
        {
        }

        public virtual DbSet<type_efficacy> type_efficacy { get; set; }
        public virtual DbSet<type> types { get; set; }
        public virtual DbSet<tbl_vwBaseStats> tbl_vwBaseStats { get; set; }
        public virtual DbSet<tbl_vwEvolutions> tbl_vwEvolutions { get; set; }
        public virtual DbSet<tbl_vwGenIIItems> tbl_vwGenIIItems { get; set; }
        public virtual DbSet<tbl_vwGenIIMoves> tbl_vwGenIIMoves { get; set; }
        public virtual DbSet<tbl_vwPokemonMoves> tbl_vwPokemonMoves { get; set; }
        public virtual DbSet<tbl_vwTMs> tbl_vwTMs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<type>()
                .HasMany(e => e.type_efficacy)
                .WithRequired(e => e.type)
                .HasForeignKey(e => e.damage_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<type>()
                .HasMany(e => e.type_efficacy1)
                .WithRequired(e => e.type1)
                .HasForeignKey(e => e.target_type_id)
                .WillCascadeOnDelete(false);
        }
    }
}