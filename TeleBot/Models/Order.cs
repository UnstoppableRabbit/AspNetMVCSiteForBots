using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeleBot.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderDesc { get; set; }
        public string Status { get; set; }
        public int PersonId { get; set; }
        public Person person { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
