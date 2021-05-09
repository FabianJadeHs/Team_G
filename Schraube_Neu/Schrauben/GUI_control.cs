using System;
using System.Windows;

namespace Schrauben
{
    class GUI_control
    {
        public GUI_control()
        {
            Window fenster = new Window();
            GUI meinGUI = new GUI();            
            fenster.Title = "MeinFenster";
            fenster.SizeToContent = SizeToContent.WidthAndHeight;
            fenster.ResizeMode = ResizeMode.NoResize;

            fenster.Content = meinGUI;
            fenster.ShowDialog();

            Console.WriteLine("Press any key...");
            Console.ReadKey();

            
        }
               

    }
    
}
