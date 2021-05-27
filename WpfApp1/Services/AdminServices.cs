using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireCaffeDAL;
using FireCaffeDAL.Models;
using FireCaffeDAL.Services;

namespace WpfApp1.Services
{
    public class AdminServices
    {
        public void addProduct(String Name, String Price, String Description, String Size, String Type)
        {
            ProductServices productServices = new FireCaffeDAL.Services.ProductServices();
            Product product = new Product();
            product.Name = Name;
            product.Price = Int32.Parse(Price);
            product.Description = Description;
            product.Size = Size;
            product.Type = Type;
            productServices.AddProduct(product);
        }

        public bool addNewClient(String FirstName, String LastName, String Password)
        {
            ClientServices clientServices = new ClientServices();
            //facade design
            Facade facade = new Facade();
            Client client = new Client();
            client.FirstName = FirstName;
            client.LastName = LastName;
            client.Username = FirstName + LastName;
            client.Password = Password;
            List<Client> c = clientServices.GetClientByPassword(client.Password);
            if (c.Count <= 0)
            {
                client.Admin = 0;
                client.GoldenCups = 0;
                client.SilverCups = 0;
                //facade design
                facade.addClient(client);
                return true;
            }
            return false;
        }
        public Client loginClient(String Username, String Password)
        {
            ClientServices clientServices = new ClientServices();
            List<Client> c = clientServices.GetClientByCred(Username, Password);
            if(c.Count == 1)
                return c[0];
            return null;
        }
    }
}
