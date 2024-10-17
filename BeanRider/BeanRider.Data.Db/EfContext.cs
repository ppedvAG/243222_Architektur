using BeanRider.Model.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

        readonly string _conString;

        public EfContext(string conString)
        {
            _conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString).UseLazyLoadingProxies().LogTo(x => Debug.WriteLine(x));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().UseTptMappingStrategy();

            modelBuilder.Entity<Customer>().HasMany(x => x.Orders).WithOne(x => x.Customer).OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Food>().Property(x => x.Name).HasColumnName("FoodName").HasMaxLength(230);

            modelBuilder.Entity<Customer>().Property(x => x.Modified).IsConcurrencyToken();
            //modelBuilder.<Entity>().Property(x => x.Modified).IsConcurrencyToken();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        
                        e.Created = e.Modified = System.DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.Modified = System.DateTime.Now;
                    }
                }
            }

            return base.SaveChanges();
        }

    }

}
