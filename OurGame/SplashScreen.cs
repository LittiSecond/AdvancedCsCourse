/*
 * В классах SplashScreen и Game - много одинакового кода.
 * Сначала была сделана попытка создать базовый класс, от которого потом наследовать 
 * SplashScreen и Game, но из-за того что в Game всё static, применить наслоедование
 * не удалось. 
 * Времени на выполнение задание осталось мало, из-за недостатка времени
 * класс SplashScreen копированием кода. Позже это будет переделано - исключено
 * копирование кода (может быть). 
 * 
 */

using System;
using System.Windows.Forms;
using System.Drawing;

namespace OurGame
{
    /// <summary>
    /// состояние программы - заставка оно же главное меню 
    /// </summary>
    static class SplashScreen
    {
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static MainGameForm _form;
        private static Color _formColor;
        // размеры игрового поля
        public static int Width { get; set; }
        static public int Heigth { get; set; }
        // как правильно, сначала public или сначала static писать?

        private static BaseObject[] _objs;
        static private  Timer _timer = null;

        private static bool _enabled = false;

        public static BufferedGraphics Buffer
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
            _form = form as MainGameForm;
            _formColor = _form.GetColor();
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
            _form?.SetSplashScreenUIVisibility(true);
        }

        public static void Off()
        {
            if (_enabled)
            {
                _timer?.Stop();
                _enabled = false;
            }
            _form?.SetSplashScreenUIVisibility(false);
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(_formColor);

            foreach (BaseObject obj in _objs)
            {
                obj?.Draw();
            }

            _buffer.Render();

        }

        private static void Update()
        {
            foreach (BaseObject obj in _objs)
            {
                obj?.Update();
            }
        }

        private static void Load()
        {
            _objs = new BaseObject[2];  
            _objs[0] = new Brain(new Point(200, 450), new Point(-10, 14), new Size(38, 42));
            _objs[1] = new Brain(new Point(400, 300), new Point(8, 16), new Size(38, 42));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

    }
}
