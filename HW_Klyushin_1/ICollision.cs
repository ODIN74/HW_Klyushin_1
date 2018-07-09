using System.Drawing;

namespace HW_Klyushin_1
{
    internal interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
