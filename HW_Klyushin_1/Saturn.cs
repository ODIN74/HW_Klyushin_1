
using System.Drawing;

namespace HW_Klyushin_1
{
    class Saturn:Planet
    {
        public Saturn(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Saturn)
                this.image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\planetSaturn.png");
        }
    }
}
