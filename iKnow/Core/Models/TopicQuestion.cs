using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iKnow.Core.Models
{
    public class TopicQuestion
    {
        public TopicQuestion()
        {
        }

        public TopicQuestion(Topic topic, Question question)
        {
            Topic = topic;
            TopicId = topic.Id;
            Question = question;
            QuestionId = question.Id;
        }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
