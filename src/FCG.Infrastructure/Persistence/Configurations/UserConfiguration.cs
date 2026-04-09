using FCG.Infrastructure.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasMaxLength(User.MaxNameLength).IsRequired();
        builder.Property(e => e.PasswordHash).IsRequired();
        builder.Property(e => e.Role).HasConversion<int>();
        builder.Property(e => e.Email)
            .HasConversion(
                e => e.Value,
                v => Email.Create(v))
            .HasMaxLength(320)
            .IsRequired();
        builder.HasIndex(e => e.Email).IsUnique();
    }
}
