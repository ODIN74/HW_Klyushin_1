
using System.Drawing;

namespace HW_Klyushin_1
{
    class Saturn:Planet
    {
        public Saturn(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Saturn)
                this.image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetSaturn.png");
        }
    }
}
