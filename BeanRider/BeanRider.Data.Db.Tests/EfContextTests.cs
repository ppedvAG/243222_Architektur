using BeanRider.Model;

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
    }
}