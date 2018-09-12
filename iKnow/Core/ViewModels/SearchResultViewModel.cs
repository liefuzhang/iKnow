using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class SearchResultViewModel {
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
        public IDictionary<Question, int> QuestionsWithAnswerCount { get; set; }
    }
}