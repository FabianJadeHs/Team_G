﻿using System;
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
            //Rundung soll berechnet werden; immer mit static void Main. Die Rundungsberechnung ist nur ein Beispiel für euch wie mit dem Array gearbeitet werden muss
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

            double rundung = 0;

            //Frage nach gewünschtem Gewinde
            Console.WriteLine("Von welchem Gewinde soll die Rundung berechnet werden? (ohne Leerzeichen eingeben und Großbuchstaben verwenden)");

            //Eingegebener Wert wird als string gespeichert
            string wunschgewinde = Console.ReadLine();

            //Array wird zeilenweise durchgegangen
            foreach (Schrauben m in tab.getAll())
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
        }
    }
    class Schrauben
    {
        //Eigenschaften des Arrays werden definiert
        public string Gewindebezeichnung { get; set; }
        public double Steigung { get; set; }
        public double Schluesselweite { get; set; }
        public double Nenndurchmesser { get; set; }

        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Gewindebezeichnung + "|" + Steigung + "|" + Schluesselweite + "|" + Nenndurchmesser;
        }

    }

    class Tabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden um Datenhoheit zu haben
        private List<Schrauben> liste;

        public Tabelle()
        {
            //neue leere Liste
            liste = new List<Schrauben>();

            //Daten werden aus csv Datein eingelesen; wird zeilenweise als strings eingelesen Die Verknüpfung funktioniert noch nicht
            string[] zeilen = File.ReadAllLines(@"https:\\github.com\FabianJadeHs\Team_G.git\Schrauben\Schrauben.csv");

            //für jede Zeile wird der string in Werte getrennt und als Array erzeugt
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                string Gewindebezeichnung = daten[0];
                double Steigung = double.Parse(daten[1]);
                double Schluesselweite = double.Parse(daten[2]);
                double Nenndurchmesser = double.Parse(daten[3]);

                //liste wird einer Schraube angefügt
                liste.Add(new Schrauben { Gewindebezeichnung = Gewindebezeichnung, Steigung = Steigung, Schluesselweite = Schluesselweite, Nenndurchmesser = Nenndurchmesser });
            }
        }
        //Ausgabe der Daten als Array weil Array kann nicht verändert werden
        public Schrauben[] getAll()
        {
            return liste.ToArray();
        }
    }
}
