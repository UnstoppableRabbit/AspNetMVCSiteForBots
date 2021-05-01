using System.Collections.Generic;

namespace TeleBot.Models
{
    public class Person
    {
        public int    PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }

        public List<Order>   orders { get; set; }
        public List<Comment> comments { get; set; }
    }
}
