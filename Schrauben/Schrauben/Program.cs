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
            Console.WriteLine("Welches Gewinde ist gewünsch?(also M8 etc.)(ohne Leerzeichen eingeben)");
            test1.Wunschgewindeart = Console.ReadLine();
            test1.Wunschgewindeart = test1.Wunschgewindeart.ToUpper();

            Console.WriteLine("Wie lang soll das Gewinde sein?");
            test1.Wunschgewindelaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Wie lang soll der Schaft sein?");
            test1.Wunschschaftlaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Aus welchem Material soll die Schraube sein?");
            test1.Wunschmaterial = Console.ReadLine();

            Console.WriteLine("Wie viele Schrauben möchten Sie kaufen?");
            test1.Wunschanzahl = double.Parse(Console.ReadLine());

            test1.Rundung();
            test1.Volumen();
            test1.Gewicht();
            test1.Preis();
            test1.Spannungsquerschnitt();
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

    class Materialtabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden um Datenhoheit zu haben
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
                double Materialpreis = double.Parse(daten[1]);
                double Materialdichte = double.Parse(daten[2]);

                //Liste wird ein Material angefügt
                liste.Add(new Materialarray { Materialbezeichnung = Materialbezeichnung, Materialpreis = Materialpreis, Materialdichte = Materialdichte });

            }
        }
        //Ausgabe der Daten als Array weil Array kann nicht verändert werden
        public Materialarray[] getAll()
        {
            return liste.ToArray();
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
        public double Wunschanzahl { get; set; }

        public void Rundung() //Unterprogramm Rundungsberechnung
        {
            //Rundung soll berechnet werden; immer mit static void Main. Die Rundungsberechnung ist nur ein Beispiel für euch wie mit dem Array gearbeitet werden muss
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            double rundung = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //Die Rundung wird berechnet
                    rundung = 0.1443 * m.Steigung;
                }
            }
            //Ausgabe des Rundungswertes
            Console.WriteLine(rundung + " in mm");
        }
        public void Volumen() //Unterprogramm Volumenberechnung
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
            //Ausgabe Volumen
            Console.WriteLine(volumen + " in mm³");
        }

        public void Gewicht() //Unterprogramm Gewichtsberechnung
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
            Materialtabelle tab2 = new Materialtabelle();
            foreach (Materialarray n in tab2.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if (Wunschmaterial == n.Materialbezeichnung)
                {
                    gewicht = volumen * (n.Materialdichte/1000) ;
                }

            }
            Console.WriteLine(gewicht + " in g");
        }
        public void Preis()
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            //lokale Variablen werden deklariert
            double gesamtlaenge = 0;
            double schaftvolumen = 0;
            double kopfvolumen = 0;
            double volumen = 0;
            double gewicht = 0;
            double preis = 0;

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
            Materialtabelle tab2 = new Materialtabelle();
            foreach (Materialarray n in tab2.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if (Wunschmaterial == n.Materialbezeichnung)
                {
                    gewicht = volumen * (n.Materialdichte / 1000);
                    preis = (gewicht / 1000) * n.Materialpreis * Wunschanzahl;
                }

            }
            Console.WriteLine(preis + " in Euro pro Stück");
        }
        public void Spannungsquerschnitt()
        {
            Tabelle tab = new Tabelle();

            double d2 = 0;
            double d3 = 0;
            double spannungsquerschnitt = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    d2 = m.Nenndurchmesser - 0.6495 * m.Steigung;
                    d3 = m.Nenndurchmesser - 1.2269 * m.Steigung;
                    spannungsquerschnitt = (Math.PI / 4) * Math.Pow(((d2 + d3) / 2), 2);
                }
            }
            Console.WriteLine(spannungsquerschnitt + " in mm²");
        }

    }
}
