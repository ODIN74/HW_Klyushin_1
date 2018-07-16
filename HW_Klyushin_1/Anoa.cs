using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс планеты Аноа
    /// </summary>
    class Anoa:Planet
    {
        /// <summary>
        /// Конструктор планеты Аноа
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="planet">Enum идентификатор планеты</param>
        public Anoa(Point pos, Point dir, PlanetsEnum planet) : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Anoa) this.image = Image.FromFile(@".\planetAnoa.png");
        }
    }
}
