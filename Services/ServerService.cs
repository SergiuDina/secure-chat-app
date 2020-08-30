using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Client;
using DataContext;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServerService : IServerService
    {
        private List<ConnectedClient> _connectedClients = new List<ConnectedClient>();
        private UserContext _userContext = new UserContext();

        public ServerService()
        {
            if (_userContext.Database.EnsureCreated())
            {
                Console.WriteLine($"[{DateTime.Now}]: The user database was created");
            }
        }

        public void Logon(Guid clientId, string username, byte[] password)
        {
            var loginUser = _userContext.Users.FirstOrDefault(u => u.Username == username);             

            if (loginUser != null && loginUser.Password.SequenceEqual(password))
            {
                _connectedClients.Add(new ConnectedClient
                {
                    Id = clientId,
                    Username = username,
                    Password = null,
                    CallbackChannel = OperationContext.Current.GetCallbackChannel<IClientService>()
                });

                ConnectedClient loggedClient = _connectedClients.FirstOrDefault(c => c.Username == username);
                // Notify the client that the login was succesful
                loggedClient.CallbackChannel.SuccessfulLogin(true);

                foreach (var client in _connectedClients)
                    client.CallbackChannel.UpdateListOfConnectedClients(_connectedClients.Where(c => c.Username != client.Username)
                                                                                         .ToList());

                Console.WriteLine($"[{DateTime.Now}]: User <{username}> has logged in");
                return;
            }

            var newClient = new ConnectedClient
            {
                Id = clientId,
                Username = username,
                Password = null,
                CallbackChannel = OperationContext.Current.GetCallbackChannel<IClientService>()
            };

            _connectedClients.Add(newClient);
            newClient.CallbackChannel.SuccessfulLogin(false);

            _connectedClients.Remove(newClient);
        }

        public void Logoff(Guid clientId)
        {
            var loggedOffClient = _connectedClients.FirstOrDefault(c => c.Id == clientId);

            Console.WriteLine($"[{DateTime.Now}]: User <{loggedOffClient.Username}> has logged off");
            _connectedClients.Remove(loggedOffClient);

            foreach (var client in _connectedClients)
                client.CallbackChannel.UpdateListOfConnectedClients(_connectedClients.Where(c => c.Username != client.Username)
                                                                                         .ToList());
        }

        public void SendMessage(EncMessage message)
        {
            Console.WriteLine($"[{DateTime.Now}]: {message.Sender.Username} -> {message.Recipient.Username}");
            foreach (var elem in message.Payload)
            {
                Console.Write((elem == 0x34 ? "[SYN]" : elem.ToString()) + " ");
            }
            Console.WriteLine();

            var sender = _connectedClients.FirstOrDefault(c => c.Id == message.Sender.Id);
            var recipient = _connectedClients.FirstOrDefault(c => c.Id == message.Recipient.Id);

            if (recipient != null)
            {
                recipient.CallbackChannel.ReceiveNewMessage(message);
            }
        }

        public bool Register(string username, byte[] password)
        {
            var user = new User()
            {
                Username = username,
                Password = password
            };
            if (!_userContext.Users.Any(u => u.Username == username))
            {
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
                Console.WriteLine($"[{DateTime.Now}]: User <{username}> has registered");
                return true;
            }
            return false;
        }
    }
}
