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
        //Wenn Combobox0 geklickt wird
        private void cbx_Antwort0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Objekte werden sichtbar gemacht
            lbl_Frage1.Visibility = Visibility.Visible;
            cbx_Antwort1.Visibility = Visibility.Visible;
        }
        //Wenn Combobox1 geklickt wird
        private void cbx_Antwort1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tabelle tab = new Tabelle();

            foreach (Schraubenarray m in tab.getAll())
            {
                if (cbx_Antwort0.Text == "Standardgewinde")
                {
                    for (int i = 0; i <= 33; i++)
                    {
                        cbx_Antwort1.Items.Add(m.Gewindebezeichnung[i]);
                    }
                }

                if (cbx_Antwort0.Text == "Feingewinde")
                {
                    Environment.Exit(0);
                }
                if (cbx_Antwort0.Text == "Trapezgewinde")
                {

                }
                
            }
            
        }
    }
}
