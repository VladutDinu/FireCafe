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
    public class UserServices
    {
        private const int num = 10;
        public void buyProduct(Client loggedClient, Product p)
        {
            ClientServices clientServices = new ClientServices();
            loggedClient.SilverCups += Int32.Parse(p.Size);
            loggedClient.GoldenCups += loggedClient.SilverCups / num;
            loggedClient.SilverCups = loggedClient.SilverCups % num;
            clientServices.Update(loggedClient);
        }
        public bool buyProduct_GoldenCups(Client loggedClient, Product p)
        {
            ClientServices clientServices = new ClientServices();
            if (p.Price <= loggedClient.GoldenCups)
            {
                loggedClient.GoldenCups -= Int32.Parse(p.Size);
                clientServices.Update(loggedClient);
                return true;
            }
            return false;
           
        }
    }

    
}
