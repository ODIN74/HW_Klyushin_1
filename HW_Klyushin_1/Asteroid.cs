
using System.Drawing;

namespace HW_Klyushin_1
{
    class Asteroid:BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\imageAsteroid.png");
        public Asteroid(Point pos, Point dir, Size size):base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
    }
}
