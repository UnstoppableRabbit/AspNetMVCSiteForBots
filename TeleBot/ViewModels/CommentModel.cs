using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeleBot.Models;

namespace TeleBot.ViewModels
{
    public class CommentModel
    {
        [Required(ErrorMessage = "Не указан комментарий")]
        public string Comment { get; set; }

        public List<comModel> allComs { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }

    }
}
