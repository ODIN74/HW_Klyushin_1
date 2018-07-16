using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс планеты Земля
    /// </summary>
    class Earth:Planet
    {
        /// <summary>
        /// Конструктор планеты Земля
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="planet">Enum идентификатор планеты</param>
        public Earth(Point pos, Point dir, PlanetsEnum planet) : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Earth) this.image = Image.FromFile(@".\planetEarth.png");
        }
    }
}
