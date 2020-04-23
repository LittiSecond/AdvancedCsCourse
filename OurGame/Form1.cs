using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OurGame
{
    public partial class MainGameForm : Form
    {
        public MainGameForm()
        {
            InitializeComponent();
        }

        public void SetSplashScreenUIVisibility(bool visibility)
        {
            buttonsPanel.Visible = visibility;
            messageLabel.Visible = visibility;
        }

        public Color GetColor()
        {
            return this.BackColor;
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            //messageLabel.Text = "MainGameForm->button1_Click: 1";
            Program.NewGame();
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            messageLabel.Text = "Функция Рекорды не реализована.";
            Program.Records();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Program.ExitProgramm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonsPanel.Visible = !buttonsPanel.Visible;
        }

        private void MainGameForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                Program.EndGame();
            }
        }
    }
}
