using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class TopicQuestionConfiguration : IEntityTypeConfiguration<TopicQuestion> {
        public void Configure(EntityTypeBuilder<TopicQuestion> builder)
        {
            builder.HasKey(b => new {b.QuestionId, b.TopicId});

            builder.HasOne(b => b.Question)
                .WithMany(b => b.TopicQuestions)
                .HasForeignKey(b => b.QuestionId);
            builder.HasOne(b => b.Topic)
                .WithMany(b => b.TopicQuestions)
                .HasForeignKey(b => b.TopicId);
        }
    }
}