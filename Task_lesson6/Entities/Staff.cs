using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task_lesson6.Structures;

namespace Task_lesson6.Entities
{

    /// <summary> Штат сотрудников </summary>
    class Staff
    {

        protected Dictionary<int, Departament> _departamentList;
        /// <summary> Полный список сотрудников, включаяя нераспределённых  </summary>
        protected Dictionary<int, Employee> _employeeList;
        /// <summary> Нераспределённые сотрудники </summary>
        protected Dictionary<int, Employee> _notAllocatedEmployees;
        //LinkedList<Employee>

        private const int FIRST_DEP_NUMBER = 1;
        private int _nexDepartamentNumber;
        private const int FIRST_EMPLOYEE_NUMBER = 101;
        private int _nextEmployeeNumber;

        public Staff()
        {
            _departamentList = new Dictionary<int, Departament>();
            _employeeList = new Dictionary<int, Employee>();
            _notAllocatedEmployees = new Dictionary<int, Employee>();
            _nexDepartamentNumber = FIRST_DEP_NUMBER;
            _nextEmployeeNumber = FIRST_EMPLOYEE_NUMBER;
        }

        /// <summary>
        /// Добавить отдел
        /// </summary>
        /// <param name="name"> название отдела </param>
        /// <returns> Id отдела. Значение -1 означает ошибка, отдел не создан </returns>
        public int AddDepertament(string name, string info = null, int id = -1 )
        {
            if (String.IsNullOrEmpty(name))
                return -1;
            if (_departamentList.Any(dep => dep.Value.Name == name))  // а если Value окажется null?
                return -1;            

            if (id > 0)
            {
                if (_departamentList.ContainsKey(id))
                    return -1;
            }
            else
                id = NextDepNumber();

            _departamentList.Add(id, new Departament(id, name, info));
            return id;
        }

        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="id"> номер отдела </param>
        /// <returns> false - не удалось удалить отдел</returns>
        public bool RemoveDepartament(int id)
        {
            if (!_departamentList.ContainsKey(id))
                return false;
            Departament dep = _departamentList[id];
            Employee[] eList = dep.DismissAll();
            foreach (Employee w in eList)
            {
                if (w != null)
                {
                    _notAllocatedEmployees.Add(w.Id, w);
                }
            }
            _departamentList.Remove(id);
            return true;
        }

