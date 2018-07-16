using System.Windows.Forms;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Основная игровая форма
    /// </summary>
    public partial class gameForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// КОнструктор игровой формы
        /// </summary>
        public gameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка события закрытия формы
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Уведомление</param>
        private void gameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Game.Stop();
        }
    }
}
