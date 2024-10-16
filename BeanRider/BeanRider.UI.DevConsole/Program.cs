using BeanRider.Data.Db;
using BeanRider.Logic;
using BeanRider.Model.Contracts;
using BeanRider.Model.DomainModel;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("*** BeanRider v0.1 PREMIUM EDITION ***");

string conString = "Server=(localdb)\\mssqllocaldb;Database=BeanRider_Tests;Trusted_Connection=True;";

IRepository repo = new EfContextRepositoryAdapter(conString);
OrderService orderService = new OrderService(repo);

foreach (var item in repo.GetAll<Drink>())
{
    Console.WriteLine($"{item.Name} - {item.Price}");
}

foreach (var item in orderService.GetOpenOrdersThatAreNotVegetarian())
{
    Console.WriteLine($"{item.Time} - {item.Status}");
    foreach (var orderItem in item.Items)
    {
        Console.WriteLine($"\t{orderItem.Food.Name} - {orderItem.Amount} {orderItem.OrderPrice:c} ");
    }
}
