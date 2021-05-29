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

                    Produkt dieSchraube = new Produkt(60, 50, "Standardgewinde", "M8", 5.3d, 4d, 1.25d);     //Was für Daten will er hier in der Klammer haben????

                    cc.ErzeugeZylinder(dieSchraube);
                    Console.WriteLine("Schaft");

                    
                    cc.ErzeugeGewindeHelix(dieSchraube);
                    Console.WriteLine("Gewinde");

                    if (dieSchraube.Wunschschraubenkopf == "Zylinderkopf")       
                    {
                        cc.Zylinderkopf(dieSchraube);
                        Console.WriteLine("Zylinderkopf");

                        cc.InnensechskantZ(dieSchraube);
                        Console.WriteLine("Innensechskant");
                    }

                    else if (dieSchraube.Wunschschraubenkopf == "Sechskant")
                    {
                        cc.Sechskant(dieSchraube);
                        Console.WriteLine("Sechskant");
                    }
                    
                    else if (dieSchraube.Wunschschraubenkopf == "Senkkopf")
                    {
                        cc.Senkkopf(dieSchraube);
                        Console.WriteLine("Senkkopf");

                        cc.InnensechskantS(dieSchraube);
                        Console.WriteLine("Innensechskant");
                    }

                    else if (dieSchraube.Wunschschraubenkopf == "Gewindestift")
                    {
                        cc.Gewindestift(dieSchraube);
                        Console.WriteLine("Gewindestift");

                        cc.InnensechskantGS(dieSchraube);
                        Console.WriteLine("Innensechskant");
                    }
                                       
                    

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

