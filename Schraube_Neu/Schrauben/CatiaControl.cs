using System;
using System.Windows;
using System.Collections.Generic;
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

        }

    }

    public class CatiaControl
    {
        public CatiaControl(Schraube schraube)
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
                                       
                    cc.ErzeugeZylinder(schraube);
                    Console.WriteLine("Schaft");
                                        
                    cc.ErzeugeGewindeHelix(schraube);
                    Console.WriteLine("Gewinde");

                    if (schraube.Wunschschraubenkopf == "Sechskant")
                    {
                        cc.Sechskant(schraube);
                        cc.SechskantRund(schraube);
                        cc.SechskantPlatte(schraube);
                        cc.SechsRadius(schraube);
                    }
                    else if(schraube.Wunschschraubenkopf=="Zylinderkopf mit Innensechskant")
                    {
                        cc.Zylinderkopf(schraube);
                        cc.InnensechskantZ(schraube);
                    }
                    else if(schraube.Wunschschraubenkopf=="Zylinderkopf mit Schlitz")
                    {
                        cc.Zylinderkopf(schraube);
                        cc.Schlitz();
                    }
                    else if(schraube.Wunschschraubenkopf=="Senkkopf mit Innensechskant")
                    {
                        cc.Senkkopf(schraube);
                        cc.InnensechskantS(schraube);
                    }
                    else if(schraube.Wunschschraubenkopf=="Senkkopf mit Schlitz")
                    {
                        cc.Senkkopf(schraube);
                        cc.Schlitz();
                    }
                    else if (schraube.Wunschschraubenkopf == "Gewindestift")
                    {
                        cc.InnensechskantGS(schraube);
                        cc.FaseGewindestift(schraube);
                    }

                    //cc.Zylinderkopf(schraube);
                    //Console.WriteLine("Zylinderkopf");

                    //cc.InnensechskantZ(schraube);
                    //Console.WriteLine("Innensechskant");
                    /*
                    cc.Sechskant(schraube);
                    Console.WriteLine("Sechskant");
                    
                    cc.Senkkopf(schraube);
                    Console.WriteLine("Senkkopf");

                    cc.InnensechskantS(schraube);
                    Console.WriteLine("Innensechskant");                
                                       
                    
                    cc.Gewindestift(schraube);
                    Console.WriteLine("Gewindestift");

                    cc.InnensechskantGS(schraube);
                    Console.WriteLine("Innensechskant");
                    */
                                       
                    

                    //Wann und wo den Schlitz anbinden
                    //cc.Schlitz();
                    //Console.WriteLine("Schlitz");
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
        /*
        static void Main(string[] args)
        {
            new CatiaControl();
        }
        */
    }

}

