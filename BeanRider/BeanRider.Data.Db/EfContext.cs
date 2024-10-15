using BeanRider.Model;
using Microsoft.EntityFrameworkCore;

namespace BeanRider.Data.Db
{
    public class EfContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Eatable> Eatables { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        string _conString;

        public EfContext(string conString)
        {
            _conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString).UseLazyLoadingProxies();
        }

    }
}
