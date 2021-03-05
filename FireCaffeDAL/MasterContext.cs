using FireCaffeDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL
{
    public class MasterContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public MasterContext()
            : base("defaultConnection")
        {

        }
    }
}
