using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer> {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(a => a.Content)
                .IsRequired();
        }
    }
}