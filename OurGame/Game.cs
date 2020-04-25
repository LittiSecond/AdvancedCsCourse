using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

// :(
//#define BULLETS_QUANTITY 30    

namespace OurGame
{
    /// <summary>
    /// состояние программы - игра (режим игры)
    /// </summary>
    static class Game
    {
        private const int MIN_SIZE = 0;
        private const int MAX_SIZE = 1000;

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;

        // размеры игрового поля
        private static int _width;
        private static int _height; 

        static public int Width          // выброс исключений по заданию 4 
        {
            get { return _width; }
            set
            {
                if (value <= MIN_SIZE || value > MAX_SIZE)
                    throw new ArgumentOutOfRangeException("Width", "Недопустимая ширина области рисования.");
                _width = value;
            }
        }

        static public int Height
        {
            get { return _height; }
            set
            {
                if (value <= MIN_SIZE || value > MAX_SIZE)
                    throw new ArgumentOutOfRangeException("Height", "Недопустимая высота области рисования.");
                _height = value;
            }
        }
        
        private static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;

        private static Timer _timer;

        private static bool _enabled = false;
        private static bool _dataError = false;

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
            Height = form.ClientSize.Height;
            _buffer = _context.Allocate(g, new Rectangle(0, 0, _width, _height));
            _dataError = false;
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
                if (a == null) continue;
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();

                    _bullet.Hitting();  // по заданию 3
                    a.Damaged();        // по заданию 3
                }
            }
            _bullet.Update();
        }

        private static void Load()
        {
            // в задании нет задачи использовать List<> вместо массива
            _objs = new BaseObject[30];
            _asteroids = new Asteroid[20];

            try
            {
                _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
                Random rnd = new Random();

                for (int i = 0; i < _objs.Length; i++)
                {
                    int r = rnd.Next(5, 50);
                    _objs[i] = new Star(new Point(1000, rnd.Next(0, _height)),
                        new Point(-r, r), new Size(3, 3));
                }
                for (int i = 0; i < _asteroids.Length - 1; i++)
                {
                    int r = rnd.Next(5, 50);
                    _asteroids[i] = new Asteroid(new Point(1000, rnd.Next(0, _height - r)),
                        new Point(-i, 0), new Size(r, r));
                }
                // чтобы гарантированно один астеройд был на линии пули - для проверки работы по заданию 3
                _asteroids[_asteroids.Length - 1] = new Asteroid(new Point(1000, 198),
                        new Point(-10, 0), new Size(20, 20));
            }
            catch (GameObjectException goe)    // перехват исключения по заданию 5
            {
                _dataError = true;
                MessageBox.Show(goe.Message);
            }

        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (_dataError) return;
            Draw();
            Update();
        }

    }
}
