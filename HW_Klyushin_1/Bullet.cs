using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс снаряда
    /// </summary>
    class Bullet : BaseObject
    {
        /// <summary>
        /// Изображение снаряда
        /// </summary>
        protected readonly Image image = Image.FromFile(@".\bullet.png");

        /// <summary>
        /// Свойство возвращающее текущую позицию снаряда
        /// </summary>
        public Point Position => this.Pos;

        /// <summary>
        /// Конструктор снаряда
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Переопределение метода обновления состояния снаряда
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// Переопределение метода отрисовки снаряда
        /// </summary>
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }
    }
}
