using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKnow.Core.Models;

namespace iKnow.ViewModels {
    public class TopicIndexViewModel {
        public IEnumerable<Topic> Topics{ get; set; }
        public Topic SelectedTopic { get; set; }
    }
}