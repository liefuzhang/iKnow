using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;
using System.ComponentModel.DataAnnotations;

namespace iKnow.ViewModels {
    public class SearchResultViewModel {
        public IEnumerable<Topic> Topics { get; set; }
        public IDictionary<Question, int> QuestionsWithAnswerCount { get; set; }
    }
}