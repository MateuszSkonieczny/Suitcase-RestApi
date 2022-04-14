using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class SuitcaseEfConfiguration: IEntityTypeConfiguration<Suitcase>
    {
        public void Configure(EntityTypeBuilder<Suitcase> builder)
        {
            builder.HasKey(e => e.IdSuitcase).HasName("Suitcase_pk");
            builder.Property(e => e.IdSuitcase).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
            builder.HasOne(e => e.IdCityNavigation)
                .WithMany(e => e.Suitcases)
                .HasForeignKey(e => e.IdCity)
                .HasConstraintName("Suitcase_City");
            builder.HasOne(e => e.IdUserNavigation)
                .WithMany(e => e.Suitcases)
                .HasForeignKey(e => e.IdUser)
                .HasConstraintName("Suitcase_User");
            builder.Property(e => e.IdCity).IsRequired();
            builder.Property(e => e.IdUser).IsRequired();
        }
    }
}