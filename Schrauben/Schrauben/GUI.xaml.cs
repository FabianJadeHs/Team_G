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
            cbx_Antwort0.Visibility = Visibility.Hidden;
            cbx_Antwort1.Visibility = Visibility.Hidden;

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

            Tabelle tab = new Tabelle();

            foreach (Schraubenarray m in tab.getAll())
            {
                if (cbx_Antwort0.Text == "Regelgewinde")
                {
                    cbx_Antwort1.Items.Add(m.Gewindebezeichnung);
                    
                    for (int i = 0; i<=33; i++)
                    {

                    }
                    
                }
            }
            //Objekte werden sichtbar gemacht
            lbl_Frage1.Visibility = Visibility.Visible;
            cbx_Antwort1.Visibility = Visibility.Visible;
            

            /*if (cbx_Antwort0.Text == "Regelgewinde")
            {
                //cbx_Antwort1.Items.Add(Guiversuch.Gewindebezeichnung)
            }*/
           
            if (cbx_Antwort0.Text == "Feingewinde")
            {
                //Alles wird geschlossen
                Environment.Exit(0);
            }


        }
        private void cbx_Antwort0_DropDownOpened(object sender, EventArgs e)
        {
            cbx_Antwort1.Items.Clear();
        }
        //Wenn Combobox1 geschlossen wird
        private void cbx_Antwort1_DropDownClosed(object sender, EventArgs e)
        {

            
        }


    }
}
