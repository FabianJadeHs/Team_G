using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Schrauben
{
    class Materialtabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden, um Datenhoheit zu haben
        private List<Materialarray> liste;

        public Materialtabelle()
        {
            //neue leere Liste
            liste = new List<Materialarray>();

            //Daten werden aus csv Datein eingelesen; wird zeilenweise als strings eingelesen
            string[] zeilen = File.ReadAllLines(@"..\..\..\Materialien.csv");

            //für jede Zeile wird der string in Werte getrennt und als Array erzeugt
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                string Materialbezeichnung = daten[0];
                double Materialpreis = double.Parse(daten[1], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Materialdichte = double.Parse(daten[2], CultureInfo.GetCultureInfo("de-DE").NumberFormat);

                //Liste wird ein Material angefügt
                liste.Add(new Materialarray { Materialbezeichnung = Materialbezeichnung, Materialpreis = Materialpreis, Materialdichte = Materialdichte });

            }
        }
        //Ausgabe der Daten als Array, weil Array nicht verändert werden kann
        public Materialarray[] getAll()
        {
            return liste.ToArray();
        }
    }
}
