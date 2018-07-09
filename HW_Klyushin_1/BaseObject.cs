using System;
using System.Drawing;

namespace HW_Klyushin_1
{
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public BaseObject(Point pos, Point dir)
        {
            Pos = pos;
            Dir = dir;
            Size = Size.Empty;
        }

        public BaseObject(Point pos, Point dir, Size size) : this(pos, dir)
        {
            Size = size;
        }

        public virtual void Draw()
        {
            Game.GameBuffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        public Rectangle Rect => new Rectangle(this.Pos,this.Size);

        public abstract void Update();

    }
}
