namespace Schrauben
{
    class Materialarray
    {
        //Eigenschaften des Arrays werden definiert
        public string Materialbezeichnung { get; set; }
        public double Materialpreis { get; set; }
        public double Materialdichte { get; set; }

        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Materialbezeichnung + "|" + Materialpreis + "|" + Materialdichte;
        }
    }
}
