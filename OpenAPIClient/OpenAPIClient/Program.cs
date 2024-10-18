// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var client = new OpenAPIClient.swaggerClient("https://localhost:7116",new HttpClient());

var orders = await client.OrdersAllAsync();

foreach (var item in orders)
{
    Console.WriteLine(item.CustomerName);
}