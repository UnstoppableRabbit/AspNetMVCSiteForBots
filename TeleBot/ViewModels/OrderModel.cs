using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeleBot.ViewModels
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Опишите вкратце желательную функциональность бота")]
        public string Description { get; set; }
    
    }
}
