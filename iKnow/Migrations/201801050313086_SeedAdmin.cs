namespace iKnow.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedAdmin : DbMigration {
        public override void Up() {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
                INSERT INTO [dbo].[AspNetUsers] ([Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Id], [FirstName], [LastName]) VALUES (N'admin@iknow.com', 0, N'AGHavoBlyA8ig1KDoULGzNMAOBNgefN2UDDInB88yDCP3AS9ZRbKaLQg4RYBBXEDoQ==', N'32e1d587-46f6-421d-9db9-48f6ecb6926d', NULL, 0, 0, NULL, 0, 0, N'admin@iknow.com', N'0d38b290-c52b-4e7f-9117-c10db0fdd349', N'Admin', N'IKnow')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0d38b290-c52b-4e7f-9117-c10db0fdd349', N'1')    
            ");
        }

        public override void Down() {
        }
    }
}
