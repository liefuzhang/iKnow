using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace iKnow.Core.Models
{
    public class AnswerLike
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }

        public AnswerLike(string userId, int answerId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (answerId <= 0)
            {
                throw new ArgumentException(nameof(answerId));
            }

            AppUserId = userId;
            AnswerId = answerId;
        }

        protected AnswerLike() { }
    }
}