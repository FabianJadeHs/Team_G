namespace Schrauben
{
    class Schraubenarray
    {
        //Eigenschaften des Arrays werden definiert
        public string Gewindebezeichnung { get; set; }
        public double Steigung { get; set; }
        public double Schluesselweite { get; set; }
        public double Nenndurchmesser { get; set; }
        public double Schraubenkopfhoehe { get; set; }
        public double Schraubenkopfbreite { get; set; }
        public double InnensechskantZ { get; set; }
        public double KopfhoeheZ { get; set; }
        public double KopfdurchmesserZ { get; set; }
        public double InnensechskantS { get; set; }

        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Gewindebezeichnung + "|" + Steigung + "|" + Schluesselweite + "|" + Nenndurchmesser + "|" + Schraubenkopfhoehe + "|" + Schraubenkopfbreite + "|" + InnensechskantZ + "|" + KopfhoeheZ + "|" + KopfdurchmesserZ + "|" + InnensechskantS;
        }

    }
}
