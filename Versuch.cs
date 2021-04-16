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

}
    }
}
