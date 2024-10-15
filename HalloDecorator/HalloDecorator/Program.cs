// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var p1 =  new Käse(new Käse(new Käse( new Pizza())));
var b1 =  new Käse(new Salami(new Käse( new Brot())));

Console.WriteLine($"{p1.Name} {p1.Preis}");
Console.WriteLine($"{b1.Name} {b1.Preis}");

Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(b1));
