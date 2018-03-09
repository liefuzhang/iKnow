using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public partial class AddSomePropertiesToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Intro", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Byte(nullable: false));
            AddColumn("dbo.AspNetUsers", "Location", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Location");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Intro");
        }
    }
}
