using System.Collections.Generic;
using System.Linq;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels
{
    public class SearchFullResultViewModel
    {
        public AppUser User { get; set; }

        public Topic Topic { get; set; }

        public IDictionary<Question, Answer> QuestionAnswers { get; set; }

        public string Search { get; set; }

        public bool IsEmptyResult =>
            User == null && Topic == null && QuestionAnswers?.FirstOrDefault() == null;

        public string SearchType => string.Empty;
    }
}