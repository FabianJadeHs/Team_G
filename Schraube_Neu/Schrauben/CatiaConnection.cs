using System;
using System.Windows;
using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using PARTITF;

namespace Schrauben
{
    class CatiaConnection
    {
        INFITF.Application hsp_catiaApp;
        MECMOD.PartDocument hsp_catiaPartDoc;
        MECMOD.Sketch hsp_catiaSkizze;

        ShapeFactory SF;
        HybridShapeFactory HSF;
        Pad mySchaft;
        Body myBody;
        Part myPart;
        Sketches mySketches;
        public Reference kopfFlaeche;

        #region MinimalCatia
        public bool CATIALaeuft()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject(
                    "CATIA.Application");
                hsp_catiaApp = (INFITF.Application)catiaObject;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean ErzeugePart()
        {
            INFITF.Documents catDocuments1 = hsp_catiaApp.Documents;
            hsp_catiaPartDoc = catDocuments1.Add("Part") as MECMOD.PartDocument;

            return true;
        }

        public void ErstelleLeereSkizze()
        {
            // geometrisches Set auswaehlen und umbenennen
            SF = (ShapeFactory)hsp_catiaPartDoc.Part.ShapeFactory;
            HybridBodies catHybridBodies1 = hsp_catiaPartDoc.Part.HybridBodies;
            HybridBody catHybridBody1;
            try
            {
                catHybridBody1 = catHybridBodies1.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Kein geometrisches Set gefunden! " + Environment.NewLine +
                    "Ein PART manuell erzeugen und ein darauf achten, dass 'Geometisches Set' aktiviert ist.",
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catHybridBody1.set_Name("Profile");
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            mySketches = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaSkizze = mySketches.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();
        }

        private void ErzeugeAchsensystem()
        {
            object[] arr = new object[] {0.0, 0.0, 0.0,
                                         0.0, 1.0, 0.0,
                                         0.0, 0.0, 1.0 };
            hsp_catiaSkizze.SetAbsoluteAxisData(arr);
        }
        #endregion

        #region Schraube
        internal void ErzeugeZylinder(Schraube mySchraube)
        {
            double Ri = mySchraube.Nenndurchmesser() / 2;

            myPart = hsp_catiaPartDoc.Part;
            Bodies bodies = myPart.Bodies;
            myBody = myPart.MainBody;
            // myBody = bodies.Add();

            // Hauptkoerper in Bearbeitung definieren
            myPart.InWorkObject = myPart.MainBody;

            // Skizze umbenennen
            hsp_catiaSkizze.set_Name("Kreis");

            // Skizze...
            // ... oeffnen für die Bearbeitung
            Factory2D catFactory2D1 = hsp_catiaSkizze.OpenEdition();

            // ... Kreis erstellen
            double H0 = 0;
            double V0 = 0;
            Point2D Ursprung = catFactory2D1.CreatePoint(H0, V0);
            Circle2D Kreis = catFactory2D1.CreateCircle(H0, V0, Ri, 0, 0);
            Kreis.CenterPoint = Ursprung;

            // ... schliessen
            hsp_catiaSkizze.CloseEdition();

            // Schraubenschaft durch ein Pad erstellen
            Reference RefMySchaft = myPart.CreateReferenceFromObject(hsp_catiaSkizze);
            mySchaft = SF.AddNewPadFromRef(RefMySchaft, mySchraube.Wunschgewindelaenge+mySchraube.Wunschschaftlaenge);
            myPart.Update();

        }                  

        // Erzeugt eine Helix 
        internal void ErzeugeGewindeHelix(Schraube mySchraube)
        {
            double P = mySchraube.Steigung();
            double Ri =mySchraube.Nenndurchmesser()/2;
            double l = mySchraube.Wunschschaftlaenge+mySchraube.Wunschgewindelaenge;
            HSF = (HybridShapeFactory)myPart.HybridShapeFactory;

            Sketch myGewinde = makeGewindeSkizze(mySchraube);

            HybridShapeDirection HelixDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefHelixDir = myPart.CreateReferenceFromObject(HelixDir);

            HybridShapePointCoord HelixStartpunkt = HSF.AddNewPointCoord(0, 0, Ri);
            Reference RefHelixStartpunkt = myPart.CreateReferenceFromObject(HelixStartpunkt);

            HybridShapeHelix Helix = HSF.AddNewHelix(RefHelixDir, false, RefHelixStartpunkt, P, mySchraube.Wunschgewindelaenge, false, 0, 0, false);

            Reference RefHelix = myPart.CreateReferenceFromObject(Helix);
            Reference RefmyGewinde = myPart.CreateReferenceFromObject(myGewinde);

            myPart.Update();

            myPart.InWorkObject = myBody;

            OriginElements catOriginElements = this.hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            Sketch ChamferSkizze = mySketches.Add(RefmyPlaneZX);
            myPart.InWorkObject = ChamferSkizze;
            ChamferSkizze.set_Name("Fase");

            double H_links = Ri;
            double V_links = l-0.65 * P;

            double H_rechts = Ri;
            double V_rechts = l;

            double H_unten = Ri - 0.65 * P;
            double V_unten = l;

            Factory2D catFactory2D3 = ChamferSkizze.OpenEdition();

            Point2D links = catFactory2D3.CreatePoint(H_links, V_links);
            Point2D rechts = catFactory2D3.CreatePoint(H_rechts, V_rechts);
            Point2D unten = catFactory2D3.CreatePoint(H_unten, V_unten);

            Line2D Oben = catFactory2D3.CreateLine(H_links, V_links, H_rechts, V_rechts);
            Oben.StartPoint = links;
            Oben.EndPoint = rechts;

            Line2D hypo = catFactory2D3.CreateLine(H_links, V_links, H_unten, V_unten);
            hypo.StartPoint = links;
            hypo.EndPoint = unten;

            Line2D seite = catFactory2D3.CreateLine(H_unten, V_unten, H_rechts, V_rechts);
            seite.StartPoint = unten;
            seite.EndPoint = rechts;

            ChamferSkizze.CloseEdition();

            myPart.InWorkObject = myBody;
            myPart.Update();

            Groove myChamfer = SF.AddNewGroove(ChamferSkizze);
            myChamfer.RevoluteAxis = RefHelixDir;

            myPart.Update();

            Slot GewindeRille = SF.AddNewSlotFromRef(RefmyGewinde, RefHelix);

            Reference RefmyPad = myPart.CreateReferenceFromObject(mySchaft);
            HybridShapeSurfaceExplicit GewindestangenSurface = HSF.AddNewSurfaceDatum(RefmyPad);
            Reference RefGewindestangenSurface = myPart.CreateReferenceFromObject(GewindestangenSurface);

            GewindeRille.ReferenceSurfaceElement = RefGewindestangenSurface;

            Reference RefGewindeRille = myPart.CreateReferenceFromObject(GewindeRille);

            myPart.Update();
        }

        // Separate Skizzenerzeugung für de Helix
        private Sketch makeGewindeSkizze(Schraube mySchraube)
        {
            double P = mySchraube.Steigung();
            double Ri = mySchraube.Nenndurchmesser()/2;
            double l = mySchraube.Wunschgewindelaenge + mySchraube.Wunschschaftlaenge;

            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            Sketch myGewinde = mySketches.Add(RefmyPlaneZX);
            myPart.InWorkObject = myGewinde;
            myGewinde.set_Name("Gewinde");

            double V_oben_links = l-(((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180));
            double H_oben_links = Ri;

            double V_oben_rechts = l+(((((Math.Sqrt(3) / 2) * P) / 6) + 0.6134 * P) * Math.Tan((30 * Math.PI) / 180));
            double H_oben_rechts = Ri;

            double V_unten_links = l-((0.1443 * P) * Math.Sin((60 * Math.PI) / 180));
            double H_unten_links = Ri - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_unten_rechts =l+(0.1443 * P) * Math.Sin((60 * Math.PI) / 180);
            double H_unten_rechts = Ri - (0.6134 * P - 0.1443 * P) - Math.Sqrt(Math.Pow((0.1443 * P), 2) - Math.Pow((0.1443 * P) * Math.Sin((60 * Math.PI) / 180), 2));

            double V_Mittelpunkt = l;
            double H_Mittelpunkt = Ri - ((0.6134 * P) - 0.1443 * P);

            Factory2D catFactory2D2 = myGewinde.OpenEdition();

            Point2D Oben_links = catFactory2D2.CreatePoint(H_oben_links, V_oben_links);
            Point2D Oben_rechts = catFactory2D2.CreatePoint(H_oben_rechts, V_oben_rechts);
            Point2D Unten_links = catFactory2D2.CreatePoint(H_unten_links, V_unten_links);
            Point2D Unten_rechts = catFactory2D2.CreatePoint(H_unten_rechts, V_unten_rechts);
            Point2D Mittelpunkt = catFactory2D2.CreatePoint(H_Mittelpunkt, V_Mittelpunkt);

            Line2D Oben = catFactory2D2.CreateLine(H_oben_links, V_oben_links, H_oben_rechts, V_oben_rechts);
            Oben.StartPoint = Oben_links;
            Oben.EndPoint = Oben_rechts;

            Line2D Rechts = catFactory2D2.CreateLine(H_oben_rechts, V_oben_rechts, H_unten_rechts, V_unten_rechts);
            Rechts.StartPoint = Oben_rechts;
            Rechts.EndPoint = Unten_rechts;

            Circle2D Verrundung = catFactory2D2.CreateCircle(H_Mittelpunkt, V_Mittelpunkt, 0.1443 * P, 0, 0);
            Verrundung.CenterPoint = Mittelpunkt;
            Verrundung.StartPoint = Unten_rechts;
            Verrundung.EndPoint = Unten_links;

            Line2D Links = catFactory2D2.CreateLine(H_oben_links, V_oben_links, H_unten_links, V_unten_links);
            Links.StartPoint = Unten_links;
            Links.EndPoint = Oben_links;

            myGewinde.CloseEdition();
            myPart.Update();

            return myGewinde;
        }

        #endregion

        internal void Zylinderkopf(Schraube mySchraube)
        {
            //myKopf=Skizze
            //in CatiaControl Klasse aufrufen
            //CreateCircle(H0, V0, 20, 0, 0)=(Höhe,Breite,Radius,0,0)

            double R = mySchraube.KopfdurchmesserZ()/2;
            double H = mySchraube.KopfhoeheZ();


            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneYZ = (Reference)catOriginElements.PlaneYZ;

            Sketch myZylinderkopf = mySketches.Add(RefmyPlaneYZ);
            myPart.InWorkObject = myZylinderkopf;
            myZylinderkopf.set_Name("Kopf");



            ///myPart = hsp_catiaPartDoc.Part;
            ///Bodies bodies = myPart.Bodies;
            ///myBody = myPart.MainBody;
            // myBody = bodies.Add();

            // Hauptkoerper in Bearbeitung definieren
            myPart.InWorkObject = myPart.MainBody;

            // Skizze umbenennen
            ///myKopf.set_Name("Kopf");

            // Skizze...
            // ... oeffnen für die Bearbeitung
            Factory2D catFactory2D1 = myZylinderkopf.OpenEdition();

            // ... Kreis erstellen
            double H0 = 0;
            double V0 = 0;
            Point2D Ursprung = catFactory2D1.CreatePoint(H0, V0);
            Circle2D Kreis = catFactory2D1.CreateCircle(H0, V0, R, 0, 0);
            Kreis.CenterPoint = Ursprung;

            // ... schliessen
            myZylinderkopf.CloseEdition();

            // Schraubenschaft durch ein Pad erstellen
            Reference RefMySchaft = myPart.CreateReferenceFromObject(myZylinderkopf);
            mySchaft = SF.AddNewPadFromRef(RefMySchaft, -H);
            myPart.Update();

            kopfFlaeche = myPart.CreateReferenceFromName("Selection_RSur:(Face:(Brp:(Pad.2;2);None:();Cf12:());Pad.2_ResultOUT;Z0;G9061)");

        }

        internal void Sechskant(Schraube mySchraube)
        {
            double SW = mySchraube.Schluesselweite()/2;
            double K = 2 * SW / Math.Sqrt(3);
            double T = mySchraube.KopfhoeheSE();



            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneYZ = (Reference)catOriginElements.PlaneYZ;

            Sketch mySechskant = mySketches.Add(RefmyPlaneYZ);
            myPart.InWorkObject = mySechskant;
            mySechskant.set_Name("Sechskantkopf");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = mySechskant.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-K / 2, SW);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(-K, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-K / 2, -SW);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(K / 2, -SW);
            // meine Punkte
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(K, 0);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(K / 2, SW);



            // dann die Linien im Uhrzeiger oben links start

            Line2D catLine2D1 = catFactory2D1.CreateLine(-K / 2, SW, -K, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(-K, 0, -K / 2, -SW);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(-K / 2, -SW, K / 2, -SW);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(K / 2, -SW, K, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;
            //meine
            Line2D catLine2D5 = catFactory2D1.CreateLine(K, 0, K / 2, SW);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(K / 2, SW, -K / 2, SW);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;




            // Skizzierer verlassen
            mySechskant.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPartDoc.Part.ShapeFactory;
            Pad catPad1 = catShapeFactory1.AddNewPad(mySechskant, -T);

            // Block umbenennen
            catPad1.set_Name("Sechskant");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();


        }

        internal void Senkkopf(Schraube mySchraube)
        {
            double KR = mySchraube.KopfhoeheS();       //Stmmt nicht ganz mit der wirklichen Höhe überein, muss größer als die Höhe sein!!!!

            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneZX = (Reference)catOriginElements.PlaneZX;

            HybridShapeDirection Dir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefDir = myPart.CreateReferenceFromObject(Dir);

            Sketch mySenkkopf = mySketches.Add(RefmyPlaneZX);
            myPart.InWorkObject = mySenkkopf;
            mySenkkopf.set_Name("Senkkopf");

            myPart.InWorkObject = myPart.MainBody;

            Factory2D catFactory2D1 = mySenkkopf.OpenEdition();

            //Kopfmitte
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(0, 0);
            //Kopfaußen
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(KR, 0);
            //Untenmitte
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(0, KR);

            Line2D catLine2D1 = catFactory2D1.CreateLine(0, 0, KR, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(KR, 0, -0, KR);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(0, KR, 0, 0);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D1;


            // Skizzierer verlassen
            mySenkkopf.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            Shaft senkkopf = SF.AddNewShaft(mySenkkopf);
            senkkopf.RevoluteAxis = RefDir;
            senkkopf.set_Name("Senkkopf");

            hsp_catiaPartDoc.Part.Update();

            kopfFlaeche = myPart.CreateReferenceFromName("Selection_RSur:(Face:(Brp:((Brp:(Shaft.1;0:(Brp:(Sketch.3;1)));Brp:(Pad.1;1)));None:();Cf12:());Shaft.1_ResultOUT;Z0;G9061)");


        }

        internal void Gewindestift(Schraube mySchraube)
        {
            /*
            double SW = 10;
            double K = 2 * SW / Math.Sqrt(3);



            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference RefmyPlaneYZ = (Reference)catOriginElements.PlaneYZ;

            Sketch mySechskant = mySketches.Add(RefmyPlaneYZ);
            myPart.InWorkObject = mySechskant;
            mySechskant.set_Name("Sechskantkopf");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = mySechskant.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-K / 2, SW);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(-K, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-K / 2, -SW);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(K / 2, -SW);
            // meine Punkte
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(K, 0);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(K / 2, SW);



            // dann die Linien im Uhrzeiger oben links start

            Line2D catLine2D1 = catFactory2D1.CreateLine(-K / 2, SW, -K, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(-K, 0, -K / 2, -SW);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(-K / 2, -SW, K / 2, -SW);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(K / 2, -SW, K, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;
            //meine
            Line2D catLine2D5 = catFactory2D1.CreateLine(K, 0, K / 2, SW);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(K / 2, SW, -K / 2, SW);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;




            // Skizzierer verlassen
            mySechskant.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPartDoc.Part.ShapeFactory;
            Pad catPad1 = catShapeFactory1.AddNewPad(mySechskant, -5);

            // Block umbenennen
            catPad1.set_Name("Sechkant");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            */
        }

        internal void InnensechskantZ(Schraube mySchraube)
        {
            //Innenschlüsselweite
            double Ib = mySchraube.InnensechskantZ()/2;
            //Kantenlänge
            double K = 2 * Ib / Math.Sqrt(3);
            //tiefe 
            double T = mySchraube.SechskanttiefeZ();



            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference refPlane = kopfFlaeche;

            Sketch myInnensechskant = mySketches.Add(refPlane);
            myPart.InWorkObject = myInnensechskant;
            myInnensechskant.set_Name("Innensechskant");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = myInnensechskant.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-K / 2, Ib);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(-K, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-K / 2, -Ib);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(K / 2, -Ib);
            // meine Punkte
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(K, 0);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(K / 2, Ib);



            // dann die Linien im Uhrzeiger oben links start

            Line2D catLine2D1 = catFactory2D1.CreateLine(-K / 2, Ib, -K, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(-K, 0, -K / 2, -Ib);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(-K / 2, -Ib, K / 2, -Ib);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(K / 2, -Ib, K, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;
            //meine
            Line2D catLine2D5 = catFactory2D1.CreateLine(K, 0, K / 2, Ib);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(K / 2, Ib, -K / 2, Ib);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;




            // Skizzierer verlassen
            myInnensechskant.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            Pocket Loch = SF.AddNewPocket(myInnensechskant, T);
            Loch.set_Name("Innensechskant");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();



        }

        internal void InnensechskantS(Schraube mySchraube)
        {
            //Innenschlüsselweite
            double Ib = mySchraube.InnensechskantS();
            //Kantenlänge
            double K = 2 * Ib / Math.Sqrt(3);
            //tiefe 
            double T = mySchraube.SechskanttiefeS();



            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference refPlane = kopfFlaeche;

            Sketch myInnensechskant = mySketches.Add(refPlane);
            myPart.InWorkObject = myInnensechskant;
            myInnensechskant.set_Name("Innensechskant");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = myInnensechskant.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-K / 2, Ib);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(-K, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-K / 2, -Ib);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(K / 2, -Ib);
            // meine Punkte
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(K, 0);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(K / 2, Ib);



            // dann die Linien im Uhrzeiger oben links start

            Line2D catLine2D1 = catFactory2D1.CreateLine(-K / 2, Ib, -K, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(-K, 0, -K / 2, -Ib);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(-K / 2, -Ib, K / 2, -Ib);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(K / 2, -Ib, K, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;
            //meine
            Line2D catLine2D5 = catFactory2D1.CreateLine(K, 0, K / 2, Ib);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(K / 2, Ib, -K / 2, Ib);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;




            // Skizzierer verlassen
            myInnensechskant.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            Pocket Loch = SF.AddNewPocket(myInnensechskant, T);
            Loch.set_Name("Innensechskant");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();



        }

        internal void InnensechskantGS(Schraube mySchraube)
        {
            //Innenschlüsselweite
            double Ib = mySchraube.InnensechskantGS();
            //Kantenlänge
            double K = 2 * Ib / Math.Sqrt(3);
            //tiefe 
            double T = mySchraube.SechskanttiefeGS();



            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference refPlane = kopfFlaeche;

            Sketch myInnensechskant = mySketches.Add(refPlane);
            myPart.InWorkObject = myInnensechskant;
            myInnensechskant.set_Name("Innensechskant");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = myInnensechskant.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-K / 2, Ib);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(-K, 0);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(-K / 2, -Ib);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(K / 2, -Ib);
            // meine Punkte
            Point2D catPoint2D5 = catFactory2D1.CreatePoint(K, 0);
            Point2D catPoint2D6 = catFactory2D1.CreatePoint(K / 2, Ib);



            // dann die Linien im Uhrzeiger oben links start

            Line2D catLine2D1 = catFactory2D1.CreateLine(-K / 2, Ib, -K, 0);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(-K, 0, -K / 2, -Ib);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(-K / 2, -Ib, K / 2, -Ib);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(K / 2, -Ib, K, 0);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D5;
            //meine
            Line2D catLine2D5 = catFactory2D1.CreateLine(K, 0, K / 2, Ib);
            catLine2D5.StartPoint = catPoint2D5;
            catLine2D5.EndPoint = catPoint2D6;

            Line2D catLine2D6 = catFactory2D1.CreateLine(K / 2, Ib, -K / 2, Ib);
            catLine2D6.StartPoint = catPoint2D6;
            catLine2D6.EndPoint = catPoint2D1;




            // Skizzierer verlassen
            myInnensechskant.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            Pocket Loch = SF.AddNewPocket(myInnensechskant, T);
            Loch.set_Name("Innensechskant");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();



        }

        internal void Schlitz()
        {
            OriginElements catOriginElements = hsp_catiaPartDoc.Part.OriginElements;
            Reference refPlane = kopfFlaeche;

            Sketch mySchlitz = mySketches.Add(refPlane);
            myPart.InWorkObject = mySchlitz;
            mySchlitz.set_Name("Sechskantkopf");

            myPart.InWorkObject = myPart.MainBody;


            // Rechteck in Skizze einzeichnen
            // Skizze oeffnen
            Factory2D catFactory2D1 = mySchlitz.OpenEdition();

            // Rechteck erzeugen

            // erst die Punkte
            //oben links gegen Uhrzeigersein
            Point2D catPoint2D1 = catFactory2D1.CreatePoint(-1.5, 100);
            Point2D catPoint2D2 = catFactory2D1.CreatePoint(1.5, 100);
            Point2D catPoint2D3 = catFactory2D1.CreatePoint(1.5, -100);
            Point2D catPoint2D4 = catFactory2D1.CreatePoint(-1.5, -100);

            Line2D catLine2D1 = catFactory2D1.CreateLine(-1.5, 100, 1.5, 100);
            catLine2D1.StartPoint = catPoint2D1;
            catLine2D1.EndPoint = catPoint2D2;

            Line2D catLine2D2 = catFactory2D1.CreateLine(1.5, 100, 1.5, -100);
            catLine2D2.StartPoint = catPoint2D2;
            catLine2D2.EndPoint = catPoint2D3;

            Line2D catLine2D3 = catFactory2D1.CreateLine(1.5, -100, -1.5, -100);
            catLine2D3.StartPoint = catPoint2D3;
            catLine2D3.EndPoint = catPoint2D4;

            Line2D catLine2D4 = catFactory2D1.CreateLine(-1.5, -100, -1.5, 100);
            catLine2D4.StartPoint = catPoint2D4;
            catLine2D4.EndPoint = catPoint2D1;

            // Skizzierer verlassen
            mySchlitz.CloseEdition();
            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();

            hsp_catiaPartDoc.Part.InWorkObject = hsp_catiaPartDoc.Part.MainBody;

            // Block(Balken) erzeugen
            Pocket Loch = SF.AddNewPocket(mySchlitz, 3);
            Loch.set_Name("Schlitz");

            // Part aktualisieren
            hsp_catiaPartDoc.Part.Update();
        }

    }

}

