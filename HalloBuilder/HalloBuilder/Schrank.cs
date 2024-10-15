namespace HalloBuilder
{
    public class Schrank
    {
        public int AnzTüren { get; private set; }
        public int AnzBöden { get; private set; }
        public string Farbe { get; private set; } = string.Empty;
        public Oberfläche Oberfläche { get; private set; }

        private Schrank()
        { }

        public class Builder
        {
            Schrank _schrank = new Schrank();

            public Builder SetAnzTüren(int türen)
            {
                if (türen < 2 && türen > 7)
                    throw new System.ArgumentException("Anzahl der Türen muss zwischen 2 und 7 liegen");

                _schrank.AnzTüren = türen;
                return this;
            }

            public Builder SetAnzBöden(int böden)
            {
                if (böden < 1 || böden > 6)
                    throw new System.ArgumentException("Anzahl der Böden muss zwischen 1 und 6 liegen");

                _schrank.AnzBöden = böden;
                return this;
            }

            public Builder SetFarbe(string farbe)
            {
                if (_schrank.Oberfläche != Oberfläche.Lackiert)
                    throw new System.ArgumentException("Farbe kann nur Lackiert");

                _schrank.Farbe = farbe;
                return this;
            }

            public Builder SetOberfläche(Oberfläche oberfläche)
            {
                _schrank.Oberfläche = oberfläche;
                return this;
            }

            public Schrank Build()
            {
                return _schrank;
            }
        }

    }

    public enum Oberfläche
    {
        Natur,
        Gewachst,
        Lackiert
    }
}
