using iKnow.Core.Models;
using iKnow.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace iKnow.Persistence
{
    public class iKnowContext : IdentityDbContext<AppUser>
    {
        public iKnowContext(DbContextOptions<iKnowContext> options)
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicFollowing> TopicFollowings { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AnswerLike> AnswerLikes { get; set; }
        public DbSet<TopicQuestion> TopicQuestions { get; set; }
        public DbSet<TopicUser> TopicUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}