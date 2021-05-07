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
            //Regelgewinde = new string[] {} 

            DataContext = this;
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
        }
        //Wenn Combobox0 geschlossen wird
        private void cbx_Antwort0_DropDownClosed(object sender, EventArgs e)
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();
            Materialtabelle tab2 = new Materialtabelle();

            //Schraubenarray wird zeilenweise durchgegangen
            foreach (Schraubenarray m in tab.getAll())
            {
                //wenn Regelgewinde ausgewähl, dann wird nur passendes angezeigt
                //passt noch nicht, weil alles angezeigt wird
                
                if (cbx_Antwort0.Text == "Regelgewinde")
                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);

                    /*
                    int j = 0;
                    while(m.Gewindebezeichnung != "0")
                    {
                        cbx_Antwort1.Items.Add(m.Gewindebezeichnung{j};);
                        j++;
                    }
                    */                    
                }
                //Wenn Feingewinde ausgewählt
                if (cbx_Antwort0.Text == "Feingewinde")
                {
                    //Alles wird geschlossen
                    Environment.Exit(0);
                }
                //Wenn Trapezgewinde ausgewählt
                if (cbx_Antwort0.Text == "Trapezgewinde")
                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);
                }
                
            }
            //beispielsweise comboboxfüllung
            
            foreach (Materialarray n in tab2.getAll())
            {
                cbx_Antwort4.Items.Add(n.Materialbezeichnung);
            }

        }
        //Wenn Combobox0 wieder geöffnet wird, wird gecleart
        private void cbx_Antwort0_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Antwort1.Items.Clear();
            cbx_Antwort4.Items.Clear();
        }
        //Wenn Combobox1 geschlossen wird
        private void cbx_Antwort1_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void cbx_Antwort4_DropDownClosed(object sender, EventArgs e)
        {


        }
        private void cbx_Antwort6_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void cbx_Antwort7_DropDownClosed(object sender, EventArgs e)
        {

        }
        private void NumbervalidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #region Variablenzuweisung
        private void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            //Variablenzuweisung
            cbx_Antwort1.SelectedItem = Guiversuch.Wunschgewindeart;
            cbx_Antwort4.SelectedItem = Guiversuch.Wunschmaterial;
            cbx_Antwort6.SelectedItem = Guiversuch.Wunschschraubenkopf;
            Guiversuch.Wunschgewindelaenge = double.Parse(txtb_Antwort2.Text);
            Guiversuch.Wunschschaftlaenge = double.Parse(txtb_Antwort3.Text);
            Guiversuch.Wunschanzahl = double.Parse(txtb_Antwort5.Text);
        }
        #endregion
    }
}
