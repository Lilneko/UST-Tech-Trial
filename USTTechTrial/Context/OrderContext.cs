using Microsoft.EntityFrameworkCore;
using USTTechTrial.Models;

namespace USTTechTrial.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {}

        public DbSet<Order> Orders {get; set;}
        public DbSet<Item> Items {get; set;}
    }

}