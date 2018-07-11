using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Klyushin_1
{
    public partial class SplashScreen : System.Windows.Forms.Form
    {
        public SplashScreen(Size size)
        {
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
            InitializeComponent();
            this.ClientSize = size;
        }

        //Create a Bitmpap Object.
        Bitmap animatedImage = new Bitmap(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\Space1.gif");
        bool currentlyAnimating = false;
        private static BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        public static BufferedGraphics Buffer;
        Graphics g;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }

        public static int Height { get; set; }

        private PointF[] stretchImage;
        private PointF showText;

        //This method begins the animation.
        public void AnimateImage()
        {
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            g = this.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = this.ClientSize.Width;
            Height = this.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            DoubleBuffered = true;

            stretchImage = new PointF[] { new PointF(0.0f, 0.0f), new PointF(0.0f, Height), new PointF(Width, 0.0f) };

            showText = new PointF(Width - 100, Height - 30);

            if (!currentlyAnimating)
            {
                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {

            //Force a call to the Paint event handler.
            this.Invalidate();

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Begin the animation.
            AnimateImage();


            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            e.Graphics.DrawImage(this.animatedImage, this.stretchImage);
            e.Graphics.DrawString("Клюшин Антон", new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular), new SolidBrush(Color.White), Width - 100, Height - 30);
            Buffer.Dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            gameForm mainForm = new gameForm();
            mainForm.ClientSize = new Size(this.ClientSize.Width,this.ClientSize.Height);
            Game.Init(mainForm);
            mainForm.Show();
            Game.Draw();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
 
        }
    }
}
