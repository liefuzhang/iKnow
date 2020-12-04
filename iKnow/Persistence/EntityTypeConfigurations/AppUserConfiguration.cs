using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser> {
        public void Configure(EntityTypeBuilder<AppUser> builder) { 
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Location)
                .HasMaxLength(50);

            builder.Property(u => u.Intro)
                .HasMaxLength(255);

            builder.HasMany(u => u.Questions)
                .WithOne(q => q.AppUser)
                .HasForeignKey(q => q.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.Answers)
                .WithOne(a => a.AppUser)
                .HasForeignKey(a => a.AppUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}