using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Task_lesson8_WcfService
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost host = new ServiceHost(typeof(RemoteStaffService),
                new Uri("http://localhost:8095/RemoteStaffService"),
                new Uri("net.tcp://localhost/RemoteStaffService"));

            host.AddServiceEndpoint(typeof(IRemoteStaffService),
                new BasicHttpBinding(), "http://localhost:8095/RemoteStaffService");
            host.AddServiceEndpoint(typeof(IRemoteStaffService),
                new NetTcpBinding(), "net.tcp://localhost/RemoteStaffService");

            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true});

            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

            host.Open();

            Console.WriteLine("Ну, как-бы сервис запущен.");
            Console.ReadLine();
        }
    }
}
