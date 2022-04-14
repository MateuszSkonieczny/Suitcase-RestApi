using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class CityEfConfiguration: IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.IdCity).HasName("City_pk");
            builder.Property(e => e.IdCity).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
            builder.HasOne(e => e.IdCountryNavigation)
                .WithMany(e => e.Cities)
                .HasForeignKey(e => e.IdCountry)
                .HasConstraintName("City_Country");
            builder.Property(e => e.IdCountry).IsRequired();
        }
    }
}