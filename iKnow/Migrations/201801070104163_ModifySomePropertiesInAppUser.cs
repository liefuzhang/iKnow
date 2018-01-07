namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySomePropertiesInAppUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Intro", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "Location", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Location", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Intro", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
