using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Schrauben
{
    /// <summary>
    /// Interaktionslogik für GUI.xaml
    /// </summary>
    public partial class GUI : UserControl
    {
        public string[] Arten { get; set; }
        public string[] Regelgewinde { get; set; }
        public string[] Richtung { get; set; }
        public string[] Kopfarten { get; set; }
        public string[] Materialien { get; set; }

        #region Initialisierung
        //neues Objekt einer Klasse wird initialisiert
        Schraube Guiversuch = new Schraube();
        
        public GUI()
        {
            //Komponenten der GUI werden initialisiert
            InitializeComponent();
            tctl_Fenster.Visibility = Visibility.Hidden;
            btn_Berechnen.Visibility = Visibility.Hidden;
            img_Logo.Visibility = Visibility.Hidden;
            img_Schraubenschema.Visibility = Visibility.Hidden;
                  
            Arten = new string[] { "Regelgewinde", "Feingewinde", "Trapezgewinde" };
            Richtung = new string[] { "Rechtsgewinde", "Linksgewinde" };
            Kopfarten = new string[] { "Sechskant", "Zylinderkopf", "Senkkopf", "Gewindestift", };
            Materialien = new string[] { "Baustahl", "V4A", "Messing", "Aluminium", "Kupfer" };
            //Regelgewinde = new string[] {} 

            DataContext = this;
           
            
        }
        #endregion

        //Wenn Button geklickt wird, wird App geschlossen
        private void btn_Schliessen_Click(object sender, RoutedEventArgs e)
        {
            //Alles wird geschlossen
            Environment.Exit(0);
        }
        //Wenn Button geklickt wird
        private void btn_Konfigurieren_Click(object sender, RoutedEventArgs e)
        {
            //Objekte werden sichtbar gemacht
            lbl_Begruessung.Content = "";
            tctl_Fenster.Visibility = Visibility.Visible;
            btn_Berechnen.Visibility = Visibility.Visible;
            img_Logo.Visibility = Visibility.Visible;
            img_Schraubenschema.Visibility = Visibility.Visible;
            btn_Konfigurieren.Visibility = Visibility.Hidden;
        }
        
        //Wenn Combobox0 geschlossen wird
        private void cbx_Antwort0_DropDownClosed(object sender, EventArgs e)
        {
            //neue Tabellen werden deklariert
            Tabelle tab = new Tabelle();
            Materialtabelle tab2 = new Materialtabelle();
            
            //Abfrage nach dem Gewindetyp


            if (cbx_Antwort0.Text == "Regelgewinde")
            {

                Schraubenarray[] Regelgewind = new Schraubenarray[33];

                Array.Copy(tab.getAll(), 0, Regelgewind, 0, 33);            //Gibt vor, welche Zeilen des Arrays ausgegeben werden mit Beginn und Anzahl der nachfolgenden Zeilen
                //Schraubenarray wird zeilenweise durchgegangen
                foreach (Schraubenarray m in Regelgewind)

                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);

                }
            }

            
            if (cbx_Antwort0.Text == "Feingewinde")
            {
                Schraubenarray[] feinSchraubeArray = new Schraubenarray[51];

                Array.Copy(tab.getAll(), 34, feinSchraubeArray, 0, 51);        //Gibt vor, welche Zeilen des Arrays ausgegeben werden mit Beginn und Anzahl der nachfolgenden Zeilen
                //Schraubenarray wird zeilenweise durchgegangen
                foreach (Schraubenarray m in feinSchraubeArray)

                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);

                }
            }


            if (cbx_Antwort0.Text == "Trapezgewinde")
            {
                Schraubenarray[] trapezSchraubeArray = new Schraubenarray[24];

                Array.Copy(tab.getAll(), 85, trapezSchraubeArray, 0, 24);     //Gibt vor, welche Zeilen des Arrays ausgegeben werden mit Beginn und Anzahl der nachfolgenden Zeilen
                //Schraubenarray wird zeilenweise durchgegangen
                foreach (Schraubenarray m in trapezSchraubeArray)

                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);

                }
            }
        }
        //Wenn Combobox0 wieder geöffnet wird, wird gecleart
        private void cbx_Antwort0_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Antwort1.Items.Clear();
            //cbx_Antwort4.Items.Clear();
        }

        private void cbx_Antwort4_DropDownClosed(object sender, EventArgs e)    //Abfrage des Materials
        {

            cbx_Antwort8.Items.Clear();
            Festigkeitstabelle tab3 = new Festigkeitstabelle();

            //Nur die Auswahl Baustahl ermöglicht das Auswählen unterschiedlicher Festigkeitsklassen

            if (cbx_Antwort4.Text == "Baustahl")
            {
                Festigkeitsarray[] BaustahlFestigkeitsarray = new Festigkeitsarray[10];
                Array.Copy(tab3.getAll(), 0, BaustahlFestigkeitsarray, 0, 10);           //Stellt die entsprechenden Zeilen aus dem Array zur Auswahl dar
                foreach (Festigkeitsarray p in BaustahlFestigkeitsarray)

                {
                    cbx_Antwort8.Items.Add(p.Festigkeitsklassenbezeichnung);
                }
            }
            //Ausgabe wenn kein Baustahl ausgewählt wurde
            else
            {
                cbx_Antwort8.Items.Add("Standard Festigkeiten");
            }
        }

        private void NumbervalidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //nur Eingaben von 0 bis 9 sind möglich
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #region Variablenzuweisung
        private void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            
            //neues Objekt wird erzeugt
            Schraube test1 = new Schraube();

            //benötigte Variablen werden aus Eingaben in der GUI genommen
            test1.Wunschgewindeart = cbx_Antwort1.Text;
                       
            test1.Wunschgewindeart = test1.Wunschgewindeart.ToUpper();
            
            test1.Wunschgewindeart = test1.Wunschgewindeart.Replace(" ", String.Empty);

            test1.Wunschschraubenkopf = cbx_Antwort6.Text;

            test1.Wunschgewindelaenge = double.Parse(txtb_Antwort2.Text);
                                 
            test1.Wunschschaftlaenge = double.Parse(txtb_Antwort3.Text);

            test1.Wunschmaterial = cbx_Antwort4.Text;

            test1.Wunschfestigkeit = cbx_Antwort8.Text;

            test1.Wunschanzahl = double.Parse(txtb_Antwort5.Text);

            //Unterprogramme werden abgerufen und geben Werte aus
            double rundung = test1.Rundung();
            richTextBox.AppendText("Die Rundung beträgt " + Math.Round(rundung,3) + " mm." + Environment.NewLine);

            test1.Kopfvolumen();

            double volumen = test1.Volumen();
            richTextBox.AppendText("Das Volumen einer Schraube beträgt " + Math.Round(volumen,2) + " mm³." + Environment.NewLine);

            double gewicht = test1.Gewicht();
            richTextBox.AppendText("Das Gewicht einer Schraube beträgt " + Math.Round(gewicht,2) + " in g." + Environment.NewLine);

            double preis = test1.Preis();
            richTextBox.AppendText("Der Preis aller Schrauben beziffert sich auf " + Math.Round(preis,2) + " Euro insgesamt." + Environment.NewLine);

            double schwerpunkt = test1.Schwerpunkt();
            richTextBox.AppendText("Der Schwerpunkt liegt " + Math.Round(schwerpunkt,2) + " mm unterhalb des Schraubenkopfes" + Environment.NewLine);

            double spannungsquerschnitt = test1.Spannungsquerschnitt();
            richTextBox.AppendText("Der Spannungsquerschnitt einer Schraube beträgt " + Math.Round(spannungsquerschnitt,2) + " mm²." + Environment.NewLine);

            double ftm = test1.Flaechentraegheitsmoment();
            richTextBox.AppendText("Das Flächenträgheitsmoment beträgt " + Math.Round(ftm,2) + " mm^4" + Environment.NewLine);

            double vorspannkraft = test1.Vorspannkraft();
            richTextBox.AppendText("Vorspannkraft beträgt " + Math.Round(vorspannkraft,2) + " N" + Environment.NewLine);

            double schluesselweite = test1.Schluesselweite();
            richTextBox.AppendText("Die Schluesselweite beträgt " + schluesselweite  + Environment.NewLine);

            double steigung = test1.Steigung();
            richTextBox.AppendText("Die Steigung beträgt " + steigung + Environment.NewLine);
                       
        }

        #endregion

       
    }
}
