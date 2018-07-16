
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс планет
    /// </summary>
    class Planet : BaseObject
    {
        /// <summary>
        /// Изображение планеты
        /// </summary>
        protected Image image;

        /// <summary>
        /// Конструктор планеты
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="planet">Enum идентификатор планеты</param>
        public Planet(Point pos, Point dir, PlanetsEnum planet) : base(pos,dir)
        {
        }

        /// <summary>
        /// Нумерованный список планет
        /// </summary>
        public enum PlanetsEnum
        {
            Earth,
            Venus,
            Saturn,
            Anoa
        }

        /// <summary>
        /// Переопределение метода отрисовки планеты
        /// </summary>
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image, Pos);
        }

        /// <summary>
        /// Переопределение метода обновления состояния объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X > Game.Width) Pos.X -= Game.Width + Size.Width;
            if (Pos.Y > Game.Height) Pos.Y -= Game.Width + Size.Width;
        }
    }
}
