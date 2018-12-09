using System.Data.Entity;
using AvorionDataConverter.Entities;

namespace AvorionDataConverter.Entity_Framework {
    public class AvorionContext : DbContext {
        public AvorionContext() : base("DefaultConnection") {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AvorionContext>());
            Database.Initialize(true);
        }

        public DbSet<Sector> Sectors { get; set; }

        public DbSet<StationType> StationTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Sector>().HasMany(x => x.Stations);
        }
    }
}