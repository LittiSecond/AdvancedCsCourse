using System;
using System.Drawing;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurGame
{
    class Ship : BaseObject
    {

        private int _energy = 100;
        public int Energy => _energy;

        public static event Message MessageDie;

        public Ship (Graphics g, Point pos, Point dir, Size size) :
            base(g, pos, dir, size)
        {

        }

        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        public override void Draw()
        {
            _graphics?.FillEllipse(Brushes.Wheat, _pos.X, _pos.Y, _size.Width, _size.Height);
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        public void Up()
        {
            if (_pos.Y > 0) _pos.Y = _pos.Y - _dir.Y; 
        }

        public void Down()
        {
            if (_pos.Y < GraphicHandler.Height) _pos.Y = _pos.Y + _dir.Y;
        }

        public void Die()
        {
            MessageDie?.Invoke();
        }

    }
}
