using System;
using System.Windows.Forms;

namespace OurGame
{
    static class Program
    {
        private static Game _game;
        private static MainMenu _menu;

        public static event Action<Keys> KeyPress;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);



            MainGameForm form = new MainGameForm
            {
                // Width = Screen.PrimaryScreen.Bounds.Width,
                // Height = Screen.PrimaryScreen.Bounds.Height,
                Width = 998,
                Height = 800,
            };

            try
            {
                GraphicHandler.Init(form);
                _menu = new MainMenu();
                _menu.Init(form);
                _game = new Game();
                _game.Init();
                _game.Off();
                _menu.On();
                form.Show();
                TimeHandler.On(_menu);
            }
            catch (ArgumentOutOfRangeException e)
            {
                string mess = "Получено исключение ArgumentOutOfRangeException: " + e.Message;
                MessageBox.Show(mess);
            }

            Application.Run(form);
        }

        static public void NewGame()
        {
            TimeHandler.Off();
            _menu.Off();
            _game.On();
            TimeHandler.On(_game);
        }

        static public void Records()
        {

        }

        static public void ExitProgramm()
        {
            Application.Exit();
        }

        static private void EndGame()
        {
            TimeHandler.Off();
            _game.Off();
            _menu.On();
            TimeHandler.On(_menu);
        }

        static public void MainGameForm_KeyPress(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    EndGame();
                    break;
                default:
                    //KeyPress(e);
                    KeyPress?.Invoke(e.KeyCode);
                    break;
            }


        }
        

    }
}
