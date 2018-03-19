namespace iKnow.Persistence.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddFollowTopicSeedData : DbMigration {
        public override void Up() {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Activities] ON

INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (58, N'02f28cbf-ef25-463f-9c77-901ae627ac54', 1, 24, 0, 0, N'2018-02-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (59, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 1, 26, 0, 0, N'2018-01-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (60, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 1, 18, 0, 0, N'2018-03-11 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (61, N'174b4461-5b84-40da-bba1-71a45bf0d6a3', 1, 34, 0, 0, N'2018-03-12 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (62, N'1a698814-ab5b-467e-b243-ece7a100e291', 1, 30, 0, 0, N'2018-03-16 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (63, N'1a698814-ab5b-467e-b243-ece7a100e291', 1, 28, 0, 0, N'2018-02-01 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (64, N'b6072fcd-7589-46f6-841a-d3c2adb8323d', 1, 29, 0, 0, N'2018-03-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (65, N'5bece49d-76a0-4184-88f7-9e420fec1666', 1, 19, 0, 0, N'2018-02-01 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (66, N'ad3ffb5d-95b1-4528-a0a5-92591a21de34', 1, 20, 0, 0, N'2018-01-02 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (67, N'75245e73-98c1-4828-a7b7-77c0eefa3d7d', 1, 31, 0, 0, N'2018-03-12 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (68, N'b6072fcd-7589-46f6-841a-d3c2adb8323d', 1, 39, 0, 0, N'2018-01-01 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (69, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 1, 14, 0, 0, N'2018-03-06 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (70, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 1, 16, 0, 0, N'2017-09-06 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (71, N'75245e73-98c1-4828-a7b7-77c0eefa3d7d', 1, 18, 0, 0, N'2017-03-06 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (72, N'b6072fcd-7589-46f6-841a-d3c2adb8323d', 1, 27, 0, 0, N'2018-01-09 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (73, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 1, 25, 0, 0, N'2018-03-09 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (74, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 1, 36, 0, 0, N'2017-12-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (75, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 1, 34, 0, 0, N'2017-11-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (76, N'174b4461-5b84-40da-bba1-71a45bf0d6a3', 1, 34, 0, 0, N'2018-03-19 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (77, N'cef53707-04ea-45d5-b7ac-c86f87aa625d', 1, 33, 0, 0, N'2018-10-16 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (78, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 1, 40, 0, 0, N'2018-03-16 22:56:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (79, N'3af7e93a-7873-41fb-b991-43b89126f635', 1, 41, 0, 0, N'2018-03-11 22:56:00')

SET IDENTITY_INSERT [dbo].[Activities] OFF
            ");
        }

        public override void Down() {
        }
    }
}
