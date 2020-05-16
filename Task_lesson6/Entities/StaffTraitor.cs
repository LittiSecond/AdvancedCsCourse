using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson6.Entities
{
    class StaffTraitor : Staff
    {

        public Dictionary<int, Employee> Employees
        {
            get
            {
                return _employeeList;
            }
        }

        public Dictionary<int, Departament> Departaments
        {
            get
            {
                return _departamentList;
            }
        }


    }
}
