using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class UserProfileConfiguration : EntityTypeConfiguration<UserProfile> {
        public UserProfileConfiguration() {
            Property(up => up.RealName)
                .IsRequired()
                .HasMaxLength(50);

            Property(up => up.Gender)
                .IsRequired();

            Property(up => up.Email)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}