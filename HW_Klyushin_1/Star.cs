using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс звезд
    /// </summary>
    class Star : BaseObject
    {
        /// <summary>
        /// Изображение звезды
        /// </summary>
        protected readonly Image image = Image.FromFile(@".\imageStar1.png");

        /// <summary>
        /// Конструктор звезды
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Переопределение метода отрисовки объекта
        /// </summary>
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image,Pos);
        }

        /// <summary>
        /// Переопределение метода обновления состояния объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X += Game.Width - Size.Width;
        }

    }
}
