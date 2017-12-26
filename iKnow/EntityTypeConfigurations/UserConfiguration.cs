using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class UserConfiguration : EntityTypeConfiguration<User> {
        public UserConfiguration() {
            Property(u => u.LoginName)
                .IsRequired()
                .HasMaxLength(255);

            HasRequired(u => u.UserProfile)
                .WithRequiredPrincipal(up => up.User);


            HasMany(u => u.Questions)
                .WithRequired(q => q.User)
                .HasForeignKey(q => q.UserId)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Topics)
                .WithMany(t => t.Users)
                .Map(m => {
                    m.ToTable("TopicUsers");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TopicId");
                });

            HasMany(u => u.Answers)
                .WithRequired(a => a.User)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}