using System;
using System.Drawing;


namespace OurGame
{
    /// <summary>
    /// сгусток энергии (энергетическая сфера )
    /// </summary>
    class EnergyGlobe : BaseObject
    {
        // в место аптечек - энергетический шар.
        // аптечка, восстанавливающая энергию кораблю - бред

        const int WIDTH = 20;
        const int HEIGHT = 20;
        const int DEFOULT_ENERGI = 15;  

        public int Power { get; private set; }

        private Brush[] _brushes;
        private int _brushIndex;


        public EnergyGlobe(Graphics g, Point pos, Point dir, int power = DEFOULT_ENERGI) : base(g, pos, dir, new Size(WIDTH, HEIGHT))
        {
            Power = power;
            _brushes = new Brush[]
            {
                Brushes.White,
                Brushes.Wheat,
                Brushes.Yellow
            };            
        }


        public override void Draw()
        {
            _graphics?.FillEllipse(NextBrush(), _pos.X, _pos.Y, _size.Width, _size.Height);
        }

        public override void Update()
        {
            _pos.X = _pos.X + _dir.X;
            if (_pos.X < 0) Reset();
        }

        public void Reset()
        {
            _pos.X = GraphicHandler.Width;
        }

        private Brush NextBrush()
        {
            if (_brushIndex >= _brushes.Length)
                _brushIndex = 0;
            return _brushes[_brushIndex++];
        }
    }
}
