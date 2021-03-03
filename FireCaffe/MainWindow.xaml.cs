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
        public NameScope[] names;
        public MainWindow()
        {
            InitializeComponent();
            var rand = new Random();
            var people = rand.Next(0, 1000);
            PeopleToday.Content += people.ToString();
            OrdersToday.Content += rand.Next(people, 2*people).ToString();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            txtUsername.Clear();
            txtPassword.Clear();
            if (true)
            {
                Menu.Visibility = Visibility.Visible;
                Offers.Visibility = Visibility.Visible;
                CoffeBeans.Visibility = Visibility.Visible;
                Locations.Visibility = Visibility.Visible;
                Contact.Visibility = Visibility.Visible;

                Login.Visibility = Visibility.Hidden;
                Username.Visibility = Visibility.Hidden;
                Password.Visibility = Visibility.Hidden;
                txtUsername.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                btnSubmit.Visibility = Visibility.Hidden;
            }
        }

        private void Exit_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            Console.WriteLine("tepup");
            Environment.Exit(0);
        }
    }
}
