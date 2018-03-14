using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iKnow.Core.Models;
using iKnow.Persistence;

namespace iKnow.IntegrationTests.Extensions {
    public static class DbContextExtensions {
        public static Topic AddTestTopicToDatabase(this iKnowContext context, string name = "Test Topic") {
            var topic = new Topic {
                Name = name
            };

            context.Topics.Add(topic);
            context.SaveChanges();

            context.Entry(topic).Reload();
            return topic;
        }

        public static Question AddTestQuestionToDatabase(this iKnowContext context, string title = "Test question?") {
            var userId = context.Users.First().Id;

            var question = new Question {
                Title =  title,
            };
            question.SetUserId(userId);

            context.Questions.Add(question);
            context.SaveChanges();

            context.Entry(question).Reload();
            return question;
        }

        public static Answer AddTestAnswerToDatabase(this iKnowContext context, int questionId, string content = "Test answer") {
            var userId = context.Users.First().Id;
            var answer   = new Answer() {
                Content = content,
                AppUserId = userId,
                QuestionId = questionId,
                CreatedDate = DateTime.Now
            };

            context.Answers.Add(answer);
            context.SaveChanges();

            context.Entry(answer).Reload();
            return answer;
        }
    }
}
