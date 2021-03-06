using System;
using FireCaffeDAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Services
{
    public class ProductServices
    {
        public List<Product> GetProducts()
        {
            using (var context = new MasterContext())
            {
                return context.Products.ToList();
            }

        }
        public List<Product> GetProductsByType(string type)
        {
            using (var context = new MasterContext())
            {
                    var product = (from c in context.Products
                                  where c.Type == type
                                  select c).ToList();
                    return product;
            }

        }
    }
}
