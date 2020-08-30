using Services;
using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.Encryption;
using System.Security.Cryptography;
using Services.Encryption;
using System.Diagnostics;

namespace Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IServerService _serverChannel;
        private Guid _userId;
        private string _username;
        private ServerProxy _serverProxy;
        private ClientService _clientService;
        private Dictionary<string, byte[]> _clientsPrivateKeys = new Dictionary<string, byte[]>();
        private byte[] privateKey;

        public Window1()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public Window1(IServerService serverChannel, Guid userId, string username, ServerProxy serverProxy, ClientService clientService)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this._serverChannel = serverChannel;
            this._userId = userId;
            this._username = username;
            this._serverProxy = serverProxy;
            this._clientService = clientService;

            _clientService.ConnectedClientsListUpdatedEvent += UpdateListOfConnectedClients;
            _clientService.NewMessageReceivedEvent += DisplayNewMessage;

            clientsListBox.SelectionChanged += OnClientSelect;
            Closed += OnClosed;
        }

        private void OnClientSelect(object sender, SelectionChangedEventArgs e)
        {
            var selectedClient = clientsListBox.SelectedItem as ConnectedClient;
            if (selectedClient != null && !_clientsPrivateKeys.ContainsKey(selectedClient.Username))
            {
                NegotiateKey(selectedClient);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageTextBox.Text) || (messageTextBox.Text == "Write a message"))
            {
                MessageBox.Show("Please enter a message first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var selectedClientRecipient = clientsListBox.SelectedItem as ConnectedClient;

            if (selectedClientRecipient == null)
            {
                MessageBox.Show("Please select a recipient first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            // TODO: Here we will encrypt the message
            using (Aes aes = Aes.Create())
            {
                var encryptedMessage = new EncMessage()
                {
                    UsedForDHExchange = false,
                    Payload = AesEncryption.EncryptStringToBytes(messageTextBox.Text, _clientsPrivateKeys[selectedClientRecipient.Username], new byte[16]),
                    Recipient = selectedClientRecipient,
                    Sender = new ConnectedClient()
                    {
                        Id = _userId,
                        Username = _username
                    }
                };

                _serverChannel.SendMessage(encryptedMessage);
            }

            messagesListBox.Items.Add($"Me> {messageTextBox.Text}");
            messageTextBox.Clear();
        }

        private void messageTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            messageTextBox.Clear();
            messageTextBox.Opacity = 0.4;
            messageTextBox.Background = Brushes.White;
            messageTextBox.Foreground = Brushes.DarkBlue;
        }

        private void UpdateListOfConnectedClients(List<ConnectedClient> connectedClients)
        {
            this.clientsListBox.ItemsSource = connectedClients;
            this.clientsListBox.DisplayMemberPath = "Username";
            this.clientsListBox.SelectedValuePath = "Id";
        }

        private void NegotiateKey(ConnectedClient selectedClient)
        {
            var synMessage = new EncMessage()
            {
                Sender = new ConnectedClient()
                {
                    Id = _userId,
                    Username = _username
                },
                Recipient = selectedClient,
                UsedForDHExchange = true,
                Payload = new byte[1] { 0x34 }
            };
            _serverChannel.SendMessage(synMessage);
            byte[] rndBytes = new byte[32];
            RNGCryptoServiceProvider.Create().GetBytes(rndBytes);

            privateKey = Curve25519.ClampPrivateKey(rndBytes);
            byte[] publicKey = Curve25519.GetPublicKey(privateKey);

            synMessage.Payload = publicKey;
            _serverChannel.SendMessage(synMessage);
        }

        private void DisplayNewMessage(EncMessage message)
        {
            if (!message.UsedForDHExchange)
            {
                // TODO: Here we will decrypt the message and show it
                using (Aes aes = Aes.Create())
                {
                    this.messagesListBox.Items.Add($"{message.Sender.Username}> " +
                        $"{AesEncryption.DecryptStringFromBytes(message.Payload, _clientsPrivateKeys[message.Sender.Username], new byte[16])}");
                }
            }
            else
            {
                if (message.Payload[0] == 0x34 && message.Payload.Length == 1)
                {
                    byte[] rndBytes = new byte[32];
                    RNGCryptoServiceProvider.Create().GetBytes(rndBytes);

                    privateKey = Curve25519.ClampPrivateKey(rndBytes);
                    byte[] publicKey = Curve25519.GetPublicKey(privateKey);

                    var pubKeyMessage = new EncMessage()
                    {
                        Recipient = message.Sender,
                        Sender = message.Recipient,
                        UsedForDHExchange = true,
                        Payload = publicKey
                    };
                    _serverChannel.SendMessage(pubKeyMessage);
                }
                else
                {
                    if (!_clientsPrivateKeys.ContainsKey(message.Sender.Username))
                    {
                        _clientsPrivateKeys[message.Sender.Username] = Curve25519.GetSharedSecret(privateKey, message.Payload);
                    }
                }
            }
        }

        private void OnClosed(object sender, EventArgs e)
        {
            try
            {
                _serverChannel.Logoff(this._userId);
            } 
            catch (Exception)
            { }

            _serverProxy.Close();
            base.OnClosed(e);
            Process.GetCurrentProcess().Kill();
        }

        private void messageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendButton_Click(this, e);
            }
        }
    }
}
