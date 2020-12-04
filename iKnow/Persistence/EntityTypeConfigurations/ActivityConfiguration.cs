using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class ActivityConfiguration : IEntityTypeConfiguration<Activity> {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.Property(a => a.DateTime)
                .IsRequired();
        }
    }
}