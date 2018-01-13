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

        public string PlainContent {
            get {
                var parsed = Regex.Replace(Content, "</(p|li|h2|blockquote)>", " ");
                return Regex.Replace(parsed, "<.*?>", String.Empty).Trim();
            }
        }

        public string ShortContent
            => PlainContent.Length > Constants.ShortAnswerLenth
                ? PlainContent.Substring(0, Constants.ShortAnswerLenth) + "..."
                : PlainContent;

        public string ShortContentImageData {
            get {
                var match = Regex.Match(Content, "<img src=\\\"data:.*?\">");
                return match.Success ? match.Value : null;
            }
        }
    }
}