using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientService : IClientService
    {
        public delegate void NewMessageReceivedDelegate(EncMessage message);
        public event NewMessageReceivedDelegate NewMessageReceivedEvent;

        public delegate void ConnectedClientsListUpdatedDelegate(List<ConnectedClient> connectedClients);
        public event ConnectedClientsListUpdatedDelegate ConnectedClientsListUpdatedEvent;

        public delegate void SuccesfulLoginDelegate(bool isSuccessful);
        public event SuccesfulLoginDelegate SuccessfulLoginEvent;

        public void ReceiveNewMessage(EncMessage message)
        {
            NewMessageReceivedEvent(message);
        }

        public void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients)
        {
            ConnectedClientsListUpdatedEvent?.Invoke(connectedClients);
        }

        public void SuccessfulLogin(bool isSuccessful)
        {
            SuccessfulLoginEvent?.Invoke(isSuccessful);
        }
    }
}
