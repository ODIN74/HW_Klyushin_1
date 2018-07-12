using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    class Anoa:Planet
    {
        public Anoa(Point pos, Point dir, PlanetsEnum planet)
            : base(pos, dir, planet)
        {
            if (planet == PlanetsEnum.Anoa)
                this.image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetAnoa.png");
        }
    }
}
