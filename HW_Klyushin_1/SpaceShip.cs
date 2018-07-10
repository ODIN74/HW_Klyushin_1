using System;
using System.Drawing;

namespace HW_Klyushin_1
{

    class SpaceShip : BaseObject
    {
        private int energy = 100;

        private readonly Image image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\spaceship.png");

        public SpaceShip(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public int Energy => this.energy;

        public void ReductionOfEnergy(int n)
        {
            this.energy -= n;
        }

        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image, this.Pos);
        }

        public override void Update()
        {
        }

        public void MoveUp()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void MoveDown()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void MoveRight()
        {
            if (Pos.X < Game.Height) Pos.X = Pos.X + Dir.X;
        }

        public void MoveLeft()
        {
            if (Pos.X > 0) Pos.X = Pos.X - Dir.X;
        }

        public void Die()
        {

        }
    }
}
