using System;
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
using System.Data;

using Task_lesson7.Entities;
using Task_lesson7.VievModels;

namespace Task_lesson7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StaffObservable _staff;

        public MainWindow()
        {
            InitializeComponent();
            _staff = this.Resources["Staff"] as StaffObservable;
            _staff.GetConnectionString = SendConnectionString;
            _staff.SendMessage = Print;
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
            _staff.ReloadAll();
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();
        }

        private void WorkerAboutButton_Click(object sender, RoutedEventArgs e)
        {
            Print("WorkerAboutButton_Click:");

            //int ind = FullWorkersList.SelectedIndex;
            //if (ind < 0 || ind > _fullEmployeeInfosList.Count)
            //    return;

            //EmployeeInfo selectedEmp = _fullEmployeeInfosList[ind];

            AboutEmployeeWindow wind = new AboutEmployeeWindow();
            wind.Owner = this;
            wind.DataContext = _staff;

            //wind.EmployeeInfo = selectedEmp;
            //wind.DepartamentsInfo = _departamentInfoList;
            //wind.ShowInfo();

            //wind.Show();
            wind.ShowDialog();
        }

        private string SendConnectionString()
        {
            return Task_lesson7.Properties.Settings.Default.StaffConnectionString;
        }

        private void TestMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _staff.Test1();
        }

        private void FullWorkersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = FullWorkersList.SelectedIndex;
            if (ind < 0)
            {
                WorkerAboutButton.IsEnabled = false;
            }
            else
            {
                WorkerAboutButton.IsEnabled = true;
            }
        }


    }
}
