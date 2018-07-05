using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    class Planet:BaseObject
    {
        public enum PlanetEnum { Earth, Venus, Saturn, Anoa}
        protected Image image;

        public Planet(Point pos, Point dir, Size size, PlanetEnum planet):base(pos, dir, size)
        {
            switch (planet)
            {
                case PlanetEnum.Earth:
                    image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetEarth.png");
                    break;
                case PlanetEnum.Venus:
                    image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetVenus.png");
                    break;
                case PlanetEnum.Saturn:
                    image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetSaturn.png");
                    break;
                case PlanetEnum.Anoa:
                    image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\planetAnoa.png");
                    break;
            }
        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X > Game.Width) Pos.X -= Game.Width + Size.Width;
            if (Pos.Y > Game.Height) Pos.Y -= Game.Width + Size.Width;
        }
    }
}
