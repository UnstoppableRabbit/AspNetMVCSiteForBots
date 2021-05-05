using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TeleBot.Models;

namespace TeleBot.ViewModels
{
    public class ChangeModel
    {

        [Required(ErrorMessage = "Не указанa фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        public Person user { get; set; }
    }
}
