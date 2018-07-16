using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Интерфейс обработки столкновений
    /// </summary>
    internal interface ICollision
    {
        /// <summary>
        /// Свойство возвращающее область для определения пересечения
        /// </summary>
        Rectangle Rect { get; }

        /// <summary>
        /// Метод обработки столкновения
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <returns>True or False</returns>
        bool Collision(ICollision obj);
    }
}
