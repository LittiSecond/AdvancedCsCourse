using System;
using System.Drawing;

namespace OurGame
{
    abstract class BaseObject : ICollision
    {

        protected Point _pos;
        protected Point _dir;
        protected Size _size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            _pos = pos;
            _dir = dir;
            _size = size;
        }

        public abstract void Draw();

        public virtual void Update()
        {
            //_pos.X = _pos.X + _dir.X;
            //_pos.Y = _pos.Y + _dir.Y;
            //if (_pos.X < 0) _dir.X = Math.Abs(_dir.X);
            //if (_pos.X + _size.Width > Game.Width) _dir.X = -Math.Abs(_dir.X);
            //if (_pos.Y < 0) _dir.Y = Math.Abs(_dir.Y);
            //if (_pos.Y + _size.Height > Game.Heigth) _dir.Y = -Math.Abs(_dir.Y);
            _pos.X = _pos.X + _dir.X;
            if (_pos.X < 0)
            {
                _pos.X = Game.Width + _size.Width;
            }
        }

        public Rectangle Rect => new Rectangle(_pos, _size);

        public bool Collision(ICollision o) => this.Rect.IntersectsWith(o.Rect);

    }
}
