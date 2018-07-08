using System;
using System.Windows.Forms;

namespace HW_Klyushin_1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form form = new SplashScreen();
            form.Width = 800;
            form.Height = 600;
            //SplashScreen.Init();
            form.Show();
            //Game.Draw();
            Application.Run(form);
        }
    }
}
