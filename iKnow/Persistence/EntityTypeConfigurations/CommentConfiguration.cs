using iKnow.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment> {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(a => a.Content)
                .IsRequired();
        }
    }
}