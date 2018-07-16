using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс космического корабля
    /// </summary>
    class SpaceShip : BaseObject
    {
        /// <summary>
        /// Энергия корабля
        /// </summary>
        private int energy = 100;

        /// <summary>
        /// Набранные очки
        /// </summary>
        private int points = 0;

        /// <summary>
        /// Изображение космического корабля
        /// </summary>
        private readonly Image image = Image.FromFile(@".\spaceship.png");

        /// <summary>
        /// Свойство возвращающее текущее колличество энергии
        /// </summary>
        public int Energy => this.energy;

        /// <summary>
        /// Свойство возвращающее колличество набранных очков
        /// </summary>
        public int Points => this.points;

        /// <summary>
        /// Свойство возвращающее текущую позицию корабля
        /// </summary>
        public Point Position => this.Pos;

        /// <summary>
        /// Конструктор космического корабля
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="size">Размер</param>
        public SpaceShip(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Метод уменьшения энергии
        /// </summary>
        /// <param name="n">Дикремент энергии</param>
        public void ReductionOfEnergy(int n)
        {
            this.energy -= n;
        }

        /// <summary>
        /// Метод увеличения энергии
        /// </summary>
        /// <param name="n">Инкремент энергии</param>
        public void IncreaseEnergy(int n)
        {
            if(this.energy < 100) this.energy += n;
        }

        /// <summary>
        /// Метод увеличения колличества очков
        /// </summary>
        public void HitTarget()
        {
            this.points++;
        }

        /// <summary>
        /// Переопределение метода отрисовки космического корабля
        /// </summary>
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image, this.Pos);
        }

        /// <summary>
        /// Метод переопределения метода обновления состояния объекта
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// Метод перемещения вверх
        /// </summary>
        public void MoveUp()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Метод перемещения вниз
        /// </summary>
        public void MoveDown()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// Метод перемещения вправо
        /// </summary>
        public void MoveRight()
        {
            if (Pos.X < Game.Height) Pos.X = Pos.X + Dir.X;
        }

        /// <summary>
        /// Метод перемещения влево
        /// </summary>
        public void MoveLeft()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }

        /// <summary>
        /// Метод смерти космического корабля
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }

        /// <summary>
        /// Метод генерации события
        /// </summary>
        public static event Message MessageDie;
    }
}
