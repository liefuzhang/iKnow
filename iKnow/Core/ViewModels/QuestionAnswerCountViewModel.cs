﻿using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class QuestionAnswerCountViewModel {
        public IDictionary<Question, int> QuestionsWithAnswerCount { get; set; }
    }
}