
using System.Drawing;

namespace HW_Klyushin_1
{
    class Asteroid : BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\imageAsteroid.png");
        public Asteroid(Point pos, Point dir, Size size):base(pos, dir, size)
        {
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }

        public void Regenerate()
        {
            this.Pos.X = Game.Width - 20;
            this.Dir.X++;
            this.Dir.Y++;
        }
    }
}
