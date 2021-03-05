using FireCaffeDAL;
using FireCaffeDAL.Models;
using FireCaffeDAL.Services;
using System;
using System.Collections.Generic;
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


namespace FireCaffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            var rand = new Random();
            var people = rand.Next(0, 1000);
            PeopleToday.Content += people.ToString();
            OrdersToday.Content += rand.Next(people, 2*people).ToString();
            LoginPanel.Visibility = Visibility.Hidden;
            SignUpPanel.Visibility = Visibility.Hidden;
        }
        private void show_hide_standard()
        {
            LoginButton.Visibility = Visibility.Hidden;
            SignUpButton.Visibility = Visibility.Hidden;

            Menu.Visibility = Visibility.Visible;
            Offers.Visibility = Visibility.Visible;
            CoffeBeans.Visibility = Visibility.Visible;
            Locations.Visibility = Visibility.Visible;
            Contact.Visibility = Visibility.Visible;
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            ClientServices clientServices = new ClientServices();
            if (SignUpPanel.Visibility==Visibility.Visible)
            {
                Client client = new Client();
                client.FirstName = txtFirstName.Text;
                client.LastName = txtLastNameSignUp.Text;
                client.Username = txtFirstName.Text+ txtLastNameSignUp.Text;
                client.Password = txtPasswordSignUp.Text;
                List<Client> c = clientServices.GetClientByPassword(client.Password);
                while (c.Count > 0)
                {
                    txtPasswordSignUp.Clear();
                    MessageBox.Show("This password has been used by other members, please enter another one");
                    c = clientServices.GetClientByPassword(client.Password);
                    if (c.Count == 0)
                        break;
                }
                client.Admin = 0;
                client.GoldenCups = 0;
                client.SilverCups = 0;
                if (c.Count <= 0)
                {
                    clientServices.AddClient(client);
                    mainWindowViewModel.Clients.Add(client);
                    SignUpPanel.Visibility = Visibility.Hidden;
                    MessageBox.Show("Account created.");
                    show_hide_standard();
                }
                
            }
            else
            {
                List<Client> c = clientServices.GetClientByCred(txtUsernameLogin.Text, txtPasswordLogin.Text);
                if (c.Count > 0)
                {
                    LoginPanel.Visibility = Visibility.Hidden;
                    show_hide_standard();
                }
                else
                {
                    txtPasswordLogin.Clear();
                    txtUsernameLogin.Clear();
                    MessageBox.Show("Wrong credentials.");
                }

            }

        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login.Content = "Login";
            SignUpPanel.Visibility = Visibility.Hidden;
            if(LoginPanel.Visibility!=Visibility.Visible)
                LoginPanel.Visibility = Visibility.Visible;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUp.Content = "Sign Up";
            LoginPanel.Visibility = Visibility.Hidden;
            if (SignUpPanel.Visibility != Visibility.Visible)
                SignUpPanel.Visibility = Visibility.Visible;
        }
    }
}
