using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Schrauben
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void Rundung()
        {
            //Rundung soll berechnet werden; immer mit static void Main. Die Rundungsberechnung ist nur ein Beispiel für euch wie mit dem Array gearbeitet werden muss
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

            double rundung = 0;

            //Frage nach gewünschtem Gewinde
            Console.WriteLine("Von welchem Gewinde soll die Rundung berechnet werden? (ohne Leerzeichen eingeben und Großbuchstaben verwenden)");

            //Eingegebener Wert wird als string gespeichert
            string wunschgewinde = Console.ReadLine();

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (wunschgewinde == m.Gewindebezeichnung)
                {
                    //Die Rundung wird berechnet
                    rundung = 0.1443 * m.Steigung;
                }
            }
            //Ausgabe des Rundungswertes
            Console.WriteLine(rundung);
            Console.ReadKey();

        } //Unterprogramm Rundung Eingabe/Ausgabe

        
    }
    class Schraubenarray
    {
        //Eigenschaften des Arrays werden definiert
        public string Gewindebezeichnung { get; set; }
        public double Steigung { get; set; }
        public double Schluesselweite { get; set; }
        public double Nenndurchmesser { get; set; }
        public double Schraubenkopfhoehe { get; set; }
        public double Schraubenkopfbreite { get; set; }

        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Gewindebezeichnung + "|" + Steigung + "|" + Schluesselweite + "|" + Nenndurchmesser + "|" + Schraubenkopfhoehe + "|" + Schraubenkopfbreite;
        }

    }

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
                double Steigung = double.Parse(daten[1]);
                double Schluesselweite = double.Parse(daten[2]);
                double Nenndurchmesser = double.Parse(daten[3]);
                double Schraubenkopfhoehe = double.Parse(daten[4]);
                double Schraubenkopfbreite = double.Parse(daten[5]);

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

    class Materialtabellen
    {
        struct Material
        {
            //deklariere Struktur mit Variablen
            public string Materialbezeichnung;
            public double Preis;
            public double Dichte;
        }

        public void Materialarray()
        {
            //deklariere Feld
            Material[] Materialien = new Material[5];

            //speichere Werte; Preis in Euro pro Kilogramm; Dichte in Gramm pro Kubikzentimeter 
            Materialien[0].Materialbezeichnung = "Baustahl";
            Materialien[0].Preis = 0.5;
            Materialien[0].Dichte = 7.85;

            Materialien[1].Materialbezeichnung = "V4A";
            Materialien[1].Preis = 1.5;
            Materialien[1].Dichte = 8;

            Materialien[2].Materialbezeichnung = "Messing";
            Materialien[2].Preis = 3.35;
            Materialien[2].Dichte = 8.73;

            Materialien[3].Materialbezeichnung = "Aluminium";
            Materialien[3].Preis = 1.95;
            Materialien[3].Dichte = 2.7;

            Materialien[4].Materialbezeichnung = "Kupfer";
            Materialien[4].Preis = 7.76;
            Materialien[4].Dichte = 8.96;

        }
    }

    class Schraube
    {
        private double Gewindelaenge;
        private double Schaftlaenge;
        private double Material;
       

        public Schraube()
        {
            Gewindelaenge = 10;
            Schaftlaenge = 0;
            Material=7.85;

        }

        public Schraube(double Gewindelaenge, double Schaftlaenge)
        {
            this.Gewindelaenge = Gewindelaenge;
            this.Schaftlaenge = Schaftlaenge;
            Material = 7.85;
        }

        public void setGewindelaenge(double local_Gewindelaenge)
        {
            Gewindelaenge = local_Gewindelaenge;
        }

        public void setSchaftlaenge(double local_Schaftlaenge)
        {
            Schaftlaenge = local_Schaftlaenge;
        }

        public void setMaterial(double local_Material)
        {
            Material = local_Material;
        }
                
        public double getSchraubenlaenge()
        {
            double res;
            res = (Schaftlaenge + Gewindelaenge);
            return res;
        }

        public double getVolumen()
        {
            double Volumen;
            Volumen = (Math.PI * Math.Pow(r, 2));
            return Volumen;
        }
    }
}
