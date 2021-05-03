using System;

namespace TeleBot.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int PersonId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
