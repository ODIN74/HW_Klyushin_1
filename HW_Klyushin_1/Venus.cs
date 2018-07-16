using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс планеты Венера
    /// </summary>
    class Venus:Planet
    {
        /// <summary>
        /// Конструктор Венеры
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="planet">Enum идентификатор планеты</param>
        public Venus(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Venus)
                this.image = Image.FromFile(@".\planetVenus.png");
        }
    }
}
