using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class TopicFollowingConfiguration : IEntityTypeConfiguration<TopicFollowing> {
        public void Configure(EntityTypeBuilder<TopicFollowing> builder)
        {
            builder.HasKey(f => new {f.UserId, f.TopicId});
        }
    }
}