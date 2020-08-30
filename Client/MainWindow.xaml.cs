using Entities;
using Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Security.Cryptography;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Guid _userId;

        private ServerProxy _serverProxy;
        private IServerService _serverChannel;
        private ClientService _clientService;
        private string _username;

        Window1 objChatRoom = new Window1();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            usernameTextBox.GotFocus += OnFocusUsername;
            passwordTextBox.GotFocus += OnFocusPassword;

            CreateServerChannel();
        }

        private void OnSuccessfulLogin(bool isSuccessful)
        {
            if (isSuccessful)
            {
                objChatRoom = new Window1(_serverChannel, _userId, _username, _serverProxy, _clientService);
                this.Visibility = Visibility.Hidden;
                objChatRoom.usernameLabel.Content = _username;
                objChatRoom.Show();

                objChatRoom.sendButton.IsEnabled = true;
                objChatRoom.messageTextBox.IsEnabled = true;
            } 
            else
            {
                MessageBox.Show("Username or password is wrong", "Incorrect credentials", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CreateServerChannel()
        {
            _clientService = new ClientService();

            _serverProxy = new ServerProxy(_clientService);
            _serverChannel = _serverProxy.ChannelFactory.CreateChannel();

            _clientService.SuccessfulLoginEvent += OnSuccessfulLogin;
        }

        private void OnFocusPassword(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Clear();
            passwordTextBox.GotFocus -= OnFocusPassword;
        }

        private void OnFocusUsername(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Clear();
            usernameTextBox.GotFocus -= OnFocusUsername;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text)||(usernameTextBox.Text=="Username"))
            {
                MessageBox.Show("Username can not be null/whitespace");
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Password)||(passwordTextBox.Password=="password"))
            {
                MessageBox.Show("Password can not be null/whitespace");
                return;
            }

            _username = usernameTextBox.Text;
            var password = passwordTextBox.Password;

            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(password);
            var hashedPassword = sha1.ComputeHash(data);

            _userId = Guid.NewGuid();

            try
            {
                _serverChannel.Logon(_userId, _username, hashedPassword);
            }
            catch (InvalidOperationException exc)
            {
                MessageBox.Show("InvalidOperationException:" + exc.Message + " " + exc.StackTrace);
            }
            catch (System.ServiceModel.FaultException exc)
            {
                MessageBox.Show("FaultException:" + exc.Message + " " + exc.StackTrace);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Failed to logon. Make sure the server application is running.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                usernameTextBox.IsEnabled = true;
                passwordTextBox.IsEnabled = true;
                loginButton.IsEnabled = true;
                return;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(passwordTextBox.Password);
            var hashedPassword = sha1.ComputeHash(data);
            if (!_serverChannel.Register(usernameTextBox.Text, hashedPassword))
            {
                MessageBox.Show("Please choose another username that's not taken already", "Username taken", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                MessageBox.Show("Account created successfully", "Registered", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
