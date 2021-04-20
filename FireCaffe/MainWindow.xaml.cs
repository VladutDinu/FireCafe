using FireCaffeDAL;
using FireCaffeDAL.Models;
using FireCaffeDAL.Services;
using WpfApp1.Services;
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
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
using Pdf417EncoderLibrary;
using IronBarCode;
using System.Web;
using System.Data.SQLite;
using Microsoft.Maps.MapControl.WPF;
using log4net;
using log4net.Config;
namespace FireCaffe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        private Client loggedClient;
        Panel[] panels = new Panel[8];
        private const int num = 10;
        FileStream fs = new FileStream("../../log4netConfig.xml", FileMode.Open);
        private protected ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindow()
        {
            InitializeComponent();
            XmlConfigurator.Configure(fs);
            var rand = new Random();
            var people = rand.Next(0, 500);
            PeopleToday.Content += people.ToString();
            OrdersToday.Content += rand.Next(people, 2*people).ToString();
            //load_panel();

        }
        private static ImageSource ToImageSource(System.Drawing.Image image, ImageFormat imageFormat)
        {
            BitmapImage bitmap = new BitmapImage();

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                // Save to the stream
                image.Save(stream, imageFormat);

                // Rewind the stream
                stream.Seek(0, SeekOrigin.Begin);

                // Tell the WPF BitmapImage to use this stream
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            return bitmap;
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
            AddProducts.Visibility = Visibility.Visible;
            btnLocations.Visibility = Visibility.Visible;
            btnContact.Visibility = Visibility.Visible;
            scanBarCOde.Visibility = Visibility.Visible;
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            ClientServices clientServices = new ClientServices();
            if (SignUpPanel.Visibility==Visibility.Visible)
            {
                AdminServices adminServices = new AdminServices();
                if(!adminServices.addNewClient(txtFirstName.Text, txtLastNameSignUp.Text, txtPasswordSignUp.Password))
                {
                    txtPasswordSignUp.Clear();
                    MessageBox.Show("This password has been used by other members, please enter another one");
                }
                else
                {
                    List<Client> c = clientServices.GetClientByCred(txtFirstName.Text+ txtLastNameSignUp.Text, txtPasswordSignUp.Password);
                    mainWindowViewModel.Clients.Add(c[0]);
                    SignUpPanel.Visibility = Visibility.Hidden;
                    MessageBox.Show("Account created.");
                    show_hide_standard(c[0]);
                    loggedClient = c[0];
                    logger.Info("New client has been added to DB "+ loggedClient.Username);
                }
                
            }
            else
            {
                AdminServices adminServices = new AdminServices();
                Client c = adminServices.loginClient(txtUsernameLogin.Text, txtPasswordLogin.Password);
                if ( c == null)
                {
                    txtPasswordLogin.Clear();
                    txtUsernameLogin.Clear();
                    MessageBox.Show("Wrong credentials.");
                    logger.Warn("Wrong credentials.");
                }
                else if(c.Admin !=1)
                {
                    LoginPanel.Visibility = Visibility.Hidden;
                    show_hide_standard(c);
                    loggedClient = c;
                    logger.Info("Simple user logged in " + loggedClient.Username);
                }
                else
                {
                    LoginPanel.Visibility = Visibility.Hidden;
                    show_hide_admin();
                    loggedClient = c;
                    logger.Info("Admin logged in " + loggedClient.Username);
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
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
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
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("Coffe");
            productType.Content = "Coffee";
        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            ProductsPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Visible;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AdminServices adminServices = new AdminServices();
            adminServices.addProduct(txtProductName.Text, txtProductPrice.Text, txtDescripton.Text, txtSize.Text, txtType.Text);
        }

        private void HotDrinks_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("HotDrink");
            productType.Content = "Hot drinks";
        }

        private void Desserts_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            ProductsPanel.Visibility = Visibility.Visible;
            lvProducts.ItemsSource = productServices.GetProductsByType("Dessert");
            productType.Content = "Desserts";
        }

        private void btnBuyProduct_Click(object sender, RoutedEventArgs e)
        {
            UserServices us = new UserServices();
            us.buyProduct(loggedClient, (Product)lvProducts.SelectedItem);
            SilverCupsCount.Text = loggedClient.SilverCups.ToString();
            GoldenCupsCount.Text = loggedClient.GoldenCups.ToString();
        }

        private void btnBuyWithGoldenCups_Click(object sender, RoutedEventArgs e)
        {
            UserServices us = new UserServices();
            if(us.buyProduct_GoldenCups(loggedClient, (Product)lvProducts.SelectedItem))
            {
                GoldenCupsCount.Text = loggedClient.GoldenCups.ToString();
            }
            else
            {
                MessageBox.Show("Not enough golden cups. :(");
            }
            
        }

        private void btnOffers_Click(object sender, RoutedEventArgs e)
        {
            ProductsPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            OffersPanel.Visibility = Visibility.Visible;
            GeneratedBarcode MyBarCode = BarcodeWriter.CreateBarcode(loggedClient.Password, BarcodeWriterEncoding.Code128).ResizeTo(450, 250).SaveAsImage("barcode.jpeg");
            BarCode.Source = ToImageSource(MyBarCode.Image, ImageFormat.Png);
        }

        private void btnBrowseBarCode_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "E:\\an3facsem2\\pdpf2\\FireCaffe\\FireCaffe\\bin\\Debug";
            dlg.Filter = "Image files (*.jpeg)|*.jpeg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                userBarCode.Source = bitmap;
                BarcodeResult FormatsResult = BarcodeReader.QuicklyReadOneBarcode("barcode.jpeg", BarcodeEncoding.Code128);
                string result = FormatsResult.Text;
                
                ClientServices clientServices = new ClientServices();
                List<Client> scannedClient = clientServices.GetClientByPassword(result);
                scannedClient[0].SilverCups += 2;
                if (scannedClient[0].SilverCups >= num) { 
                    scannedClient[0].GoldenCups += 1;
                    scannedClient[0].SilverCups = scannedClient[0].SilverCups%10;
                }

                clientServices.Update(scannedClient[0]);
                MessageBox.Show("User: "+scannedClient[0].Username+" has received 2 SilverCups");
            }
        }

        private void scanBarCOde_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            ProductsPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Visible;
        }

        private void bntLocation1_Click(object sender, RoutedEventArgs e)
        {
            //45.653050, 25.582512
            Microsoft.Maps.MapControl.WPF.Location location = new Microsoft.Maps.MapControl.WPF.Location(45.653050, 25.582512);
            myMap.Center = location;
            if (pushPin1.Visibility == Visibility.Hidden)
                pushPin1.Visibility = Visibility.Visible;
        }

        private void bntLocation2_Click(object sender, RoutedEventArgs e)
        {
            //45.644274, 25.595408
            Microsoft.Maps.MapControl.WPF.Location location = new Microsoft.Maps.MapControl.WPF.Location(45.644274, 25.595408);
            myMap.Center = location;
            if (pushPin2.Visibility == Visibility.Hidden)
                pushPin2.Visibility = Visibility.Visible;
        }

        private void btnLocations_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Hidden;
            ProductsPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Visible;
            AddProductsPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            OffersPanel.Visibility = Visibility.Hidden;
            ContactPanel.Visibility = Visibility.Visible;
            ProductsPanel.Visibility = Visibility.Hidden;
            AddProductsPanel.Visibility = Visibility.Hidden;
            BarCodeScannerPanel.Visibility = Visibility.Hidden;
            LocationPanel.Visibility = Visibility.Hidden;
        }
    }
}
