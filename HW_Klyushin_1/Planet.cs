
using System.Drawing;

namespace HW_Klyushin_1
{
    class Planet : BaseObject
    {
        public enum PlanetsEnum  
        {
            Earth,
            Venus,
            Saturn,
            Anoa
        }

        protected Image image;

        public Planet(Point pos, Point dir, PlanetsEnum planet):base(pos,dir)
        {
        }

        public override void Draw()
        {
            Game.GameBuffer.Graphics.DrawImage(image, Pos);
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
