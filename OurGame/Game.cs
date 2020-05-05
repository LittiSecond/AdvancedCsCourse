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
        private static int FIRE_INTERVAL = 10;  //интервал между выстрелами в тактах
        private static int MIN_ASTEROID_DAMAG = 5;
        private static int MAX_ASTEROID_DAMAG = 50;

        private static Color _bgColor = Color.Black;
        
        private List<Asteroid> _asteroids;
        private List<Bullet> _bullets;
        private List<Bullet> _bulletsToDelete;
        private int _fireTimer;      // подсчёт тактов с прошлого выстрела

        private Ship _ship; // = new Ship(GraphicHandler.Graphics, new Point(10, 400), 
                            // new Point(5,5), new Size(12, 12));

        private EnergyGlobe _energyGlobe;

        private int _score;

        //private  bool _dataError = false;

        public event Message GameOver;

        public Game()
        {
            _asteroids = new List<Asteroid>();
            _bullets = new List<Bullet>();
            _bulletsToDelete = new List<Bullet>();
        }

        public void Init()
        {
            Load();
            Ship.MessageDie += Finish;
            Program.KeyPress += Form_KeyDown;
        }

        public override void On()
        {
            base.On();
            GraphicHandler.BackgroundColor = _bgColor;
            Log("Начало игры.");
        }

        public override void Update()
        {
            if (!_enabled) return;
            base.Update();

            // обнаружение коллизий астероидов
            for (int i = _asteroids.Count - 1; i >= 0; i--)
            {
                Asteroid a = _asteroids[i];
                if (a == null) continue;
                bool isCollision = false;

                for (int j = _bullets.Count - 1; j >= 0; j--)
                {
                    Bullet b = _bullets[j];
                    if (b == null) continue;                   

                    if (a.Collision(b))
                    {
                        System.Media.SystemSounds.Hand.Play();

                        DestroyAsteroid(a);
                        DeleteBullet(b);
                        _score += 1;
                        isCollision = true;
                        break;
                    }
                }

                if (isCollision) continue;

                if (_ship == null)
                    continue;

                if (!_ship.Collision(a))
                    continue;
                Random rnd = new Random();
                int damag = rnd.Next(MIN_ASTEROID_DAMAG, MAX_ASTEROID_DAMAG);
                _ship.EnergyLow(damag);
                Log("По кораблю нанесён урон = " + damag.ToString());
                System.Media.SystemSounds.Asterisk.Play();
                if (_ship.Energy <= 0)
                    _ship.Die();

            }

            // столкновение с энергичическим шаром 
            if ( _ship != null && _ship.Collision(_energyGlobe))
            {
                System.Media.SystemSounds.Beep.Play();
                int energ = _energyGlobe.Power;
                _ship.EnergyUp(energ);
                _energyGlobe.Reset();
                Log("Получено энергии = " + energ.ToString());
            }

            //удаление пуль
            foreach (Bullet b in _bulletsToDelete)
            {
                _objectsFullList.Remove(b);
                _bullets.Remove(b);
            }
            _bulletsToDelete.Clear();

            _fireTimer++;
        }

        public override void Draw()
        {
            if (!_enabled) return;
            base.Draw();

            if (_ship != null)
            {
                GraphicHandler.Graphics.DrawString("Энэргы: " + _ship.Energy.ToString(),
                    SystemFonts.DefaultFont, Brushes.White, 0, 0);
            }

            GraphicHandler.Graphics.DrawString("Скорэ: " + _score.ToString(),
                SystemFonts.DefaultFont, Brushes.White, 0, 20);

        }

        private void Finish()
        {
            _enabled = false;
            GraphicHandler.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60,
                FontStyle.Underline), Brushes.White, 200, 100);
            Log("Корабль уничтожен. Игра закончена со счётом = " +_score.ToString());
            GameOver?.Invoke();
        }

        private void Load()
        {
            Graphics g = GraphicHandler.Graphics;
            int height = GraphicHandler.Height;
            try
            {
                //CrateBullet(new Point(0, 200));

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


                _ship = new Ship(g, new Point(10, 400),
                            new Point(10,5), new Size(12, 12));
                _objectsFullList.Add(_ship);


                _energyGlobe = new EnergyGlobe(g, new Point(1000, rnd.Next(0, height - 50)),
                        new Point(-10, 0));
                _objectsFullList.Add(_energyGlobe);

            }
            catch (GameObjectException goe)
            {
                //_dataError = true;
                MessageBox.Show(goe.Message);
                Log(goe.Message);
                throw;
            }
        }

        private void CrateBullet(Point position)
        {
            Bullet b = new Bullet(GraphicHandler.Graphics, position, new Point(10, 0), new Size(4, 1));
            _bullets.Add(b);
            _objectsFullList.Add(b);
            b.OnDelete += DeleteBullet;
        }

        /// <summary>
        /// Сложить пулю в список для удаления. Позже будет удалено.
        /// </summary>
        /// <param name="b"></param>
        private void DeleteBullet(Bullet b)
        {
            // Удалить сразу нельзя из-за того, что этот метод вызывается во время 
            // перечисления foreach по коллекции _objectsFullList
            if (b != null)
            {
                //_objectsFullList.Remove(b);
                //_bullets.Remove(b);
                _bulletsToDelete.Add(b);
                b.OnDelete -= DeleteBullet;
            }
        }

        private void DestroyAsteroid(Asteroid a)
        {
            if (a == null) return;
            _objectsFullList.Remove(a);
            _asteroids.Remove(a);
            Log("Астеройд уничтожен");
        }

        private void Form_KeyDown(Keys keyCode)
        {
            if (keyCode == Keys.ControlKey)
            {
                if (_fireTimer >= FIRE_INTERVAL)
                {
                    CrateBullet(new Point(_ship.Rect.X + 10, _ship.Rect.Y + 4));
                    _fireTimer = 0;
                    Log("Выстрел");
                }
            }
            if (keyCode == Keys.Up) _ship.Up();
            if (keyCode == Keys.Down) _ship.Down();
        }


    }
}
