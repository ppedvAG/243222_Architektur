using Microsoft.Playwright;
using System.Threading.Tasks;
using Xunit;

namespace BeanRider.UI.Blazor.Tests.Components.Pages
{
    public class OrderPlaytests:IAsyncLifetime
    {
        private IPlaywright _playwright;
        private IBrowser _browser;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false, // Set to false if you want to see the browser window during tests
               
            });
        }

        public async Task DisposeAsync()
        {
            await _browser.DisposeAsync();
            _playwright.Dispose();
        }

        [Fact]
        public async Task OrdersPage_Should_DisplayLoadingMessage_When_OrdersAreNull()
        {
            // Arrange: Set up browser and page
            var page = await _browser.NewPageAsync();

            // Act: Navigate to the Orders page
            await page.GotoAsync("https://localhost:7285/orders");

            // Assert: Verify the "Loading..." message appears when the page is initially loaded
            var loadingMessage = await page.Locator("p").TextContentAsync();
            Assert.Equal("Loading....", loadingMessage);
        }

        [Fact]
        public async Task OrdersPage_Should_DisplayOrders_When_OrdersAreAvailable()
        {
            // Arrange: Set up browser and page
            var page = await _browser.NewPageAsync();

            // Act: Navigate to the Orders page
            await page.GotoAsync("https://localhost:7285/orders");

            // Wait for the orders table to load (you may need to adjust the selector or wait condition)
            await page.WaitForSelectorAsync("table");

            // Assert: Verify that the table has rows for the orders
            var rows = await page.Locator("tbody tr").CountAsync();
            Assert.True(rows > 0, "No rows found in orders table");

            // Assert: Verify specific data in the first order row
            var firstOrderCustomer = await page.Locator("tbody tr:nth-child(1) td:nth-child(3)").TextContentAsync();
            Assert.Equal("Name954edabc-e9da-4f87-a24a-20b35bc7effe", firstOrderCustomer);
        }
    }
}
