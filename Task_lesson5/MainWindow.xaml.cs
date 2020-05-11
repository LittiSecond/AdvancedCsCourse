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

using Task_lesson5.Structures;

namespace Task_lesson5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Presenter _presenter;
        /// <summary> содержимое списка отделов ListBox Name="DepartamentList" на вкладке "Отделы" </summary>
        private List<DepartamentInfo> _departamentInfoList;
        /// <summary> содержимое списка работников отдела
        ///   ListBox Name="DepartWorkersList" на вкладке "Отделы" </summary>
        private List<EmployeeInfo> _depEmployeeInfoList;

        /// <summary> полный список сотрудников </summary>
        private List<EmployeeInfo> _fullEmployeeInfosList;


        public MainWindow()
        {
            InitializeComponent();
            _presenter = new Presenter(Print);
            _departamentInfoList = new List<DepartamentInfo>();
            _depEmployeeInfoList = new List<EmployeeInfo>();
            _fullEmployeeInfosList = new List<EmployeeInfo>();
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
            _presenter.LoadDefoultStaff();
            UpdateData(null, null);
        }

        private void ClearLogMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LogTextBox.Clear();
        }

        private void UpdateData(object sender, RoutedEventArgs e)
        {
            Print("MainWindow->UpdateData:");
            UpdateDepartamentList();
            UpdateFullEmployeesList();
        }

        private void UpdateDepartamentList()
        {
            Print("MainWindow->UpdateDepartamentList:");
            DepartamentList.Items.Clear();
            _departamentInfoList.Clear();
            ClearDepartamentInfo();
            foreach (var di in _presenter.GetDepartamentInfos())
            {
                DepartamentList.Items.Add(di.name);
                _departamentInfoList.Add(di);
            }
        }

        private void ClearDepartamentInfo()
        {            
            DepartamentName.Text = " ... отдел не выбран ...";
            DepartamentChief.Text = String.Empty;
            DepartamentInfoTextBox.Text = String.Empty;
            DepartWorkersList.Items.Clear();
            _depEmployeeInfoList.Clear();
        }

        /// <summary>
        /// Вывод информации об отделе в поля формы на вкладке "отдел" при выборе
        /// строки в списке отделов - ListBox Name="DepartamentList"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartamentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (sender as ListBox);
            int ind = lb.SelectedIndex;
            Print("MainWindow->DepartamentList_SelectionChanged: SelectedIndex = " + ind.ToString());
            if (ind < 0 || ind >= _departamentInfoList.Count)
            {
                ClearDepartamentInfo();
                return;
            }

            DepartamentInfo dpi = _departamentInfoList[ind];
            DepartamentName.Text = dpi.name;
            // вывод, кто начальник
            if (dpi.chiefId <= 0)
            {
                DepartamentChief.Text = "-"; // когда нет начальника
            }
            else
            {
                EmployeeInfo ei = _presenter.GetEmployeeInfo(dpi.chiefId);
                DepartamentChief.Text = ei.surName + " " + ei.firstName;
            }

            DepartamentInfoTextBox.Text = dpi.info;

            // вывод списка работников отдела
            ShowDepEmployeeList(dpi.id);           
        }

        /// <summary>
        /// Обновить список сотрудников отдела 
        /// ListBox Name="DepartWorkersList" на вкладке "Отделы"
        /// </summary>
        /// <param name="departamentId"></param>
        private void ShowDepEmployeeList(int departamentId)
        {
            DepartWorkersList.Items.Clear();
            _depEmployeeInfoList.Clear();

            Func<IEnumerable<EmployeeInfo>> enumerator = _presenter.GetEmployeeInfosOfDepertament(departamentId);
            
            if (enumerator != null)
            {
                foreach (EmployeeInfo ei in enumerator.Invoke())
                {
                    DepartWorkersList.Items.Add(ei.surName + " " + ei.firstName + " - " + ei.appointment);
                    _depEmployeeInfoList.Add(ei);
                }
            }
        }


        /// <summary>
        /// Обновить таблицу работников на вкладке "Сотрудники таблицей"
        /// </summary>
        private void UpdateEmployeesTable()
        {

        }

        /// <summary>
        /// Обновить список работников на вкладке "Сотрудники списком"
        /// </summary>
        private void UpdateFullEmployeesList()
        {
            FullWorkersList.Items.Clear();
            _fullEmployeeInfosList.Clear();
            foreach (EmployeeInfo ei in _presenter.GetEmployeeInfos())
            {
                string str = "таб.№" + ei.id.ToString() + "  " + ei.surName + " " + 
                    ei.firstName + " отдел: " + ei.departamentId.ToString();
                FullWorkersList.Items.Add(str);
                _fullEmployeeInfosList.Add(ei);
            }
        }

        /// <summary>
        /// Изменение выбранной строки в ListBox Name="FullWorkersList"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FullWorkersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (sender as ListBox);
            int ind = lb.SelectedIndex;
            if (ind < 0)
                WorkerAboutButton.IsEnabled = false;
            else
                WorkerAboutButton.IsEnabled = true;
        }

        private void WorkerAboutButton_Click(object sender, RoutedEventArgs e)
        {
            int ind = FullWorkersList.SelectedIndex;
            if (ind < 0 || ind > _fullEmployeeInfosList.Count)
                return;

            EmployeeInfo selectedEmp = _fullEmployeeInfosList[ind];

            AboutEmployeeWindow wind = new AboutEmployeeWindow();
            wind.Owner = this;

            wind.EmployeeInfo = selectedEmp;
            wind.DepartamentsInfo = _departamentInfoList;
            wind.ShowInfo();

            //wind.Show();
            wind.ShowDialog();
        }
    }


}
