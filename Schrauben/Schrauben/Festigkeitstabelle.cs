using System.Globalization;

namespace Schrauben
{
    class Festigkeitstabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden um Datenhoheit zu haben
        private List<Festigkeitsarray> liste;

        public Festigkeitstabelle()
        {
            //neue leere Liste
            liste = new List<Festigkeitsarray>();

            //Daten werden aus csv Datein eingelesen; wird zeilenweise als strings eingelesen
            string[] zeilen = File.ReadAllLines(@"..\..\..\Festigkeitstabelle.csv");

            //für jede Zeile wird der string in Werte getrennt und als Array erzeugt
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                double Festigkeitsklassenbezeichnung = double.Parse(daten[0], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Zugfestigkeit = double.Parse(daten[1], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Streckgrenze = double.Parse(daten[2], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Bruchdehnung = double.Parse(daten[3], CultureInfo.GetCultureInfo("de-DE").NumberFormat);

                //Liste wird ein Material angefügt
                liste.Add(new Festigkeitsarray { Festigkeitsklassenbezeichnung = Festigkeitsklassenbezeichnung, Zugfestigkeit = Zugfestigkeit, Streckgrenze = Streckgrenze, Bruchdehnung = Bruchdehnung });

            }
        }
        //Ausgabe der Daten als Array weil Array kann nicht verändert werden
        public Festigkeitsarray[] getAll()
        {
            return liste.ToArray();
        }
    }
   
}

