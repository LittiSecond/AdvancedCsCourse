using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;


namespace Task_lesson8_WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(
        IncludeExceptionDetailInFaults = true)]
    public class RemoteStaffService : IRemoteStaffService
    {
        private const string CONNECTION_STRING_NAME = "StaffConnectionString";

        public int GetDepartamentQuantity()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            int ret = -1;
            string sqlExpresion = @"SELECT COUNT(*) FROM [Staff].[dbo].[Departaments]";
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                SqlCommand command = new SqlCommand(sqlExpresion, connect);

                ret = Convert.ToInt32(command.ExecuteScalar());
            }

            return ret;
        }

        public DepartamentInfo[] GetDepartaments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;

            string sqlExpresion = @"SELECT * FROM [Staff].[dbo].[Departaments]";

            List<DepartamentInfo> depList = new List<DepartamentInfo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpresion, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["Id"]);
                            string name = Convert.ToString(reader["Name"]);
                            string info = Convert.ToString(reader["Info"]);
                            object chiefO = reader["Chief"];
                            int chiefId = (chiefO is DBNull) ? -1 : Convert.ToInt32(chiefO);
                            depList.Add(new DepartamentInfo()
                            {
                                Id = id,
                                Name = name,
                                Info = info,
                                ChiefId = chiefId
                            });

                        }
                    }

            }

            return depList.ToArray();

        }

        public EmployeeInfo[] GetEmployees()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;

            string sqlExpresion = @"SELECT * FROM [Staff].[dbo].[Employees]";

            List<EmployeeInfo> emplList = new List<EmployeeInfo>();

            using (SqlConnection connection = new SqlConnection(connectionString))
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
                            string appointment = Convert.ToString(reader["Appointment"]);
                            string info = Convert.ToString(reader["Info"]);
                            object depO = reader["DepartamentId"];
                            int depId = (depO is DBNull) ? -1 : Convert.ToInt32(depO);
                            object salO = reader["Salary"];
                            double salary = (salO is DBNull) ? 0 : Convert.ToDouble(salO);

                            emplList.Add(new EmployeeInfo()
                            {
                                Id = id,
                                FirstName = firstName,
                                SurName = surName,
                                Appointment = appointment,
                                Info = info,
                                DepartamentId = depId,
                                Salary = salary
                            });

                        }
                    }
            }

            return emplList.ToArray();
        }


    }
}
