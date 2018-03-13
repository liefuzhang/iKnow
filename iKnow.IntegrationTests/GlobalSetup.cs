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
            MigrateDbToLatestVersion();
            Seed();
        }

        private static void MigrateDbToLatestVersion() {
            // Create and upgrade database to the lastest version if it's not already
            var configuration = new iKnow.Persistence.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }

        public void Seed() {
            var context = new iKnowContext();

            if (!context.Users.Any(u => u.FirstName == "user1first")) {
                context.Users.Add(new AppUser {
                    FirstName = "user1first",
                    LastName = "user1last",
                    UserName = "user1firstuser1last0",
                    Email = "-",
                    PasswordHash = "-"
                });
                context.Users.Add(new AppUser {
                    FirstName = "user2first",
                    LastName = "user2last",
                    UserName = "user2firstuser2last0",
                    Email = "-",
                    PasswordHash = "-"
                });
            }

            context.SaveChanges();
        }
    }
}
