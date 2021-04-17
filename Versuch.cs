using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Versuche
{
    class Program
    {
        static void Main(string[] args)
        {
    
              {
                double Schaftlaenge, Gewindelaenge, Schraubenlaengee;

                Console.WriteLine("Wie lang soll der Schaft sein?");
                Schaftlaenge = double.Parse(Console.ReadLine());

                Console.WriteLine("Wie lang soll das Gewinde sein?");
                Gewindelaenge = double.Parse(Console.ReadLine());

                Schraubenlaengee = Schraubenlaenge(Schaftlaenge, Gewindelaenge);

                Console.WriteLine(Schraubenlaengee);
                Console.ReadKey();

            }
        }

        static double Schraubenlaenge(double Schaftlaenge, double Gewindelaenge) //Unterprogramm Schraubenlaenge, Eingabe Schaftlaenge/Gewindelaenge, Ausgabe Schraubenlaenge
        {
            double Schraubenlaengee;

            Schraubenlaengee = Schaftlaenge + Gewindelaenge;

            return Schraubenlaengee;
        }
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


    public void Materialarray()
    {
        struct Material
    {
        //deklariere Struktur mit Variablen
        public string Materialbezeichnung;
        public double Preis;
        public double Dichte;
    } 
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

public Schraube()
{
    Gewindelaenge = 10;
    Schaftlaenge = 0;
    Material = 7.85;

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

