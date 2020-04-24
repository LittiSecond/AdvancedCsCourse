using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            Form form = new MainGameForm
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height,
            };
            //form.Width = 1422;
            //form.Height = 800;
            SplashScreen.Init(form);
            Game.Init(form);            
            form.Show();
            Game.Off();
            SplashScreen.On();
            SplashScreen.Draw();
            Application.Run(form);
        }

        static public void NewGame()
        {
            SplashScreen.Off();
            Game.On();
            Game.Draw();
        }

        static public void Records()
        {

        }

        static public void ExitProgramm()
        {
            Application.Exit();
        }

        static public void EndGame()
        {
            Game.Off();
            SplashScreen.On();
            SplashScreen.Draw();
        }


    }
}
