using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iKnow.Core.Models;
using iKnow.Persistence;

namespace iKnow.IntegrationTests.Extensions
{
    public static class DbContextExtensions
    {
        public static Topic AddTestTopicToDatabase(this iKnowContext context, string name = "Test Topic")
        {
            var topic = new Topic
            {
                Name = name
            };

            context.Topics.Add(topic);
            context.SaveChanges();

            context.Entry(topic).Reload();
            return topic;
        }

        public static Question AddTestQuestionToDatabase(this iKnowContext context, string title = "Test question?")
        {
            var question = new Question
            {
                Title = title,
            };
            question.SetUserId(context.Users.First().Id);

            context.Questions.Add(question);
            context.SaveChanges();

            context.Entry(question).Reload();
            return question;
        }

        public static Answer AddTestAnswerToDatabase(this iKnowContext context, int questionId, string content = "Test answer",
            string userId = null)
        {
            var answer = new Answer()
            {
                Content = content,
                AppUserId = userId ?? context.Users.First().Id,
                QuestionId = questionId,
                CreatedDate = DateTime.Now
            };

            context.Answers.Add(answer);
            context.SaveChanges();

            context.Entry(answer).Reload();
            return answer;
        }

        public static Comment AddTestCommentToDatabase(this iKnowContext context, int answerId, string content = "Test comment",
            string userId = null)
        {
            var comment = new Comment()
            {
                Content = content,
                AppUserId = userId ?? context.Users.First().Id,
                AnswerId = answerId,
                CreatedDate = DateTime.Now
            };

            context.Comments.Add(comment);
            context.SaveChanges();

            context.Entry(comment).Reload();
            return comment;
        }

        public static TopicFollowing AddTestTopicFollowingToDatabase(this iKnowContext context, int topicId)
        {
            var topicFollowing = new TopicFollowing(context.Users.First().Id, topicId);

            context.TopicFollowings.Add(topicFollowing);
            context.SaveChanges();

            context.Entry(topicFollowing).Reload();
            return topicFollowing;
        }

        public static Activity AddTestActivityTopicFollowingToDatabase(this iKnowContext context, int topicId)
        {
            var activity = Activity.ActivityFollowTopic(context.Users.First().Id, topicId);

            return AddActivity(context, activity);
        }

        public static Activity AddTestActivityAnswerQuestionToDatabase(this iKnowContext context, int questionId, int answerId)
        {
            var activity = Activity.ActivityAnswerQuestion(context.Users.First().Id, questionId, answerId);

            return AddActivity(context, activity);
        }

        public static Activity AddTestActivityAddQuestionToDatabase(this iKnowContext context, int questionId)
        {
            var activity = Activity.ActivityAddQuestion(context.Users.First().Id, questionId);

            return AddActivity(context, activity);
        }

        private static Activity AddActivity(iKnowContext context, Activity activity)
        {
            context.Activities.Add(activity);
            context.SaveChanges();

            context.Entry(activity).Reload();
            return activity;
        }
    }
}
