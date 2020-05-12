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
using System.Windows.Shapes;

using Task_lesson5.Structures;

namespace Task_lesson5
{
    /// <summary>
    /// Interaction logic for AboutEmployeeWindow.xaml
    /// </summary>
    public partial class AboutEmployeeWindow : Window
    {
        private List<DepartamentInfo> _departamentInfoList;
        
        public AboutEmployeeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public EmployeeInfo EmployeeInfo { get; set; }
        public List<DepartamentInfo> DepartamentsInfo
        {
            set
            {
                _departamentInfoList = value;
            }        
        }

        public void ShowInfo()
        {
            IdTexBox.Text = EmployeeInfo.id.ToString();
            WorkerNameText.Text = EmployeeInfo.surName + " " + EmployeeInfo.firstName;
            AppointTextBox.Text = EmployeeInfo.appointment;
            SalaryTextBox.Text = EmployeeInfo.salary.ToString();
            InfoTextBox.Text = EmployeeInfo.info;

            //DepartamentComboBox.Items.Clear();

            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = " нет ";
            DepartamentComboBox.Items.Add(newTextBlock);
            DepartamentComboBox.SelectedIndex = 0;
            for (int i = 0; i < _departamentInfoList.Count; i++)
            {
                
                newTextBlock = new TextBlock();
                newTextBlock.Text = _departamentInfoList[i].name;
                DepartamentComboBox.Items.Add(newTextBlock);

                if (_departamentInfoList[i].id == EmployeeInfo.departamentId)
                {
                    DepartamentComboBox.SelectedIndex = i + 1;
                }
            }

            /*
             *                 TextBlock newTextBlock = new TextBlock();
                newTextBlock.Text = s;
                box.Items.Add(newTextBlock);
             * */
        }

    }
}
