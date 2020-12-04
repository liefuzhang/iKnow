using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question> {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(q => q.Description)
                .HasMaxLength(1000);
        }
    }
}