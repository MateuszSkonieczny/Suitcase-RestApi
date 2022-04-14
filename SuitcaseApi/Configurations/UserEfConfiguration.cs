using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuitcaseApi.Models;

namespace SuitcaseApi.Configurations
{
    public class UserEfConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.IdUser).HasName("User_Id");
            builder.Property(e => e.IdUser).UseIdentityColumn();
            builder.Property(e => e.Login).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(200).IsRequired();
            builder.Property(e => e.HashedPassword).HasMaxLength(255).IsRequired();
            builder.Property(e => e.RefreshToken).HasMaxLength(255).IsRequired();
        }
    }
}