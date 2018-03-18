using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class ActivityViewModel {
        public DateTime DateTime { get; set; }
        public Topic Topic { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}