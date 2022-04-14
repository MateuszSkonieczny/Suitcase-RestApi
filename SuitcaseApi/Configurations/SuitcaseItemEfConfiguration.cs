using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class SuitcaseItemEfConfiguration: IEntityTypeConfiguration<SuitcaseItem>
    {
        public void Configure(EntityTypeBuilder<SuitcaseItem> builder)
        {
            builder.HasKey(e => new {e.IdSuitcase, e.IdItem}).HasName("SuitcaseItem_pk");
            builder.ToTable("Suitcase_Item");
            builder.HasOne(e => e.IdItemNavigation)
                .WithMany(e => e.SuitcaseItems)
                .HasForeignKey(e => e.IdItem)
                .HasConstraintName("Item_SuitcaseItem");
            builder.HasOne(e => e.IdSuitcaseNavigation)
                .WithMany(e => e.SuitcaseItems)
                .HasForeignKey(e => e.IdSuitcase)
                .HasConstraintName("Suitcase_SuitcaseItem");
            builder.Property(e => e.IdItem).IsRequired();
            builder.Property(e => e.IdSuitcase).IsRequired();
        }
    }
}