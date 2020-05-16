namespace Task_lesson6.Structures
{
    public struct EmployeeInfo
    {
        // значение id = -1 признак ошибки, такого Employee нет.

        public string firstName;
        public string surName;
        public int id;
        public int departamentId;
        public string appointment;
        public double salary;
        public string info;

        public static EmployeeInfo Error 
        {
            get
            {
                //EmployeeInfo ei = new EmployeeInfo();
                //ei.id = -1;
                //return ei;
                return new EmployeeInfo
                {
                    firstName = null,
                    surName = null,
                    id = -1,
                    departamentId = -1,
                    appointment = null,
                    salary = 0,
                    info = null
                };
            }
        }

    }
}
