using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Channels;

using Task_lesson8_WcfService;

namespace Task_lesson7
{
    class RemoteStaffClient : ClientBase<IRemoteStaffService>, IRemoteStaffService
    {
        public RemoteStaffClient(Binding binding, EndpointAddress endpoint) : base(binding, endpoint)
        { }

        // не работает, при вызове методов исключение: 
        //"System.ServiceModel.FaultException`1: 'Ссылка на объект не указывает на экземпляр объекта.'"

        public int GetDepartamentQuantity() => Channel.GetDepartamentQuantity();

        public DepartamentInfo[] GetDepartaments() => Channel.GetDepartaments();

        public EmployeeInfo[] GetEmployees() => Channel.GetEmployees();
    }
}
