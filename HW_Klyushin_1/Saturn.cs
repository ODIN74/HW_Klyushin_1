using System.Drawing;

namespace HW_Klyushin_1
{
    /// <summary>
    /// Класс планеты Сатурн
    /// </summary>
    internal class Saturn:Planet
    {
        /// <summary>
        /// Конструктор планеты Саткрн
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Смещение</param>
        /// <param name="planet">Enum идентификатор планеты</param>
        public Saturn(Point pos, Point dir, PlanetsEnum planet) : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Saturn) this.image = Image.FromFile(@".\planetSaturn.png");
        }
    }
}
