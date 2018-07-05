
using System.Drawing;

namespace HW_Klyushin_1
{
    class Venus:Planet
    {
        public Venus(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Venus)
                this.image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\planetVenus.png");
        }
    }
}
