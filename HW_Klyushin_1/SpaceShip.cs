using System;
using System.Drawing;

namespace HW_Klyushin_1
{

    class SpaceShip : BaseObject
    {
        private int energy = 100;

        private int points = 0;

        private readonly Image image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\spaceship.png");

        public SpaceShip(Point pos, Point dir, Size size)
            : base(pos, dir, size)
        {
        }

        public int Energy => this.energy;

        public int Points => this.points;

        public Point Position => this.Pos;

        public void ReductionOfEnergy(int n)
        {
            this.energy -= n;
        }

        public void IncreaseEnergy(int n)
        {
            if(this.energy < 100) this.energy += n;
        }

        public void HitTarget()
        {
            this.points++;
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
            MessageDie?.Invoke();
        }

        public static event Message MessageDie;
    }
}
