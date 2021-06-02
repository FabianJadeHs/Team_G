namespace Schrauben
{
    class Schraubenarray
    {
        // Eigenschaften des Arrays werden definiert, enthält alle wichtigen Werte für die Berechnungen
        public string Gewindebezeichnung { get; set; }
        public double Steigung { get; set; }
        public double Schluesselweite { get; set; }
        public double Nenndurchmesser { get; set; }
        public double Schraubenkopfhoehe { get; set; }
        public double Schraubenkopfbreite { get; set; }
        public double InnensechskantZ { get; set; }
        public double SechskanttiefeZ { get; set; }
        public double KopfhoeheZ { get; set; }
        public double KopfdurchmesserZ { get; set; }
        public double InnensechskantS { get; set; }
        public double SechskanttiefeS { get; set; }
        public double KopfhoeheS { get; set; }
        public double KopfdurchmesserS { get; set; }
        public double InnensechkantGS { get; set; }
        public double SechskanttiefeGS { get; set; }
        public double Absatzdurchmesser { get; set; }

        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Gewindebezeichnung + "|" + Steigung + "|" + Schluesselweite + "|" + Nenndurchmesser + "|" + Schraubenkopfhoehe + "|" + Schraubenkopfbreite + "|" + InnensechskantZ + "|" + SechskanttiefeZ + "|" + KopfhoeheZ + 
                "|" + KopfdurchmesserZ + "|" + InnensechskantS + "|" + SechskanttiefeS + "|" + KopfhoeheS + "|" + KopfdurchmesserS + "|" + InnensechkantGS + "|" + SechskanttiefeGS + "|" + Absatzdurchmesser;
        }

    }
}
