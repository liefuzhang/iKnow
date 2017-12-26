using iKnow.Models;
using System.Data.Entity.ModelConfiguration;

namespace iKnow.EntityTypeConfiguration {
    internal class QuestionConfiguration : EntityTypeConfiguration<Question> {
        public QuestionConfiguration() {
            Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(255);

            Property(q => q.Description)
                .HasMaxLength(1000);

            HasMany(q => q.Answers)
                .WithRequired(a => a.Question)
                .HasForeignKey(a => a.QuestionId);

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