using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class AppUserConfiguration : EntityTypeConfiguration<AppUser> {
        public AppUserConfiguration() {
            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(u => u.Questions)
                .WithRequired(q => q.AppUser)
                .HasForeignKey(q => q.AppUserId)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Topics)
                .WithMany(t => t.AppUsers)
                .Map(m => {
                    m.ToTable("TopicUsers");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TopicId");
                });

            HasMany(u => u.Answers)
                .WithRequired(a => a.AppUser)
                .HasForeignKey(a => a.AppUserId)
                .WillCascadeOnDelete(false);
        }
    }
}