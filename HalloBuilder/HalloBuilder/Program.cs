using HalloBuilder;

Console.WriteLine("Hello, World!");


var schrank = new Schrank.Builder().SetFarbe("Rot").SetAnzTüren(333).SetAnzBöden(5554).SetOberfläche(Oberfläche.Gewachst).Build();

Console.WriteLine("ende");