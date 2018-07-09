using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    class Bullet : BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\bullet.png");
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
        }

        public Point Position => this.Pos;

        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }
    }
}
