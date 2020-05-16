﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_lesson7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Print(string str)
        {
            LogTextBox.AppendText(str + "\n");
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Print("LoadMenuItem_Click:");
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();
        }

        private void WorkerAboutButton_Click(object sender, RoutedEventArgs e)
        {
            Print("WorkerAboutButton_Click:");
        }
    }
}
