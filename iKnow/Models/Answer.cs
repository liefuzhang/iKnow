using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace iKnow.Models {
    public class Answer {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        private string _plainContent;
        public string PlainContent {
            get {
                if (string.IsNullOrEmpty(_plainContent)) {
                    var parsed = Regex.Replace(Content, "</(p|li|h3)>", "&nbsp;");
                    return Regex.Replace(parsed, "<.*?>", String.Empty);
                } else {
                    return _plainContent;
                }
            }
        }
        public string ShortContext
            => PlainContent.Length > Constants.ShortAnswerLenth ? 
            PlainContent.Substring(0, Constants.ShortAnswerLenth) + "..." : PlainContent;
    }
}