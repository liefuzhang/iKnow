using System.Data.Entity;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;
using iKnow.Core.Models;
using iKnow.Persistence;

namespace iKnow.IntegrationTests {
    [SetUpFixture]
    public class GlobalSetup {
        [OneTimeSetUp]
        public void SetUp() {
            //MigrateDbToLatestVersion();
            //Seed();

            Database.SetInitializer(new TestInitializer());
            
            var context = new iKnowContext();
            context.Database.Initialize(true);

            context.Database.ExecuteSqlCommand("DELETE FROM Activities");
            context.Database.ExecuteSqlCommand("DELETE FROM Answers");
            context.Database.ExecuteSqlCommand("DELETE FROM TopicQuestions");
            context.Database.ExecuteSqlCommand("DELETE FROM Questions");
            context.Database.ExecuteSqlCommand("DELETE FROM TopicUsers");
            context.Database.ExecuteSqlCommand("DELETE FROM Topics");
            context.Database.ExecuteSqlCommand("DELETE FROM AspNetUsers");
            context.Database.ExecuteSqlCommand("DELETE FROM TopicFollowings");
            context.Database.ExecuteSqlCommand("INSERT INTO AspNetUsers (Id, FirstName, LastName, UserName, Email, PasswordHash) VALUES ('1', 'user1first', 'user1last', 'user1firstuser1last0', '-', '-')");
            context.Database.ExecuteSqlCommand("INSERT INTO AspNetUsers (Id, FirstName, LastName, UserName, Email, PasswordHash) VALUES ('2', 'user2first', 'user2last', 'user2firstuser2last0', '-', '-')");
        }

        //    private static void MigrateDbToLatestVersion() {
        //        // Create and upgrade database to the lastest version if it's not already
        //        var configuration = new iKnow.Persistence.Migrations.Configuration();
        //        var migrator = new DbMigrator(configuration);
        //        migrator.Update();
        //    }

        //    public void Seed() {
        //        var context = new iKnowContext();

        //        if (!context.Users.Any(u => u.FirstName == "user1first")) {
        //            context.Users.Add(new AppUser {
        //                FirstName = "user1first",
        //                LastName = "user1last",
        //                UserName = "user1firstuser1last0",
        //                Email = "-",
        //                PasswordHash = "-"
        //            });
        //            context.Users.Add(new AppUser {
        //                FirstName = "user2first",
        //                LastName = "user2last",
        //                UserName = "user2firstuser2last0",
        //                Email = "-",
        //                PasswordHash = "-"
        //            });
        //        }

        //        context.SaveChanges();
        //    }
        //}
    }

    public class TestInitializer : DropCreateDatabaseAlways<iKnowContext> {
        //protected override void Seed(iKnowContext context) {
        //    context.Users.AddOrUpdate(new AppUser {
        //        FirstName = "user1first",
        //        LastName = "user1last",
        //        UserName = "user1firstuser1last0",
        //        Email = "-",
        //        PasswordHash = "-"
        //    });
        //    context.Users.AddOrUpdate(new AppUser {
        //        FirstName = "user2first",
        //        LastName = "user2last",
        //        UserName = "user2firstuser2last0",
        //        Email = "-",
        //        PasswordHash = "-"
        //    });

        //    base.Seed(context);
        //}
    }
}
