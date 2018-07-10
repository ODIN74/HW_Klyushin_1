
using System.Drawing;

namespace HW_Klyushin_1
{
    class Star : BaseObject
    {
        protected readonly Image image = Image.FromFile(@"D:\Anton\C Sharp\Level 2\lesson_1\HW_Klyushin_1\HW_Klyushin_1\HW_Klyushin_1\imageStar1.png");
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image,Pos);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > Game.Width) Pos.X -= Game.Width + Size.Width;
        }

    }
}
