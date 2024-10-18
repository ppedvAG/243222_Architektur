using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using BeanRider.UI.Blazor.Components.Pages;
using Moq;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using BeanRider.Logic;
using AngleSharp.Html.Dom;



namespace BeanRider.UI.Blazor.Tests.Components.Pages
{
    public class OrdersTests : TestContext
    {
        [Fact]
        public void Should_ShowLoadingMessage_When_OrdersAreNull()
        {
            // Arrange: Set up the repository mock to return null
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAll<Order>()).Returns((IEnumerable<Order>)null);
            
            var orderServiceMock = new Mock<IOrderService>();

            // Inject the mock service
            Services.AddSingleton(repoMock.Object);
            Services.AddSingleton(orderServiceMock.Object);

            // Act: Render the component
            var cut = RenderComponent<Orders>();

            

            // Assert: Verify that the loading message is displayed
            cut.MarkupMatches(@"
        <h3>Orders</h3>
        <p>Loading....</p>");
        }


        [Fact]
        public void Should_RenderOrders_When_OrdersAreAvailable()
        {
            // Arrange: Create some test orders
            var testOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
                Time = DateTime.Now,
                Customer = new Customer { Name = "John Doe",Number="1" },
                Items = new List<OrderItem> { new OrderItem { Amount = 2, Order = null, Food = null } }
            },
            new Order
            {
                Id = 2,
                Time = DateTime.Now,
                Customer = new Customer { Name = "Jane Smith",Number="2" },
                Items = new List<OrderItem> { new OrderItem { Amount = 3,Order=null,Food=null } }
            }
        };

            // Set up the repository mock to return the test orders
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.GetAll<Order>()).Returns(testOrders);
            var orderServiceMock = new Mock<IOrderService>();
            


            // Inject the mock repository
            Services.AddSingleton(repoMock.Object);
            Services.AddSingleton(orderServiceMock.Object);

            // Act: Render the component
            var cut = RenderComponent<Orders>();

            // Assert: Verify that the table is rendered with the correct data
            cut.Find("table").MarkupMatches(@"
            <table class=""table"">
                <thead>
                    <tr>
                        <th>Order ID</th>
                        <th>Order Date</th>
                        <th>Customer Name</th>
                        <th>Order items count</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>TIME_PLACEHOLDER</td>
                        <td>John Doe</td>
                        <td>2 Dinge</td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>TIME_PLACEHOLDER</td>
                        <td>Jane Smith</td>
                        <td>3 Dinge</td>
                    </tr>
                </tbody>
            </table>
        ");
        }
    }
}
