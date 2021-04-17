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

}
    

