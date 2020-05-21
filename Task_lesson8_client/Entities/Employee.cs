using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Task_lesson7.Entities
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
        public string FullName
        {
            get
            {
                return SurName + " " + FirstName;
            }
        }

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


    }
}
