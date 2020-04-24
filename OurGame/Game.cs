using System;
using System.Windows.Forms;
using System.Drawing;

namespace OurGame
{
    /// <summary>
    /// состояние программы - игра (режим игры)
    /// </summary>
    static class Game
    {
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;

        // размеры игрового поля
        public static int Width { get; set; }
        public static int Heigth { get; set; }

        private static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        private static Timer _timer;

        private static bool _enabled = false;

        public static  BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Heigth = form.ClientSize.Height;
            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Heigth));
            Load();
        }

        public static void On()
        {
            if (_enabled) return;
            if (_timer == null)
            {
                //_timer = new Timer { Interval = 100 };

                _timer = new Timer();
                _timer.Interval = 100;
                _timer.Tick += Timer_Tick;
            }
            _timer.Start();
            _enabled = true;
        }

        public static void Off()
        {
            if (_enabled)
            {
                _timer?.Stop();
                _enabled = false;
            }
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            //_buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //_buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 50, 200, 125));

            foreach (BaseObject obj in _objs)
            {
                obj?.Draw();
            }
            foreach (Asteroid a in _asteroids)
            {
                a?.Draw();
            }
            _bullet.Draw();

            _buffer.Render();

        }

        private static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj?.Update();
            }
            foreach (Asteroid a in _asteroids)
            {
                a?.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                }
            }
            _bullet.Update();
        }

        private static void Load()
        {
            _objs = new BaseObject[30];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[20];
            Random rnd = new Random();

            for (int i = 0; i < _objs.Length ; i++)
            {
                int r = rnd.Next(5, 50);
                _objs[i] = new Star(new Point(1000, rnd.Next(0, Game.Heigth)), 
                    new Point(-r, r),  new Size(3, 3));
            }
            for (int i = 0; i < _asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, Game.Heigth - r)), 
                    new Point(-i, 0), new Size(r, r));
            }

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

    }
}
