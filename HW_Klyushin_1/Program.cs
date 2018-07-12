using System;
using System.Windows.Forms;
using System.Drawing;

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
            SplashScreen form = new SplashScreen(new Size(800, 600));
            form.Show();
            Application.Run(form);
        }
    }
}
