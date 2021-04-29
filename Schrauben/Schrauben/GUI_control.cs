using System;
using System.Windows;



namespace Schrauben
{
    class  GUI_control
    {
        public GUI_control()
        {
            Window fenster = new Window();
            GUI meinGUI = new GUI();

            fenster.Content = meinGUI;
            fenster.ShowDialog();

        }

        [STAThread]

    }
}
