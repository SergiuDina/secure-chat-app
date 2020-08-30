using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract(IsOneWay = true)]
        void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients);

        [OperationContract(IsOneWay = true)]
        void ReceiveNewMessage(EncMessage message);

        [OperationContract(IsOneWay = true)]
        void SuccessfulLogin(bool isSuccessful);

    }
}
