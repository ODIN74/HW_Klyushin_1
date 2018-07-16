using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс астероидов
    /// </summary>
    class Asteroid : BaseObject
    {
        /// <summary>
        /// Изображение для астероида
        /// </summary>
        protected readonly Image image = Image.FromFile(@".\imageAsteroid.png");

        /// <summary>
        /// Конструктор астероида
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Переопределение метода обновления состояния астероида
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) Pos.X += Game.Width - Size.Width;
        }

        /// <summary>
        /// Переопределение метода отрисовки астероида
        /// </summary>
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }
    }
}
