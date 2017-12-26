using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class TopicConfiguration : EntityTypeConfiguration<Topic> {
        public TopicConfiguration() {
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasMaxLength(1000);
        }
    }
}