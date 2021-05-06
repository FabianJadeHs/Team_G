using System;
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

            //Console.WriteLine("Welcher Schraubenkopftyp ist gewünscht? (Sechskant/Zylinderkopf/Senkkopf/Gewindestift)");
            //test1.Wunschschraubenkopf = Console.ReadLine();

            Console.WriteLine("Wie lang soll das Gewinde sein?");
            test1.Wunschgewindelaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Wie lang soll der Schaft sein?");
            test1.Wunschschaftlaenge = double.Parse(Console.ReadLine());

            Console.WriteLine("Aus welchem Material soll die Schraube sein? (Baustahl/V4A/Messing/Aluminium/Kupfer)");
            test1.Wunschmaterial = Console.ReadLine();

            Console.WriteLine("Wie viele Schrauben möchten Sie kaufen?");
            test1.Wunschanzahl = double.Parse(Console.ReadLine());

            //Unterprogramme werden abgerufen und geben Werte aus
            test1.Rundung();
            //test1.Kopfvolumen();
            test1.Volumen();
            test1.Gewicht();
            test1.Preis();
            test1.Spannungsquerschnitt();
            test1.Standardausgaben();
            test1.Flaechentraegheitsmoment();
            test1.Schwerpunkt();
            Console.ReadKey();

            
            
        }
        
    }
   
    class Festigkeitsarray
    {
        //Eigenschaften des Arrays werden definiert
        public string Festigkeitsklassenbezeichnung { get; set; }
        public double Zugfestigkeit { get; set; }
        public double Streckgrenze { get; set; }
        public double Bruchdehnung { get; set; }


        // bei Ausgabe werden die Spalten getrennt
        public override string ToString()
        {
            return Festigkeitsklassenbezeichnung + "|" + Zugfestigkeit + "|" + Streckgrenze + "|" + Bruchdehnung;
        }
    }
    /*
    class Festigkeitstabelle
    {
        //Liste kann nicht direkt eingesehen oder geändert werden um Datenhoheit zu haben
        private List<Festigkeitsarray> liste;

        public Festigkeitstabelle()
        {
            //neue leere Liste
            liste = new List<Festigkeitsarray>();

            //Daten werden aus csv Datein eingelesen; wird zeilenweise als strings eingelesen
            string[] zeilen = File.ReadAllLines(@"..\..\..\Festigkeitsklassen.csv");

            //für jede Zeile wird der string in Werte getrennt und als Array erzeugt
            foreach (string zeile in zeilen)
            {
                string[] daten = zeile.Split(';');
                double Festigkeitsklassenbezeichnung = double.Parse(daten[0], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Zugfestigkeit = double.Parse(daten[1], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Streckgrenze = double.Parse(daten[2], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
                double Bruchdehnung = double.Parse(daten[3], CultureInfo.GetCultureInfo("de-DE").NumberFormat);

                //Liste wird ein Material angefügt
                liste.Add(new Festigkeitsarray { Festigkeitsklassenbezeichnung = Festigkeitsklassenbezeichnung, Zugfestigkeit = Zugfestigkeit, Streckgrenze = Streckgrenze, Bruchdehnung = Bruchdehnung });

            }
        }
        //Ausgabe der Daten als Array weil Array kann nicht verändert werden
        public Festigkeitsarray[] getAll()
        {
            return liste.ToArray();
        }
    }
   */
}

