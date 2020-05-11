using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task_lesson5.Entities;
using Task_lesson5.Structures;

namespace Task_lesson5
{
    class Presenter
    {
        private Staff _staff;

        /// <summary> делегат для отправки текстовых сообщений во внешнюю среду </summary>
        private Action<string> _sendMessage;

        public Presenter(Action<string> func)
        {
            _staff = new Staff();
            _sendMessage = func;
        }

        public void ClearStaff()
        {
            _sendMessage?.Invoke("Presenter->ClearStaff:");
            _staff.Clear();
        }

        public void LoadDefoultStaff()
        {
            _sendMessage?.Invoke("Presenter->LoadDefoultStaff:");
            CreateDefoultStaff();
        }

        public IEnumerable<DepartamentInfo> GetDepartamentInfos()
        {
            //if (4 + 5 == 8)
            //    yield break;
            //else
            return _staff.GetDepartamentInfos(); 
        }

        public IEnumerable<EmployeeInfo> GetEmployeeInfos()
        {
            return _staff.GetEmployeeInfos();
        }

        public EmployeeInfo GetEmployeeInfo(int id)
        {
            return _staff.GetEmployeeInfo(id);
        }

        public Func<IEnumerable<EmployeeInfo>> GetEmployeeInfosOfDepertament(int departamentId)
        {
            return _staff.GetEmployeeInfosOfDepertament(departamentId);
        }











        private void CreateDefoultStaff()
        {
            ClearStaff();
            int d1 = _staff.AddDepertament("Администрация", "12-34");
            int w1 = _staff.Employ("Никанор", "Язьков");
            _staff.ChangeAppointment(w1, "Директор");
            _staff.AllocateEmployee(w1, d1);
            _staff.AppointChif(w1, d1);
            _staff.SetSelary(w1, 100);

            int d2 = _staff.AddDepertament("Бугалтерия", "12-56");
            int w2 = _staff.Employ("Григорий", "Худяков");
            _staff.ChangeAppointment(w2, "Начальниг бугалтерии - Посредственный бугалтер");
            _staff.AllocateEmployee(w2, d2);
            _staff.AppointChif(w2, d2);
            _staff.SetSelary(w2, 70.11);

            int d3 = _staff.AddDepertament("Отдел кадров", "12-78");
            int w3 = _staff.Employ("Капитон", "Арцишевский");
            _staff.ChangeAppointment(w3, "Начальник отдела кадров");
            _staff.AllocateEmployee(w3, d3);
            _staff.AppointChif(w3, d3);
            _staff.SetSelary(w3, 40);

            int d4 = _staff.AddDepertament("Цех 1", "43-12");
            int w4 = _staff.Employ("Анатолий", "Звягин");
            _staff.ChangeAppointment(w4, "Начальник цеха");
            _staff.AllocateEmployee(w4, d4);
            _staff.AppointChif(w4, d4);
            _staff.SetSelary(w4, 30);
                        
            int w5 = _staff.Employ("Тит", "Мозгоедов");
            _staff.ChangeAppointment(w5, "Рабочий");
            _staff.AllocateEmployee(w5, d4);            
            _staff.SetSelary(w5, 18);

            int w6 = _staff.Employ("Варфоломей", "Игнатов");
            _staff.ChangeAppointment(w6, "Рабочий");
            _staff.AllocateEmployee(w6, d4);
            _staff.SetSelary(w6, 18);

            int d5 = _staff.AddDepertament("Цех 2", "43-34");
            int w7 = _staff.Employ("Самсона", "Давыдова");
            _staff.ChangeAppointment(w7, "Начальник цеха");
            _staff.AllocateEmployee(w7, d5);
            _staff.AppointChif(w7, d5);
            _staff.SetSelary(w7, 30);

            int w8 = _staff.Employ("Ёгер", "Гленов");
            _staff.ChangeAppointment(w8, "Рабочий");
            _staff.AllocateEmployee(w8, d5);            
            _staff.SetSelary(w5, 17);

            int w9 = _staff.Employ("Остап", "Енютин");
            _staff.ChangeAppointment(w9, "Рабочий");
            _staff.AllocateEmployee(w9, d5);
            _staff.SetSelary(w9, 17.51);

        }


    }


}
