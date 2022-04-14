using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class CountryEfConfiguration: IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(e => e.IdCountry).HasName("Country_pk");
            builder.Property(e => e.IdCountry).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
        }
    }
}