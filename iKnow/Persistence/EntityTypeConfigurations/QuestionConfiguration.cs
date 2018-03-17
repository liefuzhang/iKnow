using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class QuestionConfiguration : EntityTypeConfiguration<Question> {
        public QuestionConfiguration() {
            Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(255);

            Property(q => q.Description)
                .HasMaxLength(1000);

            HasMany(q => q.Topics)
                .WithMany(t => t.Questions)
                .Map(m => {
                    m.ToTable("TopicQuestions");
                    m.MapLeftKey("TopicId");
                    m.MapRightKey("QuestionId");
                });
        }
    }
}