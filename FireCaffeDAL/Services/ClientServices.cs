using FireCaffeDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireCaffeDAL.Services
{
    public class ClientServices
    {

        public void AddClient(Client client)
        {

            using (var context = new MasterContext())
            {
                context.Set<Client>().Add(client);
                context.SaveChanges();
            }

        }
        public List<Client> GetClientByPassword(string password)
        {
            using (var context = new MasterContext())
            {
                var client= (from c in context.Clients
                             where c.Password == password
                             select c).ToList();
                return client;
            }
        }

        public List<Client> GetClientByCred(string username, string password)
        {
            using (var context = new MasterContext())
            {
                var client = (from c in context.Clients
                              where c.Password == password && c.Username == username
                              select c).ToList();
                return client;
            }
        }

        public List<Client> GetClients()
        {
            using (var context = new MasterContext())
            {
                return context.Clients.ToList();
            }

        }

        public void Update(Client client)
        {
            using (var context = new MasterContext())
            {
                context.Entry(client).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteClient(Client client)
        {
            using (var context = new MasterContext())
            {
                context.Entry(client).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

    }
}
