using Client;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract(CallbackContract =typeof(IClientService))]
    public interface IServerService
    {
        [OperationContract(IsOneWay = true)]
        void Logon(Guid clientId, string username, byte[] password);

        [OperationContract(IsOneWay = true)]
        void Logoff(Guid clientId);

        [OperationContract(IsOneWay = true)]
        void SendMessage(EncMessage message);

        [OperationContract]
        bool Register(string username, byte[] password);
    }
}
