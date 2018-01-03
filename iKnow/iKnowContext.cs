using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using iKnow.Models;
using iKnow.EntityTypeConfiguration;

namespace iKnow {
    public class iKnowContext : DbContext {
        public iKnowContext(): base("DefaultConnection") {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}