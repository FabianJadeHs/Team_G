using System;

namespace Schrauben
{
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

        //globale Variablen innerhalb der class werden definiert damit Unterprogramme kürzer sind
        double rundung = 0;
        double volumen = 0;
        double gewicht = 0;
        double preis = 0;
        double spannungsquerschnitt = 0;

        public void Rundung() //Unterprogramm Rundungsberechnung
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

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
            Console.WriteLine("Die Rundung beträgt " + rundung + " mm.");
        }
        
        public void Volumen() //Unterprogramm Volumenberechnung
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

            //lokale Variablen werden deklariert
            double gesamtlaenge = 0;
            double schaftvolumen = 0;
            double kopfvolumen = 0;
            
            //Schraubenarray wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //die Gesamtlänge wird ausgerechnet
                    gesamtlaenge = Wunschgewindelaenge + Wunschschaftlaenge;
                    //das Volumen des Schaftes wird berechnet (Gewindelänge + Schaftlänge)
                    schaftvolumen = Math.PI * Math.Pow((m.Nenndurchmesser / 2), 2) * gesamtlaenge;
                    //das Volumen des Schraubenkopfes wird ausgerechnet
                    kopfvolumen = 2.598 * Math.Pow((m.Schraubenkopfbreite / 2), 2) * m.Schraubenkopfhoehe;
                    //das Gesamtvolumen:
                    volumen = schaftvolumen + kopfvolumen;
                }
            }
            //Ausgabe Volumen
            Console.WriteLine("Das Volumen einer Schraube beträgt " + volumen + " mm³.");
        }

        public void Gewicht() //Unterprogramm Gewichtsberechnung
        {
            // neue Materialtabelle wird erzeugt
            Materialtabelle tab2 = new Materialtabelle();

            //Materialarray wird zeilenweise durchgegangen bis Eingabewert gefunden ist
            foreach (Materialarray n in tab2.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if (Wunschmaterial == n.Materialbezeichnung)
                {
                    //Gewicht wird berechnet
                    gewicht = volumen * (n.Materialdichte/1000) ;
                }

            }
            //Ausgabe Gewicht
            Console.WriteLine("Das Gewicht einer Schraube beträgt " + gewicht + " in g.");
        }
        public void Preis() //Unterprogramm Preisberechnung
        {
            //Neue Materialtabelle wird erzeugt
            Materialtabelle tab2 = new Materialtabelle();

            //Materialarray wird zeilenweise durchgegangen
            foreach (Materialarray n in tab2.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde geprüft
                if (Wunschmaterial == n.Materialbezeichnung)
                {
                    //Preis wird berechnet
                    preis = (gewicht / 1000) * n.Materialpreis * Wunschanzahl;
                }

            }
            //Ausgabe Preis
            Console.WriteLine("Der Preis aller Schrauben beziffert sich auf " + preis + " Euro insgesamt.");
        }
        public void Spannungsquerschnitt() //Unterprogramm Spannungsquerschnittsberechnung
        {
            //neue Tabelle wird erzeugt
            Tabelle tab = new Tabelle();

            // lokale Variablen werden definiert
            double d2 = 0;
            double d3 = 0;

            //Array wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //mittlerer Durchmesser wird berechnet, siehe Tabellenbuch S.214
                    d2 = m.Nenndurchmesser - 0.6495 * m.Steigung;
                    //Kerndurchmesser wird berechnet, siehe auch Tabellenbuch
                    d3 = m.Nenndurchmesser - 1.2269 * m.Steigung;
                    //Spannungsquerschnitt wird berechnet
                    spannungsquerschnitt = (Math.PI / 4) * Math.Pow(((d2 + d3) / 2), 2);
                }
            }
            //Spannungsquerschnitt wird ausgegeben
            Console.WriteLine("Der Spannungsquerschnitt einer Schraube beträgt " + spannungsquerschnitt + " mm².");
        }
        public void Standardausgaben()
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

            //Schraubenarray wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf gleichheit mit dem Wunschgewinde geprüft
                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    //Ausgabe Gewindesteigung
                    Console.WriteLine("Die Gewindesteigung der Schraube beträgt " + m.Steigung + " mm.");
                    //Ausgabe Schraubenkopfbreite
                    Console.WriteLine("Die Schraubenkopfbreite beträgt " + m.Schraubenkopfbreite + " mm.");
                    //Ausgabe Schluesselweite
                    Console.WriteLine("Die Schlüsselweite ist " + m.Schluesselweite);
                }
            }
        }

    }
}
