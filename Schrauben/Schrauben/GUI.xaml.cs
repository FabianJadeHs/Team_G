﻿using System;
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

namespace Schrauben
{
    /// <summary>
    /// Interaktionslogik für GUI.xaml
    /// </summary>
    public partial class GUI : UserControl
    {
        public GUI()
        {
            InitializeComponent();
            lbl_Frage1.Visibility = Visibility.Hidden;
            lbl_Frage2.Visibility = Visibility.Hidden;
            lbl_Frage3.Visibility = Visibility.Hidden;
            lbl_Frage4.Visibility = Visibility.Hidden;
            lbl_Frage5.Visibility = Visibility.Hidden;
            cbx_Antwort1.Visibility = Visibility.Hidden;
        }
        
       
        private void btn_Schliessen_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_Konfigurieren_Click(object sender, RoutedEventArgs e)
        {
            lbl_Begruessung.Content = "";
            lbl_Frage1.Visibility = Visibility.Visible;
            cbx_Antwort1.Visibility = Visibility.Visible;
        }
        
    }
}
