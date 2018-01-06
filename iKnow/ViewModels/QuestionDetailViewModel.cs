using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;
using System.ComponentModel.DataAnnotations;

namespace iKnow.ViewModels {
    public class QuestionDetailViewModel {
        public Question Question { get; set; }
        [AllowHtml]
        [Required]
        public string AnswerContent { get; set; }
        public bool CanUserEdit { get; set; }
        public int UserAnswerId { get; set; }
    }
}