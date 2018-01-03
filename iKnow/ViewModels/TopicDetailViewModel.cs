using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;
using System.ComponentModel.DataAnnotations;

namespace iKnow.ViewModels {
    public class TopicDetailViewModel {
        public Topic Topic { get; set; }
        public IDictionary<Question, Answer> QuestionAnswers { get; set; }
    }
}