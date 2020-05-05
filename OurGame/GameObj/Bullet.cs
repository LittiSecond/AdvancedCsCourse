using System;
using System.Drawing;


namespace OurGame
{
    class Bullet : BaseObject
    {

        public event Action<Bullet> OnDelete;

        public Bullet(Graphics g, Point pos, Point dir, Size size) : base(g, pos, dir, size)
        {
        }

        public override void Draw()
        {
            _graphics?.DrawRectangle(Pens.OrangeRed, _pos.X, _pos.Y, _size.Width, _size.Height);
        }

        public override void Update()
        {
            _pos.X += _dir.X;
            if (_pos.X > GraphicHandler.Width)
            {
                OnDelete?.Invoke(this);
            }
        }

        /// <summary> Пуля попала в цель </summary>
        public void Hitting()
        {
            _pos.X = 0;   
        }

    }
}
