using Microsoft.EntityFrameworkCore;

namespace TeleBot.Models
{
    public class BotContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Order> Order { get; set; }
        public BotContext(DbContextOptions<BotContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
