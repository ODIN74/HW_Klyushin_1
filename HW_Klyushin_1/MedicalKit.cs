using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс аптечки
    /// </summary>
    internal class MedicalKit : BaseObject
    {
        /// <summary>
        /// Изображение аптечки
        /// </summary>
        protected readonly Image image = Image.FromFile(@".\medicalKit1.png");

        /// <summary>
        /// Свойство возвращающее текущую позицию аптечки
        /// </summary>
        public Point Position => this.Pos;

        /// <summary>
        /// Конструктор аптечки
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public MedicalKit(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        /// <summary>
        /// Метод регенерации аптечки
        /// </summary>
        public void Regenerate()
        {
            Random rnd = new Random();
            this.Pos.X = Game.Width;
            this.Pos.Y = rnd.Next(10, Game.Height - 10);
            this.Dir.X = 10;
        }

        /// <summary>
        /// Переопределение метода отрисовки аптечки
        /// </summary>
        public override void Draw()
        {
             Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }

        /// <summary>
        /// Переопределение метода  обновления состояния аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (this.Pos.X < -10) this.Dir.X = 0;
        }
    }
}
