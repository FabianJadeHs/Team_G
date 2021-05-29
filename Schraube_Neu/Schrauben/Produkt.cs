namespace Schrauben
{
    // Unterdatei zur Auflistung der Variablen die Catia übergeben werden
    class Produkt
    {
        public double laenge { get; private set; }      //Länge überprüfen
        public double Wunschgewindelaenge { get; private set; }
        public double Wunschschaftlaenge { get; private set; }
        public string Wunschgewindeart { get; private set; }
        public string Wunschgewinde { get; private set; }
        public string Wunschschraubenkopf { get; private set; }
        public double Schluesselweite { get; private set; }
        public double KopfhoeheZ { get; private set; }
        public double KopfdurchmesserZ { get; private set; }
        public double KopfhoeheS { get; private set; }
        public double KopfdurchmesserS { get; private set; }
        //public double k { get; private set; }
        public double Ri { get; private set; }
        public double P { get; private set; }
        public double InnensechskantZ { get; private set; }
        public double SechskanttiefeZ { get; private set; }
        public double InnensechskantS { get; private set; }
        public double SechskanttiefeS { get; private set; }
        public double InnensechskantGS { get; private set; }
        public double SechskanttiefeGS { get; private set; }


        public Produkt(double laenge, double Wunschgewindelaenge, double Wunschschaftlaenge, string Wunschgewindeart,
            string Wunschgewinde, string Wunschschraubenkopf, double k, double Nenndurchmesser, double Steigung, double Schluesselweite,
            double KopfhoeheZ, double KopfdurchmesserZ, double KopfhoeheS, double KopfdurchmesserS, double InnensechskantZ, double SechskanttiefeZ,
            double InnensechskantS, double SechskanttiefeS, double InnensechskantGS, double SechskanttiefeGS)
        {
            this.laenge = Wunschgewindelaenge + Wunschschaftlaenge;
            this.Wunschgewindelaenge = Wunschgewindelaenge;
            this.Wunschgewindeart = Wunschgewindeart;
            this.Wunschgewinde = Wunschgewinde;
            this.Wunschschraubenkopf = Wunschschraubenkopf;
            //this.k = k;         //Ich glaube ohne Funktion
            this.Ri = (Nenndurchmesser / 2);     //Radius zur Kreiserzeugung beim Schaft
            this.P = Steigung;
            this.Schluesselweite = Schluesselweite;
            this.KopfhoeheZ = KopfhoeheZ;
            this.KopfdurchmesserZ = KopfdurchmesserZ;
            this.KopfhoeheS = KopfhoeheS;
            this.KopfdurchmesserS = KopfdurchmesserS;
            this.InnensechskantZ = InnensechskantZ;
            this.SechskanttiefeZ = SechskanttiefeZ;
            this.InnensechskantS = InnensechskantS;
            this.SechskanttiefeS = SechskanttiefeS;
            this.InnensechskantGS = InnensechskantGS;
            this.SechskanttiefeGS = SechskanttiefeGS;

        }
    }

}

