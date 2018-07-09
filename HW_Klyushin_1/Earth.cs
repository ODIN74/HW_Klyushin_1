
using System.Drawing;

namespace HW_Klyushin_1
{
    class Earth:Planet
    {
        public Earth(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Earth)
            this.image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetEarth.png");
        }
    }
}
