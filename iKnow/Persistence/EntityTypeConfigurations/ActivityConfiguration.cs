using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class ActivityConfiguration : EntityTypeConfiguration<Activity> {
        public ActivityConfiguration() {
            Property(a => a.DateTime)
                .IsRequired();
        }
    }
}