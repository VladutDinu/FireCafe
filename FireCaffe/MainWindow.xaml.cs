using FireCaffeDAL;
using FireCaffeDAL.Models;
using FireCaffeDAL.Services;
using System;
using System.Data.Entity;
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
            btnLogin.Visibility = Visibility.Hidden;
            btnSignUp.Visibility = Visibility.Hidden;

            btnMenu.Visibility = Visibility.Visible;
            btnOffers.Visibility = Visibility.Visible;
            SilverCups.Visibility = Visibility.Visible;
            SilverCupsCount.Visibility = Visibility.Visible;
            GoldeCupsCount.Visibility = Visibility.Visible;
            GoldeCups.Visibility = Visibility.Visible;
            btnLocations.Visibility = Visibility.Visible;
            btnContact.Visibility = Visibility.Visible;
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
                    SilverCupsCount.Text += client.SilverCups.ToString();
                    GoldeCupsCount.Text += client.GoldenCups.ToString();
                    show_hide_standard();
                }
                
            }
            else
            {
                List<Client> c = clientServices.GetClientByCred(txtUsernameLogin.Text, txtPasswordLogin.Text);
                if (c.Count > 0)
                {
                    LoginPanel.Visibility = Visibility.Hidden;
                    SilverCupsCount.Text += c[0].SilverCups.ToString();
                    GoldeCupsCount.Text += c[0].GoldenCups.ToString();
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

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void Tea_Click(object sender, RoutedEventArgs e)
        {
            TeaPanel.Visibility = Visibility.Visible;
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
