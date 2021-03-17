using System;

namespace Fakultät_von_n_berechnen
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fenster Titel
            Console.Title = "Fakultät von n berechnen";

            // Eingabe von n
            Console.Write("Fakultät von n: ");
            int n = Convert.ToInt32(Console.ReadLine());

            // Fakultät berechnen
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            // Ergebnis ausgeben
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}