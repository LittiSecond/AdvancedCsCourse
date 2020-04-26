using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace OurGame
{
    /// <summary>
    /// поведение программы во время игры (режим игры)
    /// </summary>
    class Game : BaseBehaviour
    {
        private static int STARS_QUANTITY = 30;
        private static int ASTEROIDS_QUANTITY = 20;

        private static Color _bgColor = Color.Black;

        private Bullet _bullet;
        private List<Asteroid> _asteroids;
               
        //private  bool _dataError = false;

        public Game()
        {
            _asteroids = new List<Asteroid>();
        }

        public void Init()
        {
            Load();
        }

        public override void On()
        {
            base.On();
            GraphicHandler.BackgroundColor = _bgColor;
        }

        public override void Update()
        {
            if (!_enabled) return;
            base.Update();

            // обнаружение коллизий
            foreach (Asteroid a in _asteroids)
            {
                if (a == null) continue;
                
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();

                    _bullet.Hitting();  
                    a.Damaged();
                }
            }

        }

        private void Load()
        {
            Graphics g = GraphicHandler.Graphics;
            int height = GraphicHandler.Height;
            try
            {
                _bullet = new Bullet(g, new Point(0, 200), new Point(5, 0), new Size(4, 1));
                _objectsFullList.Add(_bullet);

                Random rnd = new Random();

                for (int i = 0; i < STARS_QUANTITY; i++)
                {
                    int r = rnd.Next(5, 50);
                    _objectsFullList.Add(new Star(g, new Point(1000, rnd.Next(0, height)),
                        new Point(-r, r), new Size(3, 3)));
                }

                for (int i = 0; i < ASTEROIDS_QUANTITY - 1; i++)
                {
                    int r = rnd.Next(5, 50);
                    Asteroid a = new Asteroid(g, new Point(1000, rnd.Next(0, height - r)),
                        new Point(-i, 0), new Size(r, r));
                    _asteroids.Add(a);
                    _objectsFullList.Add(a);
                }

                // чтобы гарантированно один астеройд был на линии пули
                Asteroid a2 = new Asteroid(g, new Point(1000, 198),
                        new Point(-10, 0), new Size(20, 20));
                _asteroids.Add(a2);
                _objectsFullList.Add(a2);
            }
            catch (GameObjectException goe)
            {
                //_dataError = true;
                MessageBox.Show(goe.Message);
            }

        }

    }
}
