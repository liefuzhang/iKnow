﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using iKnow.Core.Models;

namespace iKnow.ViewModels {
    public class AnswerDetailViewModel {
        public QuestionDetailViewModel QuestionDetailViewModel { get; set; }
        public Answer Answer { get; set; }
        public int AnswerCount { get; set; }
    }
}