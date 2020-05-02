using System;
using System.Drawing;

namespace OurGame
{
    //public delegate void Message();

    abstract class BaseObject : ICollision
    {
        protected Graphics _graphics;


        private const int MAX_SPEED = 100;

        protected Point _pos;
        protected Point _dir;
        protected Size _size;

        public BaseObject(Graphics g, Point pos, Point dir, Size size)
        {
            _graphics = g;
            _pos = pos;

            int speed = Math.Max(Math.Abs(dir.X), Math.Abs(dir.Y));

            if (speed > MAX_SPEED)
                throw new GameObjectException("Слишком большая скорость объекта " + this.ToString());
            _dir = dir;
            if (size.Width < 0 || size.Height < 0)
                throw new GameObjectException("Недопустимый размер объекта " + this.ToString());
            _size = size;
        }

        public abstract void Draw();

        public abstract void Update();

        public Rectangle Rect => new Rectangle(_pos, _size);

        public bool Collision(ICollision o) => this.Rect.IntersectsWith(o.Rect);

    }
}
