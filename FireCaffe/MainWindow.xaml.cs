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
        private Client loggedClient;
        public MainWindow()
        {
            InitializeComponent();
            var rand = new Random();
            var people = rand.Next(0, 500);
            PeopleToday.Content += people.ToString();
            OrdersToday.Content += rand.Next(people, 2*people).ToString();
            LoginPanel.Visibility = Visibility.Hidden;
            SignUpPanel.Visibility = Visibility.Hidden;
        }
        private void show_hide_standard(Client c)
        {
            btnLogin.Visibility = Visibility.Hidden;
            btnSignUp.Visibility = Visibility.Hidden;

            btnMenu.Visibility = Visibility.Visible;
            btnOffers.Visibility = Visibility.Visible;
            SilverCups.Visibility = Visibility.Visible;
            SilverCupsCount.Visibility = Visibility.Visible;
            GoldenCupsCount.Visibility = Visibility.Visible;
            GoldenCups.Visibility = Visibility.Visible;
            btnLocations.Visibility = Visibility.Visible;
            btnContact.Visibility = Visibility.Visible;

            SilverCupsCount.Text += c.SilverCups.ToString();
            GoldenCupsCount.Text += c.GoldenCups.ToString();
        }
        private void show_hide_admin()
        {
            btnLogin.Visibility = Visibility.Hidden;
            btnSignUp.Visibility = Visibility.Hidden;

            btnMenu.Visibility = Visibility.Visible;
            btnOffers.Visibility = Visibility.Visible;
            AddProducts.Visibility = Visibility.Visible;
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
                    show_hide_standard(client);
                    loggedClient = client;
                }
                
            }
            else
            {
                List<Client> c = clientServices.GetClientByCred(txtUsernameLogin.Text, txtPasswordLogin.Text);
                if (c.Count > 0)
                {
                    if (c[0].Admin!=1) {
                        LoginPanel.Visibility = Visibility.Hidden;
                        show_hide_standard(c[0]);
                        loggedClient = c[0];
                    }
                    else
                    {
                        LoginPanel.Visibility = Visibility.Hidden;
                        show_hide_admin();
                    }
                                       
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
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("Tea");
            productType.Content = "Tea";
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Coffee_Click(object sender, RoutedEventArgs e)
        {
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("Coffe");
            productType.Content = "Coffee";
        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            AddProductsPanel.Visibility = Visibility.Visible;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            Product product = new Product();
            product.Name = txtProductName.Text;
            product.Price = Int32.Parse(txtProductPrice.Text);
            product.Description = txtDescripton.Text;
            product.Size = txtSize.Text;
            product.Type = txtType.Text;
            productServices.AddProduct(product);
        }

        private void HotDrinks_Click(object sender, RoutedEventArgs e)
        {
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("HotDrink");
            productType.Content = "Hot drinks";
        }

        private void Desserts_Click(object sender, RoutedEventArgs e)
        {
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("Dessert");
            productType.Content = "Desserts";
        }

        private void btnBuyProduct_Click(object sender, RoutedEventArgs e)
        {
            ClientServices clientServices = new ClientServices();
            Product p = (Product)lvProducts.SelectedItem;
            loggedClient.SilverCups += Int32.Parse(p.Size);
            loggedClient.GoldenCups += loggedClient.SilverCups/10;
            loggedClient.SilverCups = loggedClient.SilverCups % 10;
            clientServices.Update(loggedClient);
            SilverCupsCount.Text = loggedClient.SilverCups.ToString();
            GoldenCupsCount.Text = loggedClient.GoldenCups.ToString();
        }

        private void btnBuyWithGoldenCups_Click(object sender, RoutedEventArgs e)
        {
            ClientServices clientServices = new ClientServices();
            Product p = (Product)lvProducts.SelectedItem;
            loggedClient.GoldenCups -= p.Price;
            clientServices.Update(loggedClient);
            GoldenCupsCount.Text = loggedClient.GoldenCups.ToString();
        }
    }
}
