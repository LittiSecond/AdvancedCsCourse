using System;
using System.Drawing;
using System.Windows.Forms;

namespace OurGame
{
    // задумался, как лучше этот класс назвать:
    // GraphicHandler, GraphicController, GraphicManager

    /// <summary>
    /// Отвечает за графику
    /// </summary>
    static class GraphicHandler
    {
        private const int MIN_SIZE = 0;
        private const int MAX_SIZE = 1000;

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;

        // размеры игрового поля
        private static int _width;
        private static int _height;

        static public int Width
        {
            get { return _width; }
            set
            {
                if (value <= MIN_SIZE || value > MAX_SIZE)
                    throw new ArgumentOutOfRangeException("Width", "GraphicHandler: Недопустимая ширина области рисования.");
                _width = value;
            }
        }

        static public int Height
        {
            get { return _height; }
            set
            {
                if (value <= MIN_SIZE || value > MAX_SIZE)
                    throw new ArgumentOutOfRangeException("Height", "GraphicHandler: Недопустимая высота области рисования.");
                _height = value;
            }
        }

        //public static BufferedGraphics Buffer
        //{
        //    get { return _buffer; }
        //}

        public static Graphics Graphics => _buffer?.Graphics;
        
        public static Color BackgroundColor { get; set; }

        public static void Init(Form form)
        {
            if (form == null) return;
            //throw new ArgumentException("GraphicHandler->Init", "form");
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            _buffer = _context.Allocate(g, new Rectangle(0, 0, _width, _height));
        }

        public static void Clear()
        {
            _buffer?.Graphics.Clear(BackgroundColor);
        }

        public static void Render()
        {
            _buffer?.Render();
        }



    }
}
