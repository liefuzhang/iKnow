using System.Data.Entity.Migrations;

namespace iKnow.Persistence.Migrations
{
    public partial class AddDefaultIconNumberToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DefaultIconNumber", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DefaultIconNumber");
        }
    }
}
