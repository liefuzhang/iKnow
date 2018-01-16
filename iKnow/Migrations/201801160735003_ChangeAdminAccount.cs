namespace iKnow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAdminAccount : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                DELETE FROM [dbo].[AspNetUsers] WHERE [Email] = 'admin@iknow.com'
                INSERT INTO [dbo].[AspNetUsers] ([Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Id], [FirstName], [LastName], [Intro], [Gender], [Location]) VALUES (N'liefuzhang@163.com', 0, N'ALpIw9lBhZXL78UFAryzsT7zCWwTwwasJ/uzpq7tsNvN2zImO0gszYbpgsNYhUx6Ug==', N'5429ae79-7c1b-4321-a3fa-fbca9f16d45d', NULL, 0, 0, NULL, 0, 0, N'liefuzhang1', N'e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8', N'Liefu', N'Zhang', NULL, 0, NULL)
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e8deb3b8-da18-4ab7-ad8f-812e65aa6fb8', N'1')    
            ");
        }
        
        public override void Down()
        {
        }
    }
}
