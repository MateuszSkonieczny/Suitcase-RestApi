using Microsoft.EntityFrameworkCore;
using SuitcaseApi.Configurations;

namespace SuitcaseApi.Models
{
    public class SuitcaseContext: DbContext
    {
        public SuitcaseContext()
        {
            
        }

        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Suitcase> Suitcase { get; set; }
        public virtual DbSet<SuitcaseItem> SuitcaseItem { get; set; }

        public SuitcaseContext(DbContextOptions<SuitcaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEfConfiguration());
            modelBuilder.ApplyConfiguration(new CountryEfConfiguration());
            modelBuilder.ApplyConfiguration(new CityEfConfiguration());
            modelBuilder.ApplyConfiguration(new UserEfConfiguration());
            modelBuilder.ApplyConfiguration(new SuitcaseEfConfiguration());
            modelBuilder.ApplyConfiguration(new SuitcaseItemEfConfiguration());
        }
    }
}