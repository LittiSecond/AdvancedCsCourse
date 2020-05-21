using System.Runtime.Serialization;

namespace Task_lesson8_WcfService
{
    [DataContract]
    public struct EmployeeInfo
    {
        // значение id = -1 признак ошибки, такого Employee нет.

        [DataMember]
        public string FirstName;
        [DataMember]
        public string SurName;
        [DataMember]
        public int Id;
        [DataMember]
        public int DepartamentId;
        [DataMember]
        public string Appointment;
        [DataMember]
        public double Salary;
        [DataMember]
        public string Info;

        public static EmployeeInfo Error 
        {
            get
            {
                return new EmployeeInfo
                {
                    FirstName = null,
                    SurName = null,
                    Id = -1,
                    DepartamentId = -1,
                    Appointment = null,
                    Salary = 0,
                    Info = null
                };
            }
        }

    }
}
