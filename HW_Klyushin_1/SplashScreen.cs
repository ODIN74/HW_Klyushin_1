using System;
using System.Drawing;
using System.Windows.Forms;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Основыное меню
    /// </summary>
    public partial class SplashScreen : System.Windows.Forms.Form
    { 
        /// <summary>
        /// Анимируемая картинка
        /// </summary>
        private Bitmap animatedImage = new Bitmap(@".\Space1.gif");

        /// <summary>
        /// Флаг запуска анимации
        /// </summary>
        private bool currentlyAnimating = false;

        /// <summary>
        /// Графический контекст буфера
        /// </summary>
        private static BufferedGraphicsContext context = BufferedGraphicsManager.Current;

        /// <summary>
        /// Экземпляр буфера
        /// </summary>
        private static BufferedGraphics Buffer;

        /// <summary>
        /// Экземпляр поверхности для отрисовки
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Вспомогательная переменная для растягивания анимируемого объекта до размера формы
        /// </summary>
        private PointF[] stretchImage;

        /// <summary>
        /// Вспомогательная переменная для отрисовки автора
        /// </summary>
        private PointF showText;

        /// <summary>
        /// Ширина ирового поля
        /// </summary>
        public static int Width { get; set; }

        /// <summary>
        /// Высота ирового поля
        /// </summary>
        public static int Height { get; set; }
       
        /// <summary>
        /// Инициализатор формы основго меню
        /// </summary>
        /// <param name="size">Размеры формы</param>
        public SplashScreen(Size size)
        {
            //проверка размеров окна
            try
            {
                if (size.Width > 1000 || size.Width < 0 || size.Height > 1000 || size.Height < 0) throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentOutOfRangeException)
            {
                if (size.Width > 1000)
                    MessageBox.Show(
                        "Слишком большая ширина окна",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                if (size.Height > 1000)
                    MessageBox.Show(
                        "Слишком большая высота окна",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                if (size.Width < 0 || size.Height < 0)
                    MessageBox.Show(
                        "Отрицательный размер окна",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                Environment.Exit(1);
            }
            this.InitializeComponent();
            this.ClientSize = size;
        }

        /// <summary>
        /// Событие покадрово отрисовки анимации на форме
        /// </summary>
        /// <param name="e">Уведомление</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (SplashScreen.ActiveForm == this)
            {
                //Begin the animation.
                this.AnimateImage();

                //Get the next frame ready for rendering.
                ImageAnimator.UpdateFrames();

                //Draw the next frame in the animation.
                e.Graphics.DrawImage(this.animatedImage, this.stretchImage);
                e.Graphics.DrawString("Клюшин Антон", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), Width - 100, Height - 30);
                Buffer.Dispose();
            }
        }

        /// <summary>
        /// Метод анимации gif изображения
        /// </summary>
        private void AnimateImage()
        {
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            g = this.CreateGraphics();
            
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = this.ClientSize.Width;
            Height = this.ClientSize.Height;
            
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            
            //Активация двойной буфризации
            this.DoubleBuffered = true;

            this.stretchImage = new PointF[] { new PointF(0.0f, 0.0f), new PointF(0.0f, Height), new PointF(Width, 0.0f) };

            this.showText = new PointF(Width - 100, Height - 30);

            if (!this.currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(this.animatedImage, new EventHandler(this.OnFrameChanged));
                this.currentlyAnimating = true;
            }
        }

        /// <summary>
        /// Обработка смены кадра
        /// </summary>
        /// <param name="o">Объект</param>
        /// <param name="e">Уведомление</param>
        private void OnFrameChanged(object o, EventArgs e)
        {
            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        /// <summary>
        /// Обработка события кнопки "Выход"
        /// </summary>
        /// <param name="sender">Источик</param>
        /// <param name="e">Уведомление</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        /// <summary>
        /// Обработка события при нажатии кнопки "Новая игра"
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Уведомление</param>
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            gameForm mainForm = new gameForm();
            mainForm.ClientSize = new Size(this.ClientSize.Width,this.ClientSize.Height);
            Game.Init(mainForm);
            mainForm.Show(this);
            this.currentlyAnimating = true;
            Game.Draw();
        }

        /// <summary>
        /// Обработка события при восстановлении активности формы
        /// </summary>
        /// <param name="sender">Источник</param>
        /// <param name="e">Уведомление</param>
        private void SplashScreen_Activated(object sender, EventArgs e)
        {
            this.currentlyAnimating = false;
        }
    }
}
