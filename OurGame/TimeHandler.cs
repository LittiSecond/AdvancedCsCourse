using System;
using System.Windows.Forms;  // для таймера


namespace OurGame
{
    /// <summary>
    /// отвечает за время и тактировку программы
    /// </summary>
    static class TimeHandler
    {
        // интервал между тактами программы, в перспективе переменный, 
        // но пока постоянный, мкс
        private static int _interval = 100;

        private static Timer _timer;

        private static bool _enabled = false;

        private static BaseBehaviour _behaviour;

        //public static BaseBehaviour Behaviour
        //{
        //    set { _behaviour = value; }
        //}

        /// <summary>
        /// интервал с прошлого такта, в секундах
        /// </summary>
        //public static float DeltaTime
        //{
        //    get { return _interval / 1000.0f;  }
        //}

        //public static void Init()
        //{
        //    if (_timer == null)
        //    {
        //        _timer = _timer = new Timer();
        //        _timer.Interval = _interval;
        //    }
        //}

        public static void On(BaseBehaviour b)
        {
            _behaviour = b;
            On();
        }

        public static void On()
        {
            if (_enabled) return;
            if (_timer == null)
            {
                _timer = new Timer { Interval = _interval };
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

        private static void Timer_Tick(object sender, EventArgs e)
        {
            GraphicHandler.Clear();
            if (_behaviour != null)
            {
                _behaviour.Update();
                _behaviour.Draw();
            }

            GraphicHandler.Render();
        }

    }
}
