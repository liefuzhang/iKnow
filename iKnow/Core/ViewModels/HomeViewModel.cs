﻿using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class HomeViewModel {
        public IDictionary<Question, Answer> QuestionAnswers { get; set; }
    }
}