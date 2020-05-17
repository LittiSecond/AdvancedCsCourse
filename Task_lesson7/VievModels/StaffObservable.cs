using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Task_lesson7.Entities;

namespace Task_lesson7.VievModels
{
    class StaffObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employeesO;
        private ObservableCollection<Departament> _departamentsO;

        public StaffObservable()
        {
            _employeesO = new ObservableCollection<Employee>();
            _departamentsO = new ObservableCollection<Departament>();

            StubCreateStaff();
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
