using System;
using System.Drawing;

namespace OurGame
{
    class Brain : BaseObject
    {
        private Image _image;
        private int _speedX;  // величина скорости по X  и Y 
        private int _speedY;

        private RectangleF _dorawRect; 

        public Brain(Point pos, Point dir, Size size)
            : base(pos, dir, size)
        {
            _image = OurGame.Properties.Resources.gb1;
            _speedX = Math.Abs(dir.X);
            _speedY = Math.Abs(dir.Y);
            _dorawRect = SplashScreen._buffer.Graphics.VisibleClipBounds; // получаю размеры области рисования
        }

        public override void Draw()
        {
            SplashScreen._buffer.Graphics.DrawImage(_image, new Rectangle(_pos, _size));

        }


        public override void Update()
        {            
            _pos.X = _pos.X + _dir.X;
            _pos.Y = _pos.Y + _dir.Y;

            if (_pos.X < -_dir.X)
            {
                _dir.X = 0;
                _dir.Y = -_speedY;
            }
            if (_pos.Y < -_dir.Y)
            {
                _dir.X = _speedX;
                _dir.Y = 0;
            }
            if (_pos.X + _dir.X > (int)_dorawRect.Width - _size.Width)
            {
                _dir.X = 0;
                _dir.Y = _speedY;
            }
            if (_pos.Y + _dir.Y > (int)_dorawRect.Height - _size.Height)
            {
                _dir.X = -_speedX;
                _dir.Y = 0;
            }
            
        }



    }
}
