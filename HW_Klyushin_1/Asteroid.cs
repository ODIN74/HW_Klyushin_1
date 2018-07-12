using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    using System;

    class Asteroid : BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\imageAsteroid.png");

        public Asteroid(Point pos, Point dir, Size size):base(pos, dir, size)
        {
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) this.Regenerate();
            if (Pos.Y < 0) this.Regenerate();
            if (Pos.Y > Game.Height) this.Regenerate(); 
        }

        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }

        public void Regenerate()
        {
            Random rnd = new Random();
            this.Pos.X = Game.Width;
            this.Pos.Y = rnd.Next(10, Game.Height - 10);
            this.Dir.X++;
        }
    }
}
