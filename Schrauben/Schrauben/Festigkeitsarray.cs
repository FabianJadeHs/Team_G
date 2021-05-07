namespace Schrauben
{
    class Festigkeitsarray
    {
        //Eigenschaften des Arrays werden definiert
        public string Festigkeitsklassenbezeichnung { get; set; }
        public double Zugfestigkeit { get; set; }
        public double Streckgrenze { get; set; }
        public double Bruchdehnung { get; set; }


        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Festigkeitsklassenbezeichnung + "|" + Zugfestigkeit + "|" + Streckgrenze + "|" + Bruchdehnung;
        }
    }
   
}

