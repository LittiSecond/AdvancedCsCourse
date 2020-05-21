using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

using Task_lesson7.Entities;
using Task_lesson8_WcfService;

namespace Task_lesson7.VievModels
{
    class StaffObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IRemoteStaffService _service;

        /// <summary> делегат для отправки текстовых сообщений во внешнюю среду </summary>
        public Action<string> SendMessage;

        private Employee _selectedEmploye;
        public Employee SelectedEmployee
        {
            get { return _selectedEmploye; }
            set { _selectedEmploye = value; }
        }

        private ObservableCollection<Employee> _employeesO;
        private ObservableCollection<Departament> _departamentsO;


        public StaffObservable()
        {
            _employeesO = new ObservableCollection<Employee>();
            _departamentsO = new ObservableCollection<Departament>();

            StubCreateStaff();
            _service = new RemoteStaffService();
        }

        public ObservableCollection<Employee> EmployeesO
        {
            get { return _employeesO; }
        }

        public ObservableCollection<Departament> DepartamentsO
        {
            get { return _departamentsO; }
        }


        public void Employ(Employee w)
        {
            _employeesO.Add(w);
        }



        public void ReloadAll()
        {
            DepartamentInfo[] depArr = LoadDepartaments();
            EmployeeInfo[] emplArr = LoadEmployees();
            SynchronizeTables(depArr, emplArr);

            OnPropertyChanged("DepartamentsO");
            OnPropertyChanged("EmployeesO");
        }

        private DepartamentInfo[] LoadDepartaments()
        {
            DepartamentInfo[] depArr = _service.GetDepartaments();           

            if (depArr == null)
            {
                SendMessage?.Invoke("StaffObservable->LoadDepartaments: Ошибка. Список отделов не получен.");
                return null;
            }
            if (depArr.Length == 0)
            {
                SendMessage?.Invoke("StaffObservable->LoadDepartaments: Ошибка. Список отделов не получен.");
                return null;
            }
            
            _departamentsO.Clear();

            foreach (DepartamentInfo depI in depArr)
            {
                if (depI == null)
                {
                    _departamentsO.Add(new Departament(-1, String.Empty));
                    continue;
                }
                _departamentsO.Add(new Departament(depI.Id, depI.Name, depI.Info));
            }
            return depArr;
        }

        private EmployeeInfo[] LoadEmployees()
        {
            EmployeeInfo[] emplArr = _service.GetEmployees();
            if (emplArr == null)
            {
                SendMessage?.Invoke("StaffObservable->LoadEmployees: Ошибка. Список отделов не получен.");
                return null;
            }
            if (emplArr.Length == 0)
            {
                SendMessage?.Invoke("StaffObservable->LoadEmployees: Ошибка. Список отделов не получен.");
                return null;
            }
            _employeesO.Clear();

            foreach(EmployeeInfo empI in emplArr)
            {                
                _employeesO.Add(new Employee(empI.FirstName, empI.SurName, empI.Id)
                {
                    Appointment = empI.Appointment,
                    Salary = empI.Salary,
                    Info = empI.Info
                });
            }
            return emplArr;
        }

        private void SynchronizeTables(DepartamentInfo[] depArr, EmployeeInfo[] emplArr)
        {
            if (depArr == null || emplArr == null)
                return;
            // работников по отделам
            for (int i = 0; i < emplArr.Length; i++)
            {
                Employee emp = _employeesO[i];
                int depId = emplArr[i].DepartamentId;
                if (depId > 0)
                {
                    Departament dep = _departamentsO.FirstOrDefault(d => d.Id == depId);
                    if (dep != null)
                    {
                        dep.AddEmployee(emp);                        
                    }
                }
            }

            //расстановка вождей отделов
            for (int i = 0; i < depArr.Length; i++)
            {
                int empId = depArr[i].ChiefId;
                if (empId > 0)
                {
                    _departamentsO[i].SetChief(empId);
                }
            }

        }

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Test1()
        {
            SendMessage?.Invoke("StaffObservable->Test1: " + _service.GetDepartamentQuantity());
        }


        #region заглушка
        /// <summary>
        /// заглушка, пока не пределаю источник данных
        /// </summary>
        private void StubCreateStaff()
        {
            Departament dep1 = new Departament(85, "Отдел 1");
            Departament dep2 = new Departament(87, "Второй Отдел", "начальника не назначать");
            _departamentsO.Add(dep1);
            _departamentsO.Add(dep2);

            Employee emp1 = new Employee("Имя1", "surИмя1", 701)
            {
                Appointment = "должность1",
                Salary = 1001,
                Info = "инфо1"                
            };
            Employee emp2 = new Employee("Имя2", "surИмя2", 702)
            {
                Appointment = "должность2",
                Salary = 2002,
                Info = "инфо2"
            };
            Employee emp3 = new Employee("Имя3", "surИмя3", 703)
            {
                Appointment = "должность3",
                Salary = 3003,
                Info = "инфо3"
            };

            _employeesO.Add(emp1);
            _employeesO.Add(emp2);
            _employeesO.Add(emp3);

            dep1.AddEmployee(emp1);
            dep1.SetChief(emp1);
            dep1.AddEmployee(emp2);
            dep2.AddEmployee(emp3);
        }

        #endregion



    }
}
