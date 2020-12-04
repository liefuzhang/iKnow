using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class TopicConfiguration : IEntityTypeConfiguration<Topic> {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);
        }
    }
}