using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schrauben
{

    class CatiaControl
    {
        CatiaControl()
        {
            try
            {

                CatiaConnection cc = new CatiaConnection();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    Console.WriteLine("0");

                    // Öffne ein neues Part
                    cc.ErzeugePart();
                    Console.WriteLine("1");

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();
                    Console.WriteLine("2");

                    Produkt dieSchraube = new Produkt(60, 50, "Standardgewinde", "M8", 5.3d, 4d, 1.25d);

                    cc.ErzeugeZylinder(dieSchraube);
                    Console.WriteLine("Schaft");

                    
                    cc.ErzeugeGewindeHelix(dieSchraube);
                    Console.WriteLine("Gewinde");

                    if (Wunschschraubenkopf == "Zylinderkopf")       // Daten aus "Produkt" übernehmen, damit das in "CatiaControl drin ist?
                    {
                        cc.Zylinderkopf();
                        Console.WriteLine("Zylinderkopf");

                        cc.Innensechskant();
                        Console.WriteLine("Innensechskant");
                    }

                    else if (Wunschschraubenkopf == "Sechskant")
                    {
                        cc.Sechskant();
                        Console.WriteLine("Sechskant");
                    }
                    
                    else if (Wunschschraubenkopf == "Senkkopf")
                    {
                        cc.Senkkopf();
                        Console.WriteLine("Senkkopf");

                        cc.Innensechskant();
                        Console.WriteLine("Innensechskant");
                    }

                    else if (Wunschschraubenkopf == "Gewindestift")
                    {
                        cc.Gewindestift();
                        Console.WriteLine("Gewindestift");
                    }
                                       
                    cc.Innensechskant();
                    Console.WriteLine("Innensechskant");

                    //Wann und wo den Schlitz anbinden
                    cc.Schlitz();
                    Console.WriteLine("Schlitz");
                }
                else
                {
                    Console.WriteLine("Laufende Catia Application nicht gefunden");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }
            Console.WriteLine("Fertig - Taste drücken.");
            Console.ReadKey();

        }

        static void Main(string[] args)
        {
            new CatiaControl();
        }
    }

}

