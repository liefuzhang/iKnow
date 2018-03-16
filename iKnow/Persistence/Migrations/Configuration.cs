using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<iKnowContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Persistence\Migrations";
            ContextKey = "iKnow.Persistence.Migrations.Configuration";
        }

        protected override void Seed(iKnowContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
