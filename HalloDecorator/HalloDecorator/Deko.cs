
interface ICompoment
{
    string Name { get; }
    decimal Preis { get; }
}

class Pizza : ICompoment
{
    public string Name => "Pizza";

    public decimal Preis => 5m;
}

class Brot : ICompoment
{
    public string Name => "Brot";

    public decimal Preis => 2m;
}

abstract class Deko : ICompoment
{
    protected ICompoment _compoment;
    public Deko(ICompoment compoment)
    {
        _compoment = compoment;
    }
    public abstract string Name { get; }
    public abstract decimal Preis { get; }
}

class Salami : Deko
{
    public Salami(ICompoment compoment) : base(compoment)
    {
    }
    public override string Name => _compoment.Name + " mit Salami";
    public override decimal Preis => _compoment.Preis + 3m;
}

class Käse : Deko
{
    public Käse(ICompoment compoment) : base(compoment)
    {
    }
    public override string Name => _compoment.Name + " mit Käse";
    public override decimal Preis => _compoment.Preis + 2m;
}