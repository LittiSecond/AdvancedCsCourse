using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

using Task_lesson6.Entities;
using Task_lesson6.VievModels;

namespace Task_lesson6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AboutEmployeeWindow _aboutEmpWindow;
        private StaffTraitor _staffTraitor;

        private StaffObserable _staffO;

        public MainWindow()
        {
            InitializeComponent();
            //_aboutEmpWindow = new AboutEmployeeWindow();
            //_aboutEmpWindow.Owner = this; 
            _staffTraitor = this.Resources["StaffT"] as StaffTraitor;
            //_staffO = new StaffObserable();
            _staffO = this.Resources["SO"] as StaffObserable;
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Print(string message)
        {
            LogTextBox.AppendText(message + "\n");
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_staffTraitor != null)
            {
                ClearStaff(_staffTraitor);
                CreateDefoultStaff(_staffTraitor);
            }
           
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();
        }

        private void UpdateData(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Вывод информации об отделе в поля формы на вкладке "отдел" при выборе
        /// строки в списке отделов - ListBox Name="DepartamentList"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartamentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Изменение выбранной строки в ListBox Name="FullWorkersList"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullWorkersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void WorkerAboutButton_Click(object sender, RoutedEventArgs e)
        {
            //if (_aboutEmpWindow == null)
            //{
                _aboutEmpWindow = new AboutEmployeeWindow();
                _aboutEmpWindow.Owner = this;
            //}
            //_aboutEmpWindow.Show();
            _aboutEmpWindow.ShowDialog();
        }





        private void ClearStaff(Staff staff)
        {
            if (staff == null)
                return;
            //_sendMessage?.Invoke("Presenter->ClearStaff:");
            staff.Clear();
        }

        private void CreateDefoultStaff(Staff staff)
        {
            if (staff == null)
                return;
            //ClearStaff();
            int d1 = staff.AddDepertament("Администрация", "12-34");
            int w1 = staff.Employ("Никанор", "Язьков");
            staff.ChangeAppointment(w1, "Директор");
            staff.AllocateEmployee(w1, d1);
            staff.AppointChif(w1, d1);
            staff.SetSelary(w1, 100);

            int d2 = staff.AddDepertament("Бугалтерия", "12-56");
            int w2 = staff.Employ("Григорий", "Худяков");
            staff.ChangeAppointment(w2, "Начальниг бугалтерии - Посредственный бугалтер");
            staff.AllocateEmployee(w2, d2);
            staff.AppointChif(w2, d2);
            staff.SetSelary(w2, 70.11);

            int d3 = staff.AddDepertament("Отдел кадров", "12-78");
            int w3 = staff.Employ("Капитон", "Арцишевский");
            staff.ChangeAppointment(w3, "Начальник отдела кадров");
            staff.AllocateEmployee(w3, d3);
            staff.AppointChif(w3, d3);
            staff.SetSelary(w3, 40);

            int d4 = staff.AddDepertament("Цех 1", "43-12");
            int w4 = staff.Employ("Анатолий", "Звягин");
            staff.ChangeAppointment(w4, "Начальник цеха");
            staff.AllocateEmployee(w4, d4);
            staff.AppointChif(w4, d4);
            staff.SetSelary(w4, 30);

            int w5 = staff.Employ("Тит", "Мозгоедов");
            staff.ChangeAppointment(w5, "Рабочий");
            staff.AllocateEmployee(w5, d4);
            staff.SetSelary(w5, 18);

            int w6 = staff.Employ("Варфоломей", "Игнатов");
            staff.ChangeAppointment(w6, "Рабочий");
            staff.AllocateEmployee(w6, d4);
            staff.SetSelary(w6, 18);

            int d5 = staff.AddDepertament("Цех 2", "43-34");
            int w7 = staff.Employ("Самсона", "Давыдова");
            staff.ChangeAppointment(w7, "Начальник цеха");
            staff.AllocateEmployee(w7, d5);
            staff.AppointChif(w7, d5);
            staff.SetSelary(w7, 30);

            int w8 = staff.Employ("Ёгер", "Гленов");
            staff.ChangeAppointment(w8, "Рабочий");
            staff.AllocateEmployee(w8, d5);
            staff.SetSelary(w5, 17);

            int w9 = staff.Employ("Остап", "Енютин");
            staff.ChangeAppointment(w9, "Рабочий");
            staff.AllocateEmployee(w9, d5);
            staff.SetSelary(w9, 17.51);

        }

   

    }
}
