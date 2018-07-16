using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Абстрактный класс игровых объектов
    /// </summary>
    abstract class BaseObject : ICollision
    {
        /// <summary>
        /// Позиция объекта
        /// </summary>
        protected Point Pos;

        /// <summary>
        /// Смещение объекта
        /// </summary>
        protected Point Dir;

        /// <summary>
        /// Размер объекта
        /// </summary>
        protected Size Size;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
            Size = Size.Empty;
        }

        /// <summary>
        /// Перегруженный конструктор класса
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public BaseObject(Point pos, Point dir, Size size) : this(pos, dir)
        {
            Size = size;
        }


        /// <summary>
        /// Делегат для обработки столкновений
        /// </summary>
        public delegate void Message();

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public virtual void Draw()
        {
            Game.GameBuffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обработки столкновений
        /// </summary>
        /// <param name="o">Интерфейс пересечений объектов</param>
        /// <returns></returns>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        /// <summary>
        /// Метод создания области для отработки столкновений с объектами
        /// </summary>
        public Rectangle Rect => new Rectangle(this.Pos,this.Size);

        /// <summary>
        /// Абстрактный метод обновления состояния объекта
        /// </summary>
        public abstract void Update();

    }
}
