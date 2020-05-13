using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task_lesson6.Structures;

namespace Task_lesson6.Entities
{
    class Employee : IEquatable<Employee>
    {

        public string FirstName { get; private set; }
        public string SurName { get; private set; }
        /// <summary> Табельный номер </summary>
        public int Id { get; private set; }
        public Departament Departament { get; set; }
        /// <summary> Должность </summary>
        public string Appointment { get; set; }
        public double Salary { get; set; }
        public string Info { get; set; }

        public Employee(string firstName, string surName, int id)
        {            
            FirstName = firstName;
            SurName = surName;
            Id = id;
            Appointment = String.Empty;
            Info = String.Empty;
        }               

        public bool Equals(Employee other)
        {
            if (other == null) return false;
            return  (FirstName == other.FirstName) &&
                    (SurName == other.SurName) &&
                    (Id == other.Id);
        }

        public EmployeeInfo EmployeeInfo
        {
            get
            {
                return new EmployeeInfo
                {
                    firstName = FirstName,
                    surName = SurName,
                    id = Id,
                    departamentId = (Departament == null) ? -1 : Departament.Id,
                    appointment = Appointment,
                    salary = Salary,
                    info = Info
                };
            }
        }
    }
}
