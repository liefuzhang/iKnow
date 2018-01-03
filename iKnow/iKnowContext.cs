using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using iKnow.Models;
using iKnow.EntityTypeConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iKnow {
    public class iKnowContext : IdentityDbContext<AppUser> {
        public iKnowContext(): base("DefaultConnection") {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            modelBuilder.Configurations.Add(new QuestionConfiguration());
            modelBuilder.Configurations.Add(new AnswerConfiguration());
            modelBuilder.Configurations.Add(new TopicConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}