using System.Data.Entity;
using iKnow.Core.Models;
using iKnow.Persistence.EntityTypeConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iKnow.Persistence
{
    public class iKnowContext : IdentityDbContext<AppUser>
    {
        public iKnowContext() : base("DefaultConnection")
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicFollowing> TopicFollowings { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AnswerLike> AnswerLikes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());
            modelBuilder.Configurations.Add(new TopicFollowingConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}