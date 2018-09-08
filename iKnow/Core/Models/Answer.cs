﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace iKnow.Core.Models {
    public class Answer {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; private set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Answer()
        {
            Comments = new HashSet<Comment>();
        }

        public string PlainContent {
            get {
                var parsed = Regex.Replace(Content, "</(p|li|h2|blockquote)>", " ");
                return Regex.Replace(parsed, "<.*?>", String.Empty).Trim();
            }
        }

        public string ShortContent
            => PlainContent.Length > Constants.ShortAnswerLength
                ? PlainContent.Substring(0, Constants.ShortAnswerLength) + "..."
                : PlainContent;

        public string ShortContentImageData {
            get {
                var match = Regex.Match(Content, "<img src=\".*?\">");
                return match.Success ? match.Value : null;
            }
        }

        public void UpdateContent(string content) {
            Content = content;
            UpdatedDate = DateTime.Now;
        }
    }
}