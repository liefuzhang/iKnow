using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;
using System.ComponentModel.DataAnnotations;

namespace iKnow.ViewModels {
    public class AnswerDetailViewModel {
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public int AnswerCount { get; set; }
    }
}