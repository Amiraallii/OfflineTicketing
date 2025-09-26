using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfflineTicketing.Core.Entities;

namespace OfflineTicketing.Infrastructure.FluentConfig
{
    public class UserFluentConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Ticketing");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x=> x.FullName)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x=> x.Password)
                .IsRequired();
        }
    }
}
