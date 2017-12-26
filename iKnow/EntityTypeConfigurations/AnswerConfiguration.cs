using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class AnswerConfiguration : EntityTypeConfiguration<Answer> {
        public AnswerConfiguration() {
            Property(a => a.Content)
                .IsRequired()
                .HasMaxLength(5000);
        }
    }
}