using System;

namespace iKnow.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int? ReplyToCommentId { get; set; }
        public Comment ReplyToComment { get; set; }

    }
}