        /// <summary>
        /// Нанять работника
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <returns>Табельный номер. Значение -1 - ошибка, работник не добавлен </returns>
        public int Employ(string firstName, string surName)
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(surName))
                return -1;
            int id = NextEmployNumber();
            Employee e = new Employee(firstName, surName, id);
            _employeeList.Add(id, e);
            _notAllocatedEmployees.Add(id, e);
            return id;
        }

        /// <summary>
        /// Нанять работника
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="surName"></param>
        /// <param name="salary"></param>
        /// <returns>Табельный номер. Значение -1 - ошибка, работник не добавлен </returns>
        public int Employ(string firstName, string surName, double salary)
        {
            int id = Employ(firstName, surName);
            if (id < 0) return id;
            SetSelary(id, salary);               
            return id;
        }


        /// <summary>
        /// Уволить работника
        /// </summary>
        /// <param name="id"> табельный номер </param>
        /// <returns> false - не удалось уволить работника </returns>
        public bool Dismiss(int id)
        {
            if (!_employeeList.ContainsKey(id))
                return false;
            Employee emp = _employeeList[id];
            emp.Departament?.DismissEmplyee(emp);
            _employeeList.Remove(id);
            return true;
        }

        //public Employee GetEmployee(int id)
        //{

        //}

        //public Departament GetDepartament(int id)
        //{

        //}

        /// <summary>
        /// Назначить работника в отдел (перевести в другой отдел)
        /// </summary>
        /// <param name="workerId">табельный номер работника</param>
        /// <param name="depNumber">номер отдела</param>
        /// <returns> false - неудалось выполнить назначение</returns>
        public bool AllocateEmployee(int workerId, int depNumber)
        {
            if (!_employeeList.ContainsKey(workerId))
                return false;
            if (!_departamentList.ContainsKey(depNumber))
                return false;
            Employee emp = _employeeList[workerId];
            Departament newDep = _departamentList[depNumber];
            Departament oldDep = emp.Departament;
            if (oldDep != null)
            {
                if (oldDep == newDep)
                    return true;

                oldDep.DismissEmplyee(emp);
                _notAllocatedEmployees.Add(emp.Id, emp);
            }
            if (newDep.AddEmployee(emp))
            {
                _notAllocatedEmployees.Remove(emp.Id);
            }
            else
                return false;

            return true;
        }

        /// <summary>
        /// Назначить работнику оклад
        /// </summary>
        /// <param name="workerId"> табельный номер работника </param>
        /// <param name="selary"> новый размер оклада </param>
        /// <returns> false  - неудалось изменить оклад </returns>
        public bool SetSelary(int workerId, double selary)
        {
            if (!_employeeList.ContainsKey(workerId))
                return false;
            Employee emp = _employeeList[workerId];
            emp.Salary = selary;
            return true;
        }

        /// <summary>
        /// Изменить должность
        /// </summary>
        /// <param name="workerId"> табельный номер работника </param>
        /// <param name="newAppointment"></param>
        /// <returns> удалось ли изменить </returns>
        public bool ChangeAppointment(int workerId, string newAppointment)
        {
            if (String.IsNullOrEmpty(newAppointment))
                return false;
            if (!_employeeList.ContainsKey(workerId))
                return false;
            Employee emp = _employeeList[workerId];
            emp.Appointment = newAppointment;
            return true;
        }

        /// <summary>
        /// Назначить начальника в отдел
        /// </summary>
        /// <param name="workerId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public bool AppointChif(int workerId, int depId)
        {
            if (!_employeeList.ContainsKey(workerId))
                return false;
            if (!_departamentList.ContainsKey(depId))
                return false;
            Employee worker = _employeeList[workerId];
            Departament dep = _departamentList[depId];
            return dep.SetChief(worker);
        }


        public DepartamentInfo GetDepartament(int id)
        {
            if (!_departamentList.ContainsKey(id))
                return DepartamentInfo.Error;
            Departament dep = _departamentList[id];            
            return dep.DepartamentInfo;
        }

        public EmployeeInfo GetEmployeeInfo(int id)
        {
            if (!_employeeList.ContainsKey(id))
                return EmployeeInfo.Error;
            return _employeeList[id].EmployeeInfo;
        }


        private int NextDepNumber()
        {
            while (_departamentList.ContainsKey(_nexDepartamentNumber))
            {
                _nexDepartamentNumber++;
            }
            return _nexDepartamentNumber++;
        }

        private int NextEmployNumber()
        {
            while (_employeeList.ContainsKey( _nextEmployeeNumber))
            {
                _nextEmployeeNumber++;
            }
            return _nextEmployeeNumber++;
        }

        private bool DismissFromDep(Employee worker)
        {
            if (worker == null)
                return false;

            Departament dep = worker.Departament;
            if (dep == null)
                return true;
            dep.DismissEmplyee(worker);
            _notAllocatedEmployees.Add(worker.Id, worker);
            return true;
        }


        public IEnumerable<DepartamentInfo> GetDepartamentInfos()
        {
            if (_departamentList.Count == 0)
                yield break;

            foreach (var dep in _departamentList)
            {
                yield return dep.Value.DepartamentInfo;
            }
        }


        public IEnumerable<EmployeeInfo> GetEmployeeInfos()
        {
            if (_employeeList.Count == 0)
                yield break;

            foreach (var emp in _employeeList)
            {
                yield return emp.Value.EmployeeInfo;
            }
        }

        //  а это не удалось заставить работать
        //
        //public IEnumerable<EmployeeInfo> GetEmployeeInfosOfDepartament(int departamentId)
        //{
        //    if (!_departamentList.ContainsKey(departamentId))
        //        yield break;

        //    Departament dep = _departamentList[departamentId];
        //    yield return dep.GetEmployeeInfos();
        //}

        public Func<IEnumerable<EmployeeInfo>> GetEmployeeInfosOfDepertament(int departamentId)
        {
            if (!_departamentList.ContainsKey(departamentId))
                return null;
            Departament dep = _departamentList[departamentId];
            return dep.GetEmployeeInfos;
        }

        public void Clear()
        {
            foreach (var dep in _departamentList)
            {
                dep.Value?.DismissAll();
            }
            _employeeList.Clear();
            _notAllocatedEmployees.Clear();
            _departamentList.Clear();
        }

    }
}
