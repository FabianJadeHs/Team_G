using System;

namespace Schrauben
{
    public class Schraube
    {
        // Eigenschaften der Schraube
        public double Gewindelaenge { get; set; }
        public double Schaftlaenge { get; set; }
        public double Material { get; set; }
        public double Gewindebezeichnung { get; set; }
        public string Wunschgewindeart { get; set; }
        public double Wunschgewindelaenge { get; set; }
        public double Wunschschaftlaenge { get; set; }
        public string Wunschmaterial { get; set; }
        public double Wunschanzahl { get; set; }
        public string Wunschschraubenkopf { get; set; }
        public string Wunschfestigkeit { get; set; }
        public string Gewinderichtung { get; set; }
        public string Gewindeart { get; set; }
           

        // globale Variablen innerhalb der class werden definiert damit Unterprogramme kürzer sind
        double rundung = 0;
        double volumen = 0;
        double kopfvolumen = 0;
        double gewicht = 0;
        double preis = 0;
        double spannungsquerschnitt = 0;
        double schwerpunkt = 0;
        double gesamtlaenge = 0;
        double schaftvolumen = 0;
        double d2 = 0;    // Flankendurchmesser
        double d3 = 0;    // Kerndurchmesser des Außengewindes
        double ftm = 0;
        double vorspannkraft = 0;
        double schluesselweite = 0;
        double steigung = 0;
        double nenndurchmesser = 0;
        double innensechskantz = 0;
        double sechskanttiefeZ = 0;
        double innensechskantS = 0;
        double sechskanttiegeS = 0;
        double innensechskantGS = 0;
        double sechskanttiefeGS = 0;
        double kopfhoeheZ = 0;
        double kopfdurchmesserS = 0;
        double kopfhoeheS = 0;
        double kopfdurchmesserZ = 0;
        double kopfhoeheSE = 0;
        double absatzdurchmesser = 0;

        #region Berechnungen
        public double Rundung() //Unterprogramm Rundungsberechnung
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
            return rundung;
        }

        public void Kopfvolumen()  // Unterprogramm Kopfvolumen, zum Abdecken verschiedener Kopfarten
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

