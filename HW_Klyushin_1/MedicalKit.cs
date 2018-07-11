﻿using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    using System.Runtime.CompilerServices;

    internal class MedicalKit : BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Основы программирования\C Sharp\Level_2\HW_Klyushin_1\HW_Klyushin_1\medicalKit.png");

        public MedicalKit(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public void Regenerate()
        {
            Random rnd = new Random();
            this.Pos.X = Game.Width;
            this.Pos.Y = rnd.Next(10, Game.Height - 10);
            this.Dir.X = 10;
        }

        public void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(this.image, this.Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (this.Pos.X < -10) this.Dir.X = 0;
        }
    }
}
