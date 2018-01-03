using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;
using System.ComponentModel.DataAnnotations;

namespace iKnow.ViewModels {
    public class HomeViewModel {
        public IDictionary<Question, Answer> QuestionAnswers { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}