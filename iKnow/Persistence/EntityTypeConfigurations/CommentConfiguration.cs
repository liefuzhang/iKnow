using System.Data.Entity.ModelConfiguration;
using iKnow.Core.Models;

namespace iKnow.Persistence.EntityTypeConfigurations {
    internal class CommentConfiguration : EntityTypeConfiguration<Comment> {
        public CommentConfiguration() {
            Property(a => a.Content)
                .IsRequired();
        }
    }
}