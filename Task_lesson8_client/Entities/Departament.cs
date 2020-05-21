using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_lesson7.Entities
{
    class Departament
    {
        //private string _name;
        //private int _id;
        //public string Name { get { return _name; } }
        //public int Id { get { return _id; } }

        private List<Employee> _workers;

        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Chief { get; set; }
        public string Info { get; set; }

        public List<Employee> Workers
        {
            get
            {
                return _workers;
            }
        }

        public Departament(int id, string name, string info = null)
        {
            Id = id;
            Name = (name != null)? name : String.Empty;
            _workers = new List<Employee>();
            Info = (info == null)? String.Empty : info;
        }

        /// <summary>
        /// Назначить работника в отдел
        /// </summary>
        /// <param name="emp"></param>
        /// <returns> false - не удалось назначить </returns>
        public bool AddEmployee(Employee emp)
        {
            if (emp == null)
                return false;
            //if (emp.DepartamentId > 0)
            //    return false;
            _workers.Add(emp);
            emp.Departament = this;
            return true;
        }

        /// <summary>
        /// Исключить работника из отдела.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool DismissEmplyee(Employee emp)
        {
            if (emp == null) return false;
            if (emp.Departament != this) return false;
            if (!_workers.Remove(emp)) return false;
            if (emp == Chief)
                Chief = null;
            emp.Departament = null;
            return true;
        }

        /// <summary>
        /// Исключить из отдела всех работников
        /// </summary>
        /// <returns> список исключённых работников </returns>
        public Employee[] DismissAll()
        {            
            foreach (Employee worker in _workers)
            {                
                if (worker != null)
                {
                    worker.Departament = null;                    
                }
            }
            Employee[] list = _workers.ToArray();
            _workers.Clear();
            Chief = null;
            return list;
        }


        /// <summary>
        /// Переименовать отдел
        /// </summary>
        /// <param name="newName">новое название отдела</param>        
        public void ChangeName(string newName)
        {
            //if (String.IsNullOrEmpty(newName)) throw new ArgumentException("", "newName");
            Name = newName;
        }

        public void ChangeId(int newId)
        {
            Id = newId;
        }

        /// <summary>
        /// Получить список сотрудников отдела
        /// </summary>
        /// <returns></returns>
        public Employee[] GetEmployees()
        {
            return _workers.ToArray();
        }

        /// <summary>
        /// Назначить начальника. Кандидат должен уже числиться в этом отделе.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool SetChief(Employee emp)
        {
            if (emp == null)
                return false;
            if (!_workers.Contains(emp))
                return false;
            Chief = emp;
            return true;
        }


        public bool SetChief(int employeeId)
        {
            if (employeeId <= 0)
            { return false; }
            bool flag = false;
            
            //for (int i = 0; i < _workers.Count; i++)
            //{
            //    Employee emp = _workers[i];
            //    if (emp.Id == employeeId)
            //    {
            //        Chief = emp;
            //        flag = true;
            //        break;
            //    }
            //}

            Employee emp = _workers.FirstOrDefault(e => e.Id == employeeId);
            if (emp != null)
            {
                    Chief = emp;
                    flag = true;                    
            }

            return flag;
        }

    }
}
