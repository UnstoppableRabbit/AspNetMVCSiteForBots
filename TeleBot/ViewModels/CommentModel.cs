using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeleBot.Models;

namespace TeleBot.ViewModels
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Не указан комментарий")]
        public string Comment { get; set; }

        public List<Comment> allComs = new List<Comment>();
        public int CurrentPage;
        public int MaxPage;

    }
}