            foreach (Schraubenarray m in tab.getAll())
            {
                //in Zeilen werden die Gewindebezeichnungen auf Gleichheit mit dem Wunschgewinde UND die Auswahl des Schraubenkopfes geprüft
                //das Volumen der verschiedenen Schraubenköpfe wird ausgerechnet
                if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Sechskant")    // Volumenberechnung Sechskantkopf, wenn Auswahl in ComboBox entsprechend ist
                {
                    kopfvolumen = 2.598 * Math.Pow((m.Schraubenkopfbreite / 2), 2) * m.Schraubenkopfhoehe;
                }

                else if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Zylinderkopf mit Innensechskant")  // Volumenberechnung Zylinderkopf
                {
                    kopfvolumen = m.KopfhoeheZ * Math.PI * Math.Pow((m.KopfdurchmesserZ / 2), 2) - (2.598 * Math.Pow((m.InnensechskantZ / 2), 2) * m.SechskanttiefeZ);
                }

                else if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Zylinderkopf mit Schlitz")  // Volumenberechnung Zylinderkopf
                {
                    kopfvolumen = m.KopfhoeheZ * Math.PI * Math.Pow((m.KopfdurchmesserZ / 2), 2);
                }

                else if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Senkkopf mit Innensechskant")   // Volumenberechnung Senkkopf
                {
                    kopfvolumen = (((Math.PI * (m.KopfhoeheS)) / 12) * (Math.Pow((m.KopfdurchmesserS), 2) + Math.Pow((m.Nenndurchmesser), 2) + ((m.KopfdurchmesserS) * (m.Nenndurchmesser)))) - (2.598 * Math.Pow((m.InnensechskantS / 2), 2) * m.SechskanttiefeS);
                }

                else if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Senkkopf mit Schlitz")   // Volumenberechnung Senkkopf
                {
                    kopfvolumen = (((Math.PI * (m.KopfhoeheS)) / 12) * (Math.Pow((m.KopfdurchmesserS), 2) + Math.Pow((m.Nenndurchmesser), 2) + ((m.KopfdurchmesserS) * (m.Nenndurchmesser))));
                }

                else if (Wunschgewindeart == m.Gewindebezeichnung && Wunschschraubenkopf == "Gewindestift")  // Volumenberechnung "Kopf" des Gewindestiftes, Volumen wird negativ, da "Kopf" im Gewinde
                {
                    kopfvolumen = -2.598 * Math.Pow((m.InnensechkantGS / 2), 2) * m.SechskanttiefeGS;
                }
            }
        }


        public double Volumen() //Unterprogramm Volumenberechnung
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();


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
                    //das Gesamtvolumen:
                    volumen = schaftvolumen + kopfvolumen;
                }
            }
            //Ausgabe Volumen
            Console.WriteLine("Das Volumen einer Schraube beträgt " + volumen + " mm³.");
            return volumen;
        }

        public double Gewicht() //Unterprogramm Gewichtsberechnung
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
                    gewicht = volumen * (n.Materialdichte / 1000);
                }

            }
            //Ausgabe Gewicht
            Console.WriteLine("Das Gewicht einer Schraube beträgt " + gewicht + " in g.");
            return gewicht;
        }
        public double Preis() //Unterprogramm Preisberechnung
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
            return preis;
        }

        public double Schwerpunkt()  // Unterprogramm Schwerpunkt
        {
            Tabelle tab = new Tabelle();

            foreach (Schraubenarray m in tab.getAll())  // ermöglicht Abfrage von Daten aus der csv-Datei
            {
                // Schraubenkopfvolumen wird negativ gewertet (Festlegung des KS-Ursprungs am Übergang zwischen Gewinde und Kopf)
                // Summe der Volumen * Abstand geteilt durch die Summe der Volumen
                // Aufgrund unterschiedlicher Geometrien muss hier zwischen den unterschiedlichen Köpfen unterschieden werden

                if (Wunschschraubenkopf == "Sechskant")
                {
                    schwerpunkt = (kopfvolumen * (-m.Schraubenkopfhoehe / 2) + schaftvolumen * (gesamtlaenge / 2)) / (kopfvolumen + schaftvolumen);
                }

                else if (Wunschschraubenkopf == "Zylinderkopf")
                {
                    schwerpunkt = (kopfvolumen * (-m.KopfhoeheZ / 2) + schaftvolumen * (gesamtlaenge / 2)) / (kopfvolumen + schaftvolumen);
                }

                else if (Wunschschraubenkopf == "Senkkopf")
                {
                    schwerpunkt = (kopfvolumen * (-m.KopfhoeheS / 2) + schaftvolumen * (gesamtlaenge / 2)) / (kopfvolumen + schaftvolumen);
                }

                else if (Wunschschraubenkopf == "Gewindestift")
                {
                    schwerpunkt = (kopfvolumen * (-m.SechskanttiefeGS / 2) + schaftvolumen * (gesamtlaenge / 2)) / (kopfvolumen + schaftvolumen);
                }

            }
            Console.WriteLine("Der Schwerpunkt liegt " + schwerpunkt + " mm unterhalb des Schraubenkopfes");
            return schwerpunkt;
        }

        public double Spannungsquerschnitt() //Unterprogramm Spannungsquerschnittsberechnung
        {
            //neue Tabelle wird erzeugt
            Tabelle tab = new Tabelle();


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
            return spannungsquerschnitt;
        }

        public double Flaechentraegheitsmoment()
        {
            // Berechnung des FTM über mit dem Spannungsquerschnitt
            {
                ftm = Math.Pow((spannungsquerschnitt / (4 / Math.PI)), 2) * (Math.PI / 32);
            }

            Console.WriteLine("Das Flächenträgheitsmoment beträgt " + ftm + " mm^4");
            return ftm;
        }

        public double Vorspannkraft()
        {
            //neue Tabelle wird deklariert
            Festigkeitstabelle tab3 = new Festigkeitstabelle();

            foreach (Festigkeitsarray o in tab3.getAll())
            {
                if (Wunschfestigkeit == o.Festigkeitsklassenbezeichnung)
                {
                    //Berechnung der benötigten Vorspannkraft
                    vorspannkraft = spannungsquerschnitt * 0.9 * o.Streckgrenze;
                }

            }
            // Ausgabe der Vorspannkraft
            Console.WriteLine("Vorspannkraft beträgt " + vorspannkraft + " N");
            return vorspannkraft;
        }
               
        public double Schluesselweite()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    schluesselweite = m.Schluesselweite;
                }
            }

            return schluesselweite;

        }

        public double Steigung()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    steigung = m.Steigung;
                }
            }

            return steigung;

        }

        public double Nenndurchmesser()
        {

            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    nenndurchmesser = m.Nenndurchmesser;
                }
            }

            return nenndurchmesser;
        }

        public double InnensechskantZ()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    innensechskantz = m.InnensechskantZ;
                }
            }

            return innensechskantz;
        }

        public double SechskanttiefeZ()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    sechskanttiefeZ = m.SechskanttiefeZ;
                }
            }

            return sechskanttiefeZ;
        }

        public double InnensechskantS()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    innensechskantS = m.InnensechskantS;
                }
            }

            return innensechskantS;
        }

        public double SechskanttiefeS()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    sechskanttiegeS = m.SechskanttiefeS;
                }
            }

            return sechskanttiegeS;
        }

        public double InnensechskantGS()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    innensechskantGS = m.InnensechkantGS;
                }
            }

            return innensechskantGS;
        }

        public double SechskanttiefeGS()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    sechskanttiefeGS = m.SechskanttiefeGS;
                }
            }

            return sechskanttiefeGS;
        }

        public double KopfhoeheZ()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    kopfhoeheZ = m.KopfhoeheZ;
                }
            }
            return kopfhoeheZ;
        }

        public double KopfdurchmesserS()
        {

            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    kopfdurchmesserS = m.KopfdurchmesserS;
                }
            }
            return kopfdurchmesserS;
        }

        public double KopfhoeheS()
        {
            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    kopfhoeheS = m.KopfhoeheS;
                }
            }
            return kopfhoeheS;
        }

        public double KopfdurchmesserZ()
        {

            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    kopfdurchmesserZ = m.KopfdurchmesserZ;
                }
            }
            return kopfdurchmesserZ;
        }

        public double KopfhoeheSE()
        {

            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    kopfhoeheSE = m.Schraubenkopfhoehe;
                }
            }
            return kopfhoeheSE;
        }

        public double Absatzdurchmesser()
        {

            Tabelle tab = new Tabelle();
            foreach (Schraubenarray m in tab.getAll())
            {

                if (Wunschgewindeart == m.Gewindebezeichnung)
                {
                    absatzdurchmesser = m.Absatzdurchmesser;
                }
            }
            return absatzdurchmesser;
        }
        #endregion


    }
}
