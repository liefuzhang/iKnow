using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class AnswerConfiguration : EntityTypeConfiguration<Answer> {
        public AnswerConfiguration() {
            Property(a => a.Content)
                .IsRequired();
        }
    }
}