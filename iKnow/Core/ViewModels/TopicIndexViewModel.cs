using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class TopicIndexViewModel {
        public IEnumerable<Topic> Topics{ get; set; }
        public Topic SelectedTopic { get; set; }
    }
}