﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Schrauben
{

    class Program
    {

        [STAThread]
        static void Main()
        {
            new GUI_control();

            Schraube test1 = new Schraube();
            

            //Kundeneingaben wie er die Schraube haben möchte
            Console.WriteLine("Welches Gewinde ist gewünscht?(also M8 etc.)");
            test1.Wunschgewindeart = Console.ReadLine();
            //Groß und Kleinschreibung sind durch den Befehl egal
            test1.Wunschgewindeart = test1.Wunschgewindeart.ToUpper();
            //Leerzeichen ergeben keine Fehler
            test1.Wunschgewindeart = test1.Wunschgewindeart.Replace(" ", String.Empty);

            Console.WriteLine("Welcher Schraubenkopftyp ist gewünscht? (Sechskant/Zylinderkopf/Senkkopf/Gewindestift)");
            test1.Wunschschraubenkopf = Console.ReadLine();

            Console.WriteLine("Wie lang soll das Gewinde sein?");
            test1.Wunschgewindelaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Wie lang soll der Schaft sein?");
            test1.Wunschschaftlaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Aus welchem Material soll die Schraube sein? (Baustahl/V4A/Messing/Aluminium/Kupfer)");
            test1.Wunschmaterial = Console.ReadLine();

            Console.WriteLine("Welche Festigkeitsklasse ist gewünscht?");
            test1.Wunschfestigkeit = Console.ReadLine();

            Console.WriteLine("Wie viele Schrauben möchten Sie kaufen?");
            test1.Wunschanzahl = double.Parse(Console.ReadLine());

            //Unterprogramme werden abgerufen und geben Werte aus
            test1.Rundung();
            test1.Kopfvolumen();
            test1.Volumen();
            test1.Gewicht();
            test1.Preis();
            test1.Schwerpunkt();
            test1.Spannungsquerschnitt();
            test1.Flaechentraegheitsmoment();
            test1.Vorspannkraft();
            test1.Standardausgaben();

            Console.ReadKey();

            

        }
        
    }
  
}

