using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class TopicFollowingConfiguration : EntityTypeConfiguration<TopicFollowing> {
        public TopicFollowingConfiguration() {
            HasKey(f => new {f.UserId, f.TopicId});
        }
    }
}