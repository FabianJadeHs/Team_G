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
            Schraube test1 = new Schraube();

            //Kundeneingaben wie er die Schraube haben möchte
            Console.WriteLine("Welches Gewinde ist gewünsch?(also M8 etc.)(ohne Leerzeichen eingeben und Großbuchstaben verwenden)");
            test1.Wunschgewindeart = Console.ReadLine();

            Console.WriteLine("Wie lang soll das Gewinde sein?");
            test1.Wunschgewindelaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Wie lang soll der Schafft sein?");
            test1.Wunschschaftlaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Aus welchem Material soll die Schraube sein?");
            test1.Wunschmaterial = Console.ReadLine();
            
            test1.Rundung();
            test1.Volumen();
            Console.ReadKey();
            
        }
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
        //Eigenschaften der Schraube
        public double Gewindelaenge { get; set; }
        public double Schaftlaenge { get; set; }
        public double Material { get; set; }
        public double Gewindebezeichnung { get; set; }
        public string Wunschgewindeart { get; set; }
        public double Wunschgewindelaenge { get; set; }
        public double Wunschschaftlaenge { get; set; }
        public string Wunschmaterial { get; set; }

        public void Rundung() //Unterprogramm Rundung
        {
            //Rundung soll berechnet werden; immer mit static void Main. Die Rundungsberechnung ist nur ein Beispiel für euch wie mit dem Array gearbeitet werden muss
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            double rundung = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if ( Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //Die Rundung wird berechnet
                    rundung = 0.1443 * m.Steigung;
                }
            }
            //Ausgabe des Rundungswertes
            Console.WriteLine(rundung);
        }  
        public void Volumen() //Unterprogramm Volumen
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            //lokale Variablen werden deklariert
            double gesamtlaenge = 0;
            double schaftvolumen = 0;
            double kopfvolumen = 0;
            double volumen = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if ( Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //die Gesamtlänge wird ausgerechnet
                    gesamtlaenge = Wunschgewindelaenge + Wunschschaftlaenge;
                    //das Volumen des Schaftes wird berechnet
                    schaftvolumen = Math.PI * Math.Pow((m.Nenndurchmesser / 2), 2) * gesamtlaenge;
                    //das Volumen des Schraubenkopfes wird ausgerechnet
                    kopfvolumen = Math.PI * Math.Pow((m.Schraubenkopfbreite / 2), 2) * m.Schraubenkopfhoehe;
                    //das Gesamtvolumen:
                    volumen = schaftvolumen + kopfvolumen;
                }
            }
            //Ausgabe Volumen
            Console.WriteLine(volumen);
        } 

        public void Gewicht()
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            //lokale Variablen werden deklariert
            double gesamtlaenge = 0;
            double schaftvolumen = 0;
            double kopfvolumen = 0;
            double volumen = 0;
            double gewicht = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //die Gesamtlänge wird ausgerechnet
                    gesamtlaenge = Wunschgewindelaenge + Wunschschaftlaenge;
                    //das Volumen des Schaftes wird berechnet
                    schaftvolumen = Math.PI * Math.Pow((m.Nenndurchmesser / 2), 2) * gesamtlaenge;
                    //das Volumen des Schraubenkopfes wird ausgerechnet
                    kopfvolumen = Math.PI * Math.Pow((m.Schraubenkopfbreite / 2), 2) * m.Schraubenkopfhoehe;
                    //das Gesamtvolumen:
                    volumen = schaftvolumen + kopfvolumen;
                }
            }
            foreach ( Materialtabellen n in tab.getAll())
            {

            }

        }

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

        
    }
}
