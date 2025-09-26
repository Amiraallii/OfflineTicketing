using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfflineTicketing.Core.Entities;

namespace OfflineTicketing.Infrastructure.FluentConfig
{
    public class TicketFluentConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket", "Ticketing");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1024);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(t => t.CreatedUser)
                   .WithMany(u => u.CreatedTickets)
                   .HasForeignKey(t => t.CreatedByUserId)
                   .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(t => t.AssignedToUser)
                   .WithMany(u => u.AssignedTickets)
                   .HasForeignKey(t => t.AssignedToUserId)
                   .OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}
