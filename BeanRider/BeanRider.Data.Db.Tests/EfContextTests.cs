using AutoFixture;
using AutoFixture.Kernel;
using BeanRider.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BeanRider.Data.Db.Tests
{
    public class EfContextTests
    {
        string conString = "Server=(localdb)\\mssqllocaldb;Database=BeanRider_Tests;Trusted_Connection=True;";

        [Fact]
        public void Can_create_DB()
        {
            var con = new EfContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
        }

        [Fact]
        public void Can_add_Drink()
        {
            var con = new EfContext(conString);
            con.Database.EnsureCreated();
            var coffee = new Drink()
            {
                Price = 2.5m,
                Name = "Coffee",
                Volume = 0.2,
                AutoBrew = false
            };

            con.Drinks.Add(coffee);
            var rows = con.SaveChanges();

            Assert.Equal(2, rows);
        }

        [Fact]
        public void Can_read_Drink()
        {
            var coffee = new Drink() { Price = 2.2m, Name = $"Coffee_{Guid.NewGuid()}" };
            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Drinks.Add(coffee);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Drinks.Find(coffee.Id);

                Assert.NotNull(loaded);
                Assert.Equal(coffee.Name, loaded.Name);
            }
        }


        [Fact]
        public void Can_update_Drink()
        {
            var coffee = new Drink() { Price = 2.6m, Name = $"Coffee_{Guid.NewGuid()}" };
            var newName = $"Latte_{Guid.NewGuid()}";
            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Drinks.Add(coffee);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Drinks.Find(coffee.Id);
                loaded!.Name = newName;
                var rows = con.SaveChanges();
                Assert.Equal(1, rows);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Drinks.Find(coffee.Id);
                Assert.Equal(newName, loaded!.Name);
            }
        }

        [Fact]
        public void Can_delete_Drink()
        {
            var coffee = new Drink() { Price = 2.7m, Name = $"Coffee_{Guid.NewGuid()}" };
            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Drinks.Add(coffee);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Drinks.Find(coffee.Id);
                con.Remove(loaded!);
                var rows = con.SaveChanges();
                Assert.Equal(2, rows);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Drinks.Find(coffee.Id);
                Assert.Null(loaded);
            }
        }

        [Fact]
        public void Can_create_Order_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            fix.Customizations.Add(new TypeRelay(typeof(Food), typeof(Eatable)));
            //fix.Customize<Order>(c => c.Without(x => x.Id)); // Setzt die Order-ID nicht
            //fix.Customize<OrderItem>(c => c.Without(x => x.Id)); // Für OrderItem
            //fix.Customize<Customer>(c => c.Without(x => x.Id)); // Für Customer
            //fix.Customize<Eatable>(c => c.Without(x => x.Id)); // Für Food
            fix.Customizations.Add(new PropertyNameOmitter(nameof(Entity.Id), nameof(Entity.Created)));
            var order = fix.Create<Order>();

            using (var con = new EfContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(order);
                var rows = con.SaveChanges();
                rows.Should().BeGreaterThan(10);
                //Assert.Equal(11, rows);
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Orders.Find(order.Id); // lazy lodading, wennn UseLazyLoadingProxies() in config

                //eager loading
                //var loadQuery = con.Orders.Where(x => x.Id == order.Id);
                //loadQuery = loadQuery.Include(x => x.Customer);
                //loadQuery = loadQuery.Include(x => x.Items).ThenInclude(x => x.Food);
                //var loaded = loadQuery.FirstOrDefault();

                //explizit loading
                //var loaded = con.Orders.Find(order.Id);
                //con.Entry(loaded).Reference(x=>x.Customer).Load();
                //con.Entry(loaded).Collection(x => x.Items).Load();

                loaded.Should().NotBeNull().And.BeEquivalentTo(order, x => x.IgnoringCyclicReferences());
            }
        }

        [Fact]
        public void Customer_can_not_be_deleted_if_it_has_orders()
        {
            var customer = new Customer() { Name = "Hans", Number = "123" };
            var order = new Order() { Customer = customer };
            using (var con = new EfContext(conString))
            {
                con.Add(customer);
                con.Add(order);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Customers.Find(customer.Id);
                con.Remove(loaded!);

                Action act = () => con.SaveChanges();
                act.Should().Throw<DbUpdateException>();
            }
        }

        [Fact]
        public void Customer_can_be_deleted_if_order_are_purged()
        {
            var customer = new Customer() { Name = "Fritz", Number = "321" };
            var order = new Order() { Customer = customer };

            using (var con = new EfContext(conString))
            {
                con.Add(customer);
                con.Add(order);
                con.SaveChanges();
            }

            using (var con = new EfContext(conString))
            {
                var loaded = con.Customers.Find(customer.Id);
                foreach (var item in loaded!.Orders)
                {
                    con.Remove(item);
                }
                con.Remove(loaded!);

                Action act = () => con.SaveChanges();
                act.Should().NotThrow<DbUpdateException>();
            }
        }

        [Fact]
        public void Changing_Customer_by_2_users_should_throw_Ex__testing_Optimistic_Concurrency()
        {
            var customer = new Customer() { Name = "Werner", Number = "321" };
            using (var con = new EfContext(conString))
            {
                con.Add(customer);
                con.SaveChanges();
            }

            using var con1 = new EfContext(conString);
            var loaded1 = con1.Customers.Find(customer.Id);

            using var con2 = new EfContext(conString);
            var loaded2 = con2.Customers.Find(customer.Id);

            loaded1.Name = "Fred";
            loaded2.Name = "Wilma";

            con1.SaveChanges();
            Action act = () => con2.SaveChanges();
            act.Should().Throw<DbUpdateConcurrencyException>();

        }
    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}