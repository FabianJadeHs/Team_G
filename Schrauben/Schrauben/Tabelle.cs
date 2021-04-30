using System.Collections.Generic;
using System.IO;
using System.Globalization;

namespace Schrauben
{
    class Tabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden um Datenhoheit zu haben
        private List<Schraubenarray> liste;

        public Tabelle()
        {
            //neue leere Liste
            liste = new List<Schraubenarray>();

            //Daten werden aus csv Datein eingelesen; wird zeilenweise als strings eingelesen
            string[] zeilen = File.ReadAllLines(@"..\..\..\Schrauben.csv");

            //für jede Zeile wird der string in Werte getrennt und als Array erzeugt
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                string Gewindebezeichnung = daten[0];
                double Steigung = double.Parse(daten[1], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Schluesselweite = double.Parse(daten[2], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Nenndurchmesser = double.Parse(daten[3], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Schraubenkopfhoehe = double.Parse(daten[4], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Schraubenkopfbreite = double.Parse(daten[5], CultureInfo.GetCultureInfo("de-DE").NumberFormat);

                //liste wird einer Schraube angefügt
                liste.Add(new Schraubenarray { Gewindebezeichnung = Gewindebezeichnung, Steigung = Steigung, Schluesselweite = Schluesselweite, Nenndurchmesser = Nenndurchmesser, Schraubenkopfhoehe = Schraubenkopfhoehe, Schraubenkopfbreite = Schraubenkopfbreite });
            }
        }
        //Ausgabe der Daten als Array weil Array kann nicht verändert werden
        public Schraubenarray[] getAll()
        {
            return liste.ToArray();
        }
    }
}
