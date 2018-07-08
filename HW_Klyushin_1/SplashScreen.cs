﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace HW_Klyushin_1
{
    using System.Runtime.CompilerServices;

    class SplashScreen : Form
    {
        //Create a Bitmpap Object.
        Bitmap animatedImage = new Bitmap(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\Space2.gif");
        bool currentlyAnimating = false;
        private static  BufferedGraphicsContext context = BufferedGraphicsManager.Current;
        public static BufferedGraphics Buffer;
        Graphics g;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }

        public static int Height { get; set; }

        private PointF[] stretchImage;

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

            stretchImage = new PointF[] {new PointF(0.0f,0.0f), new PointF(0.0f,Height), new PointF(Width, 0.0f) };


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
            Buffer.Dispose();
        }

    }
}