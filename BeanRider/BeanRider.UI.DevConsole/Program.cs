using Autofac;
using BeanRider.Data.Db;
using BeanRider.Logic;
using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using System.Reflection;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("*** BeanRider v0.1 PREMIUM EDITION ***");

string conString = "Server=(localdb)\\mssqllocaldb;Database=BeanRider_Tests;Trusted_Connection=True;";


//DI per Reflection
//var path = @"C:\Users\Fred\source\repos\ppedvAG\243222_Architektur\BeanRider\BeanRider.Data.Db\bin\Debug\net8.0\BeanRider.Data.Db.dll";
//var ass = Assembly.LoadFrom(path);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//IRepository repo = Activator.CreateInstance(typeMitRepo, conString) as IRepository;

//manual DI
//using IRepository repo = new EfContextRepositoryAdapter(conString);

//DI per AutoFac
var builder = new ContainerBuilder();
builder.Register(x => new EfContextRepositoryAdapter(conString)).As<IRepository>().SingleInstance();
builder.RegisterType<OrderService>().AsImplementedInterfaces();
builder.RegisterType<CustomerService>().AsImplementedInterfaces();
//builder.Register(x => new ConRepo()).As<IRepository>().SingleInstance();
var container = builder.Build();

IRepository repo = container.Resolve<IRepository>();

IOrderService orderService = container.Resolve<IOrderService>();
ICustomerService customerService = container.Resolve<ICustomerService>();

//var bestCustomer = customerService.GetCustomerWithMostUmsatz();
var bestCustomer = repo.CustomerWithMostUmsatz();
Console.WriteLine($"{bestCustomer.Name}");

foreach (var item in repo.Query<Drink>().Where(x => !x.Alc).OrderByDescending(x => x.KCal).ToList())
{
    Console.WriteLine($"{item.Name} - {item.Price}");
    Console.WriteLine($"Orderd:  {item.OrderItems.Sum(x => x.Amount)}");
}

foreach (var item in orderService.GetOpenOrdersThatAreNotVegetarian())
{
    Console.WriteLine($"{item.Time} - {item.Status}");
    foreach (var orderItem in item.Items)
    {
        Console.WriteLine($"\t{orderItem.Food.Name} - {orderItem.Amount} {orderItem.OrderPrice:c} ");
    }
}

