using System;
using System.Drawing;

namespace OurGame
{

    /// <summary>
    /// поведение программы в главном меню
    /// </summary>
    class MainMenu : BaseBehaviour
    {
        private static MainGameForm _form;
        private static Color _formColor;

        //public MainMenu()
        //{
            
        //}

        public void Init(MainGameForm f)
        {
            if (f != null)
            {
                _form = f;
                _formColor = f.GetColor();
            }
            Load();
        }
               
        private void Load()
        {
            Graphics g = GraphicHandler.Graphics;

            _objectsFullList.Add( new Brain(g, new Point(200, 450), new Point(-10, 14), new Size(38, 42)));
            _objectsFullList.Add( new Brain(g, new Point(400, 300), new Point(8, 16), new Size(38, 42)));
        }

        public override void On()
        {
            base.On();
            GraphicHandler.BackgroundColor = _formColor;
            _form?.SetSplashScreenUIVisibility(true);
            Log("Главное меню включено.");
        }

        public override void Off()
        {
            base.Off();
            _form?.SetSplashScreenUIVisibility(false);
            Log("Выход из главного меню.");
        }
    }
}
