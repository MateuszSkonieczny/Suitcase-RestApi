using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class ItemEfConfiguration: IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(e => e.IdItem).HasName("Item_pk");
            builder.Property(e => e.IdItem).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.IsPacked).IsRequired();
        }
    }
}