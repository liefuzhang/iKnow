using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class UserConfiguration : EntityTypeConfiguration<AppUser> {
        public UserConfiguration() {
            Property(u => u.LoginName)
                .IsRequired()
                .HasMaxLength(255);

            HasRequired(u => u.UserProfile)
                .WithRequiredPrincipal(up => up.AppUser);


            HasMany(u => u.Questions)
                .WithRequired(q => q.AppUser)
                .HasForeignKey(q => q.UserId)
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
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}