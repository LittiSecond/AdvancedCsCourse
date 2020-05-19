using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
//using System.Runtime.CompilerServices;

using Task_lesson7.Entities;


namespace Task_lesson7.VievModels
{
    class StaffObservable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public delegate string GetConnectionString();
        public Func<string> GetConnectionString;
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
            _employeesO.Clear();
            _departamentsO.Clear();
            if (GetConnectionString == null)
            {
                SendMessage?.Invoke("StaffObservable->ReloadAll: Ошибка! Не получить строку подключения.");
                return;
            }

            string sqlExpresion = @"SELECT * FROM [Staff].[dbo].[Departaments]";

            List<int> chiefs = new List<int>();

            string con = GetConnectionString();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpresion, connection);
                //SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32( reader["Id"]);
                        string name = Convert.ToString(reader["Name"]);
                        string info = Convert.ToString(reader["Info"]);
                        object chiefO = reader["Chief"];
                        //int chiefId = Convert.ToInt32(chiefO);
                        //int chiefId = (chiefO != null) ? Convert.ToInt32(chiefO) : -1;
                        int chiefId = (chiefO is DBNull) ? -1 : Convert.ToInt32(chiefO);
                        chiefs.Add(chiefId);
                        _departamentsO.Add(new Departament(id, name, info));
                    }
                }

            }
            
            sqlExpresion = @"SELECT * FROM [Staff].[dbo].[Employees]";

            List<int> depsId = new List<int>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpresion, connection);
                //SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            string firstName = Convert.ToString(reader["FirstName"]);
                            string surName = Convert.ToString(reader["SurName"]);
                            string appointment =  Convert.ToString(reader["Appointment"]);                          
                            string info = Convert.ToString(reader["Info"]);
                            object depO = reader["DepartamentId"];
                            int depId = (depO is DBNull) ? -1 : Convert.ToInt32(depO);
                            depsId.Add(depId);
                            object salO = reader["Salary"];
                            double salary = (salO is DBNull) ? 0 : Convert.ToDouble(salO);

                            Employee emp = new Employee(firstName, surName, id)
                            {
                                Appointment = appointment,
                                Info = info,
                                Salary = salary
                            };

                            if (depId> 0)
                            {
                                Departament dep = _departamentsO.FirstOrDefault(d => d.Id == depId);
                                dep.AddEmployee(emp);
                            }

                            _employeesO.Add(emp);

                        }
                    }
            }
            
            // расстановка вождей по отделам
            for (int i = 0; i < chiefs.Count; i++)
            {
                int id = chiefs[i];
                if (id > 0)
                {
                    Departament dep = _departamentsO[i];
                    dep.Chief = _employeesO.FirstOrDefault(e => e.Id == id);
                }
            }


            OnPropertyChanged("DepartamentsO");
            OnPropertyChanged("EmployeesO");

        }







        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public void Test1()
        {
            if (GetConnectionString == null)
            {
                SendMessage?.Invoke("StaffObservable->ReloadAll: Ошибка! Не получить строку подключения.");
                return;
            }

            string sqlExpresion = @"SELECT COUNT(*) FROM [Staff].[dbo].[Employees]";
            //string sqlExpresion = @"SELECT COUNT(*) FROM [dbo].[Employees]";
            //string sqlExpresion = @"SELECT COUNT(*) FROM Employees";

            string con = GetConnectionString();
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpresion, connection);

                int c = Convert.ToInt32(command.ExecuteScalar());
                SendMessage?.Invoke($"StaffObservable->Test1: '{sqlExpresion}' = {c}");

            }
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
