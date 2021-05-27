using FireCaffeDAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Models
{
    public class Facade
    {
        //Facade design pattern
        private ClientServices _clientServices;
        private ProductServices _productServices;
        public Facade()
        {
            _clientServices = new ClientServices();
            _productServices = new ProductServices();
        }
        public void addClient(Client client)
        {
            _clientServices.AddClient(client);
        }
    }
}
