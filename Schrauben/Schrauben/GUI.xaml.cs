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
        

        //neues Objekt einer Klasse wird initialisiert
        Schraube Guiversuch = new Schraube();
        public GUI()
        {
            //Komponenten der GUI werden initialisiert
            InitializeComponent();
            lbl_Frage0.Visibility = Visibility.Hidden;
            lbl_Frage1.Visibility = Visibility.Hidden;
            lbl_Frage2.Visibility = Visibility.Hidden;
            lbl_Frage3.Visibility = Visibility.Hidden;
            lbl_Frage4.Visibility = Visibility.Hidden;
            lbl_Frage5.Visibility = Visibility.Hidden;
            lbl_Frage6.Visibility = Visibility.Hidden;
            lbl_Frage7.Visibility = Visibility.Hidden;
            lbl_Frage8.Visibility = Visibility.Hidden;
            cbx_Antwort0.Visibility = Visibility.Hidden;
            cbx_Antwort1.Visibility = Visibility.Hidden;
            txtb_Antwort2.Visibility = Visibility.Hidden;
            
            txtb_Antwort3.Visibility = Visibility.Hidden;
            cbx_Antwort4.Visibility = Visibility.Hidden;
            txtb_Antwort5.Visibility = Visibility.Hidden;
            cbx_Antwort6.Visibility = Visibility.Hidden;
            cbx_Antwort7.Visibility = Visibility.Hidden;
            cbx_Antwort8.Visibility = Visibility.Hidden;
                  
            Arten = new string[] { "Regelgewinde", "Feingewinde", "Trapezgewinde" };
            //Regelgewinde = new string[] {} 

            DataContext = this;
            DataContext = this;
            
        }
        
        //Wenn Button geklickt wird, wird App geschlossen
        private void btn_Schliessen_Click(object sender, RoutedEventArgs e)
        {
            
        }
        //Wenn Button geklickt wird
        private void btn_Konfigurieren_Click(object sender, RoutedEventArgs e)
        {
            //Objekte werden sichtbar gemacht
            lbl_Begruessung.Content = "";
            lbl_Frage0.Visibility = Visibility.Visible;
            cbx_Antwort0.Visibility = Visibility.Visible;
        }
        public void einlesen()
        {
            
        }
        //Wenn Combobox0 geschlossen wird
        private void cbx_Antwort0_DropDownClosed(object sender, EventArgs e)
        {
            //neue Tabelle wird deklariert
            Tabelle tab = new Tabelle();

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
            //Objekte werden sichtbar gemacht
            lbl_Frage1.Visibility = Visibility.Visible;
            cbx_Antwort1.Visibility = Visibility.Visible;
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
            //Variablenzuweisung
            cbx_Antwort1.SelectedItem = Guiversuch.Wunschgewindeart;
            //nächste Frage und Antwortmöglichkeit werden sichbar gemacht
            lbl_Frage2.Visibility = Visibility.Visible;
            txtb_Antwort2.Visibility = Visibility.Visible;
        }

        
        private void cbx_Antwort4_DropDownClosed(object sender, EventArgs e)
        {
            //Variablenzuweisung
            cbx_Antwort4.SelectedItem = Guiversuch.Wunschmaterial;
            //nächste Frage und Antwortmöglichkeit werden sichtbar gemacht
            lbl_Frage5.Visibility = Visibility.Visible;
            txtb_Antwort5.Visibility = Visibility.Visible;

        }

        private void cbx_Antwort6_DropDownClosed(object sender, EventArgs e)
        {
            //Variablenzuweisung
            cbx_Antwort6.SelectedItem = Guiversuch.Wunschschraubenkopf;
            //nächste Frage und Antwortmöglichkeit werden sichtbar gemacht
            lbl_Frage7.Visibility = Visibility.Visible;
            cbx_Antwort7.Visibility = Visibility.Visible;
            //beispielsweise comboboxfüllung
            cbx_Antwort7.Items.Add(2);
        }

        private void cbx_Antwort7_DropDownClosed(object sender, EventArgs e)
        {
            //nächste Frage und Antwortmöglichkeit werden sichtbar gemacht
            lbl_Frage8.Visibility = Visibility.Visible;
            cbx_Antwort8.Visibility = Visibility.Visible;

        }
        
        private void NumbervalidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
