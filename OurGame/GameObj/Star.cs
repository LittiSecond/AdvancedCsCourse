using System;
using System.Drawing;

namespace OurGame
{
    class Star : BaseObject
    {

        public Star(Graphics g, Point pos, Point dir, Size size) 
            : base(g, pos,dir,size)
        {

        }

        public override void Draw()
        {
            if (_graphics == null) return;
            _graphics.DrawLine(Pens.White, _pos.X, _pos.Y,
                _pos.X + _size.Width, _pos.Y + _size.Height);
            _graphics.DrawLine(Pens.White, _pos.X + _size.Width, _pos.Y,
                _pos.X , _pos.Y + _size.Height);
        }

        public override void Update()
        {
            _pos.X = _pos.X + _dir.X;
            if (_pos.X < 0) _pos.X = GraphicHandler.Width; // + _size.Width;
        }
    }
}
