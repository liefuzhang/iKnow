using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;

namespace iKnow.ViewModels {
    public class AddQuestionViewModel {
        
        // TODO when should this be done?
        public AddQuestionViewModel() {
            Question = new Question();
            PopulateTopics();
        }

        public Question Question { get; set; }
        public int[] TopicIds { get; set; }
        public MultiSelectList Topics { get; set; }

        private void PopulateTopics() {
            using (var context = new iKnowContext()) {
                var topics = context.Topics.Select(t => new {
                    TopicId = t.Id,
                    TopicName = t.Name
                }).ToList();
                Topics = new MultiSelectList(topics, "TopicId", "TopicName");
            }
        }
    }
}