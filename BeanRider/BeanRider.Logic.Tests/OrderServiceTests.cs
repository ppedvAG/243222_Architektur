using BeanRider.Model;
using FluentAssertions;

namespace BeanRider.Logic.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CalculateTotalPrice_should_throw_ArgumentEx_if_order_is_null()
        {
            var orderService = new OrderService();

            Action act = () => orderService.CalculateTotalPrice(null!);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void CalculateTotalPrice_should_return_0_if_order_has_no_items()
        {
            var orderService = new OrderService();
            var order = new Order() { Customer = new Customer() { Name = "Fred", Number = "4" } };
            var totalPrice = orderService.CalculateTotalPrice(order);
            totalPrice.Should().Be(0);
        }

        [Fact]
        public void CalculateTotalPrice_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var orderService = new OrderService();
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

    }
}
