using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using FluentAssertions;
using Moq;

namespace BeanRider.Logic.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CalculateTotalPrice_should_throw_ArgumentEx_if_order_is_null()
        {
            var orderService = new OrderService(null!);

            Action act = () => orderService.CalculateTotalPrice(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateTotalPrice_should_return_0_if_order_has_no_items()
        {
            var orderService = new OrderService(null!);
            var order = new Order() { Customer = new Customer() { Name = "Fred", Number = "4" } };
            var totalPrice = orderService.CalculateTotalPrice(order);
            totalPrice.Should().Be(0);
        }

        [Fact]
        public void CalculateTotalPrice_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var orderService = new OrderService(null!);
            var order = new Order
            {
                Customer = new Customer() { Name = "Fred", Number = "4" }
            };
            order.Items = new List<OrderItem>
                    {

                        new OrderItem
                        {
                            Food = new Drink { Price = 10,Name="Coffee" },
                            Amount = 2,
                            Order = order
                        },
                        new OrderItem
                        {
                            Food = new Eatable { Price = 5,Name="Cake" },
                            Amount = 3,
                            OrderPrice = 4,
                            Order = order
                        }
                    };

            // Act
            decimal totalPrice = orderService.CalculateTotalPrice(order);

            // Assert
            totalPrice.Should().Be(32);
        }


        [Fact]
        public void GetOpenOrdersThatAreNotVegetarian_2_order_with_1_non_vegetarian_Mit_TestReo()
        {
            var orderService = new OrderService(new TestRepo());

            var result = orderService.GetOpenOrdersThatAreNotVegetarian();

            result.Should().HaveCount(1);
        }

        [Fact]
        public void GetOpenOrdersThatAreNotVegetarian_2_order_with_1_non_vegetarian_moq()
        {
            var customer = new Customer { Name = "Fred", Number = "4" };
            var o1 = new Order() { Customer = customer, Status = OrderStatus.Pending };
            o1.Items = new List<OrderItem> {
                             new OrderItem
                            {
                                Food = new Drink { Vegetarian = true, Name="Bier", Price=4 },
                                Amount = 1,
                                Order = o1
                            }
                };

            var o2 = new Order() { Customer = customer, Status = OrderStatus.Pending };
            o2.Items = new List<OrderItem> {
                            new OrderItem
                            {
                                Food = new Drink { Vegetarian = false, Name="Wein", Price=4 },
                                Amount = 1,
                                Order = o1
                            }
                };
            var repoMock = new Mock<IRepository>();
            //repoMock.Setup(x => x.GetAll<Order>()).Returns(new[] { o1, o2 });
            repoMock.Setup(x => x.Query<Order>()).Returns(new[] { o1, o2 }.AsQueryable());

            var orderService = new OrderService(repoMock.Object);

            var result = orderService.GetOpenOrdersThatAreNotVegetarian();

            result.Should().HaveCount(1);
            repoMock.Verify(x => x.GetAll<Order>(), Times.Never);
            repoMock.Verify(x => x.Query<Order>(), Times.Once);
        }
    }

    class TestRepo : IRepository
    {
        public void Add<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public Customer CustomerWithMostUmsatz()
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            if (typeof(T) == typeof(Order))
            {
                var customer = new Customer { Name = "Fred", Number = "4" };
                var o1 = new Order() { Customer = customer, Status = OrderStatus.Pending };
                o1.Items = new List<OrderItem> {
                             new OrderItem
                            {
                                Food = new Drink { Vegetarian = true, Name="Bier", Price=4 },
                                Amount = 1,
                                Order = o1
                            }
                };

                var o2 = new Order() { Customer = customer, Status = OrderStatus.Pending };
                o2.Items = new List<OrderItem> {
                            new OrderItem
                            {
                                Food = new Drink { Vegetarian = false, Name="Wein", Price=4 },
                                Amount = 1,
                                Order = o1
                            }
                };
                return new[] { o1, o2 }.Cast<T>();
            }

            throw new NotImplementedException();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            if (typeof(T) == typeof(Order))
                return GetAll<T>().AsQueryable();   

            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}
