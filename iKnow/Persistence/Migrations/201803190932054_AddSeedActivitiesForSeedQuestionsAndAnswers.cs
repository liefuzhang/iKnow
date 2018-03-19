namespace iKnow.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeedActivitiesForSeedQuestionsAndAnswers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
DELETE FROM [dbo].[Activities]
SET IDENTITY_INSERT [dbo].[Activities] ON

INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (1, N'fe8c5539-5945-4cfb-9892-6cb4d3935c90', 3, 0, 18, 0, N'2017-05-19 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (2, N'fe8c5539-5945-4cfb-9892-6cb4d3935c90', 3, 0, 19, 0, N'2017-11-11 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (3, N'aa32eb82-d698-468e-8c0b-dc3a6bebfc12', 3, 0, 21, 0, N'2017-08-05 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (4, N'fe8c5539-5945-4cfb-9892-6cb4d3935c90', 3, 0, 22, 0, N'2017-11-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (5, N'5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9', 3, 0, 36, 0, N'2017-11-25 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (6, N'5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9', 3, 0, 37, 0, N'2017-11-05 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (7, N'4f3518e8-abf7-457f-b6ab-15e37e99b81b', 3, 0, 38, 0, N'2017-11-11 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (8, N'4f3518e8-abf7-457f-b6ab-15e37e99b81b', 3, 0, 39, 0, N'2017-11-12 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (9, N'75245e73-98c1-4828-a7b7-77c0eefa3d7d', 3, 0, 40, 0, N'2017-11-18 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (10, N'cef53707-04ea-45d5-b7ac-c86f87aa625d', 3, 0, 41, 0, N'2017-12-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (11, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 3, 0, 42, 0, N'2017-10-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (12, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 3, 0, 43, 0, N'2017-09-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (13, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 3, 0, 44, 0, N'2017-08-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (14, N'929981dc-d19d-42df-8e7e-f6896239ff54', 3, 0, 45, 0, N'2017-12-25 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (15, N'02f28cbf-ef25-463f-9c77-901ae627ac54', 3, 0, 46, 0, N'2017-11-09 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (16, N'02f28cbf-ef25-463f-9c77-901ae627ac54', 3, 0, 47, 0, N'2017-11-16 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (17, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 3, 0, 48, 0, N'2017-11-09 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (18, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 3, 0, 49, 0, N'2017-11-17 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (19, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 3, 0, 50, 0, N'2017-11-08 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (20, N'74e14b79-514a-4db0-9de5-61f995daf9bf', 3, 0, 51, 0, N'2017-12-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (21, N'74e14b79-514a-4db0-9de5-61f995daf9bf', 3, 0, 52, 0, N'2017-11-17 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (22, N'cdd5a6a2-cb0d-409d-8cf8-95593d888423', 3, 0, 53, 0, N'2017-11-14 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (23, N'cdd5a6a2-cb0d-409d-8cf8-95593d888423', 3, 0, 54, 0, N'2017-11-11 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (24, N'1a698814-ab5b-467e-b243-ece7a100e291', 3, 0, 55, 0, N'2017-11-12 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (25, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 3, 0, 56, 0, N'2017-11-15 19:29:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (26, N'5bece49d-76a0-4184-88f7-9e420fec1666', 3, 0, 57, 0, N'2017-11-12 19:29:00')

INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (27, N'aa32eb82-d698-468e-8c0b-dc3a6bebfc12', 2, 0, 19, 16, N'2018-01-17 16:34:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (28, N'aa32eb82-d698-468e-8c0b-dc3a6bebfc12', 2, 0, 21, 17, N'2018-01-11 16:42:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (29, N'eb5d625e-aa8f-4709-a296-f92b1df0dd3e', 2, 0, 22, 18, N'2017-12-17 17:13:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (30, N'e40ee71a-e3de-4b33-a877-0cc91fdcb337', 2, 0, 22, 19, N'2018-01-02 19:06:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (31, N'5cd5bc4d-66e9-4c6d-bbf1-a0214f2f71e9', 2, 0, 36, 37, N'2017-12-17 20:05:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (32, N'174b4461-5b84-40da-bba1-71a45bf0d6a3', 2, 0, 37, 38, N'2018-01-19 20:16:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (33, N'b6072fcd-7589-46f6-841a-d3c2adb8323d', 2, 0, 37, 39, N'2018-02-17 20:31:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (34, N'4f3518e8-abf7-457f-b6ab-15e37e99b81b', 2, 0, 38, 40, N'2018-02-10 20:45:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (35, N'4f3518e8-abf7-457f-b6ab-15e37e99b81b', 2, 0, 39, 41, N'2017-12-19 20:53:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (36, N'75245e73-98c1-4828-a7b7-77c0eefa3d7d', 2, 0, 39, 42, N'2018-02-17 20:57:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (37, N'cef53707-04ea-45d5-b7ac-c86f87aa625d', 2, 0, 40, 43, N'2018-02-01 21:06:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (38, N'cef53707-04ea-45d5-b7ac-c86f87aa625d', 2, 0, 41, 44, N'2018-03-17 21:11:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (39, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 2, 0, 42, 45, N'2018-01-08 21:40:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (40, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 2, 0, 43, 46, N'2018-01-01 21:54:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (41, N'd29c1e65-8eff-4459-bbce-a5f39714e011', 2, 0, 44, 47, N'2017-12-27 21:57:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (42, N'929981dc-d19d-42df-8e7e-f6896239ff54', 2, 0, 45, 48, N'2017-11-17 22:05:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (43, N'929981dc-d19d-42df-8e7e-f6896239ff54', 2, 0, 37, 49, N'2018-01-10 22:05:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (44, N'02f28cbf-ef25-463f-9c77-901ae627ac54', 2, 0, 46, 50, N'2018-01-10 22:13:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (45, N'02f28cbf-ef25-463f-9c77-901ae627ac54', 2, 0, 47, 51, N'2018-01-18 22:24:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (46, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 2, 0, 48, 52, N'2018-01-10 22:27:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (47, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 2, 0, 49, 53, N'2018-02-17 22:31:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (48, N'bd3c0d34-47aa-4eb0-b003-f137b139b3f8', 2, 0, 50, 54, N'2018-02-07 22:33:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (49, N'3af7e93a-7873-41fb-b991-43b89126f635', 2, 0, 49, 55, N'2018-01-19 22:40:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (50, N'74e14b79-514a-4db0-9de5-61f995daf9bf', 2, 0, 51, 56, N'2018-01-28 22:46:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (51, N'74e14b79-514a-4db0-9de5-61f995daf9bf', 2, 0, 52, 57, N'2018-01-20 22:49:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (52, N'cdd5a6a2-cb0d-409d-8cf8-95593d888423', 2, 0, 53, 58, N'2017-11-17 23:08:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (53, N'cdd5a6a2-cb0d-409d-8cf8-95593d888423', 2, 0, 54, 59, N'2018-01-29 23:12:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (54, N'1a698814-ab5b-467e-b243-ece7a100e291', 2, 0, 55, 60, N'2018-03-17 23:17:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (55, N'127317d1-c913-403f-ac63-dbfb4b6c0d3a', 2, 0, 56, 61, N'2018-02-17 23:21:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (56, N'ad3ffb5d-95b1-4528-a0a5-92591a21de34', 2, 0, 56, 62, N'2018-02-17 23:24:00')
INSERT INTO [dbo].[Activities] ([Id], [AppUserId], [Type], [TopicId], [QuestionId], [AnswerId], [DateTime]) VALUES (57, N'5bece49d-76a0-4184-88f7-9e420fec1666', 2, 0, 57, 63, N'2018-02-17 23:40:00')

SET IDENTITY_INSERT [dbo].[Activities] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}
