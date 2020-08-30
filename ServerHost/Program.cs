using Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ServerHost
{

    class Program
    {
        static void Main(string[] args)
        {
            var httpUrl = new Uri("net.tcp://localhost:2112/Services/ServerService");
            ServiceHost host = new ServiceHost(typeof(ServerService), httpUrl);
            host.AddServiceEndpoint(typeof(IServerService), new NetTcpBinding(), "");
            var smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = false;
            host.Description.Behaviors.Add(smb);
            host.Open();
            Console.WriteLine("Server has started");
            Console.ReadLine();
            host.Close();
        }
    }
}
