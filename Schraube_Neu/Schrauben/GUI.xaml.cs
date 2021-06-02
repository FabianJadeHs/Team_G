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
using System.Globalization;

namespace Schrauben
{
    /// <summary>
    /// Interaktionslogik für GUI.xaml
    /// </summary>
    public partial class GUI : UserControl
    {
        
        public Schraube test1 = new Schraube();
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
            img_viertesSchema.Visibility = Visibility.Hidden;
            btn_Exportieren.Visibility = Visibility.Hidden;

            Arten = new string[] { "Regelgewinde", "Feingewinde", "Trapezgewinde" };
            Richtung = new string[] { "Rechtsgewinde", "Linksgewinde" };
            Kopfarten = new string[] { "Sechskant", "Zylinderkopf mit Innensechskant", "Zylinderkopf mit Schlitz", "Senkkopf mit Innensechskant", "Senkkopf mit Schlitz", "Gewindestift", };
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
            img_viertesSchema.Visibility = Visibility.Visible;
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
            cbx_Antwort1.MaxDropDownHeight = 240;
        }
        private void cbx_Antwort6_DropDownClosed(object sender, EventArgs e)
        {
            if (cbx_Antwort6.Text == "Gewindestift")
            {
                txtb_Antwort3.Text = "0";
                txtb_Antwort3.IsReadOnly = true;
                cbx_Antwort0.Text = "Regelgewinde";
                cbx_Antwort1.Items.Clear();

                Schraubenarray[] Regelgewind = new Schraubenarray[33];
                Tabelle tab = new Tabelle();
                Array.Copy(tab.getAll(), 0, Regelgewind, 0, 33);            //Gibt vor, welche Zeilen des Arrays ausgegeben werden mit Beginn und Anzahl der nachfolgenden Zeilen
                //Schraubenarray wird zeilenweise durchgegangen
                foreach (Schraubenarray m in Regelgewind)

                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);

                }

            }
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
        private void txtb_Antwort3_TextChanged(object sender, TextChangedEventArgs e)
        {
            test1.Wunschgewindeart = cbx_Antwort1.Text;

            double steigung = test1.Steigung();
            double ri = test1.Nenndurchmesser();
            if (txtb_Antwort3.Text == "")
            {
                txtb_Antwort3.Text = "0";
            }

            else
            {
                txtb_Antwort3.Background = Brushes.White;
                if (double.Parse(txtb_Antwort3.Text) > 15 * ri)
                {
                    txtb_Antwort3.Background = Brushes.Red;
                }
            }
        }

        private void txtb_Antwort2_TextChanged(object sender, TextChangedEventArgs e)
        {
            test1.Wunschgewindeart = cbx_Antwort1.Text;
            double steigung = test1.Steigung();
            double ri = test1.Nenndurchmesser();
            if ( txtb_Antwort2.Text == "")
            {
                txtb_Antwort2.Text = "0";
            }
            if (double.Parse(txtb_Antwort2.Text) < 3 * steigung)
            {
                txtb_Antwort2.Background = Brushes.Red;
            }
            else
            {
                txtb_Antwort2.Background = Brushes.White;
                if (double.Parse(txtb_Antwort2.Text) > 15 * ri)
                {
                    txtb_Antwort2.Background = Brushes.Red;
                }
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
            
            //eine einfache Methode die richTextBox leer zu machen
            richTextBox.SelectAll();

            richTextBox.Selection.Text="";

            //neues Objekt wird erzeugt
            //Schraube test1 = new Schraube();

            //benötigte Variablen werden aus Eingaben in der GUI genommen
            test1.Wunschgewindeart = cbx_Antwort1.Text;                     
         
            test1.Wunschschraubenkopf = cbx_Antwort6.Text;

            test1.Wunschgewindelaenge = double.Parse(txtb_Antwort2.Text);            
                                 
            test1.Wunschschaftlaenge = double.Parse(txtb_Antwort3.Text);

            test1.Wunschmaterial = cbx_Antwort4.Text;

            test1.Wunschfestigkeit = cbx_Antwort8.Text;

            test1.Wunschanzahl = double.Parse(txtb_Antwort5.Text);

            test1.Gewinderichtung = cbx_Antwort7.Text;

            test1.Gewindeart = cbx_Antwort0.Text;

            //Unterprogramme werden abgerufen und geben Werte aus
            //double rundung = test1.Rundung();
            //richTextBox.AppendText("Die Rundung beträgt " + Math.Round(rundung,3) + " mm." + Environment.NewLine+Environment.NewLine);

            double preis = test1.Preis();
            richTextBox.AppendText("Preis gesamt: " + Math.Round(preis, 2) + " Euro" + Environment.NewLine+Environment.NewLine);

            double gewicht = test1.Gewicht();
            richTextBox.AppendText("Gewicht einer Schraube: " + Math.Round(gewicht, 2) + " g" + Environment.NewLine);

            double volumen = test1.Volumen();
            richTextBox.AppendText("Volumen einer Schraube: " + Math.Round(volumen,2) + " mm³." + Environment.NewLine);
            
            double schwerpunkt = test1.Schwerpunkt();
            richTextBox.AppendText("Schwerpunkt: " + Math.Round(schwerpunkt,2) + " mm unterhalb des Schraubenkopfes" + Environment.NewLine);

            double spannungsquerschnitt = test1.Spannungsquerschnitt();
            richTextBox.AppendText("Spannungsquerschnitt: " + Math.Round(spannungsquerschnitt,2) + " mm²." + Environment.NewLine);

            double ftm = test1.Flaechentraegheitsmoment();
            richTextBox.AppendText("Flächenträgheitsmoment: " + Math.Round(ftm,2) + " mm^4" + Environment.NewLine);

            double vorspannkraft = test1.Vorspannkraft();
            richTextBox.AppendText("Vorspannkraft: " + Math.Round(vorspannkraft,2) + " N" + Environment.NewLine);

            double schluesselweite = test1.Schluesselweite();
            richTextBox.AppendText("Schluesselweite: " + schluesselweite  + Environment.NewLine);
            
            double steigung = test1.Steigung();
            richTextBox.AppendText("Steigung: " + steigung + Environment.NewLine);

            double ri = test1.Nenndurchmesser();
            richTextBox.AppendText(("Nenndurchmesser: ") + ri + Environment.NewLine);

            //Button Exportieren sichtbar schalten
            btn_Exportieren.Visibility = Visibility.Visible;

        }

        #endregion

        public void btn_Exportieren_Click(object sender, RoutedEventArgs e)
        {
            new CatiaControl(test1);
        }


    }
}
