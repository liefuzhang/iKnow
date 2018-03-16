using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class FollowingConfiguration : EntityTypeConfiguration<Following> {
        public FollowingConfiguration() {
            HasKey(f => new {f.UserId, f.TopicId});
        }
    }
}