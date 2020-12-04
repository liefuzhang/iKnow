using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class TopicUserConfiguration : IEntityTypeConfiguration<TopicUser> {
        public void Configure(EntityTypeBuilder<TopicUser> builder)
        {
            builder.HasKey(b => new {b.UserId, b.TopicId});

            builder.HasOne(b => b.User)
                .WithMany(b => b.TopicUsers)
                .HasForeignKey(b => b.UserId);
            builder.HasOne(b => b.Topic)
                .WithMany(b => b.TopicUsers)
                .HasForeignKey(b => b.TopicId);
        }
    }
}