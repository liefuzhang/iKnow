using System.Data.Entity;
using iKnow.Core.Models;
using iKnow.Persistence.EntityTypeConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iKnow.Persistence {
    public class iKnowContext : IdentityDbContext<AppUser> {
        public iKnowContext(): base("DefaultConnection") {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicFollowing> TopicFollowings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());
            modelBuilder.Configurations.Add(new TopicFollowingConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}