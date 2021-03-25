using System;

namespace Fakultät_von_n_berechnen
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fenster Titel
            Console.Title = "Fakultaet von n berechnen";

            // Eingabe von n
            Console.Write("Fakultaet von n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            // Fakultaet berechnen
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            // Ergebnis ausgeben
            // Moin ich bin der Fehler, wirklich?
            
                        
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}