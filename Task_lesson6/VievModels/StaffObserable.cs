using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Task_lesson6.Entities;

namespace Task_lesson6.VievModels
{
    class StaffObserable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Employee> _employeesO;
        private ObservableCollection<Departament> _departamentsO;
        
        public StaffObserable()
        {
            _employeesO = new ObservableCollection<Employee>();
            _departamentsO = new ObservableCollection<Departament>();
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

         

    }
}
